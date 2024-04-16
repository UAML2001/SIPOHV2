using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.WebPages;
using static PersonalUsuariosController;
using static SIPOH.Controllers.AC_CatalogosCompartidos.CatEjecucion_Cat_JuzgadosController;
using static SIPOH.Controllers.AC_CatalogosCompartidos.CatJuzgadosConTipoYSubtipoController;
using static SIPOH.Controllers.AC_RegistroHistoricos.RegistroHistoricoJOController;
using static SIPOH.Views.CustomRegistroIniciales;

namespace SIPOH
{
    public partial class RegistroUsuario : System.Web.UI.Page
    {
       public int IdPermiso = 17;
       public bool PermisoVer;
       public bool PermisoEditar;
       public bool PermisoEliminar;
       public bool PermisoAdministrador;
       public bool PermisoSistemas;
       public bool PermisoUsuario;
            
        protected void Actions()
        { 

            if(IdPermiso == 17)
            {
                ModulosVer(PermisoVer);
                ModulosEditar(PermisoEditar);
                ModulosSistemas(PermisoSistemas);
                ModulosAdministrador(PermisoAdministrador);
                ModulosEliminar(PermisoEliminar);
                CargarCatPerfil(PermisoSistemas, PermisoUsuario);
            }
            else
            {
                MensajeError("No se logro obtener los subpermisos");
            }

        }
        protected void ModulosVer(bool mode)
        {
            busquedaUsuario(mode);
        }
        protected void ModulosEditar(bool mode)
        {
            btnagregarUsuario(mode);
        }
        protected void ModulosEliminar(bool mode)
        {

        }
        protected void ModulosAdministrador(bool mode)
        {

        }
        protected void ModulosUsuario(bool mode)
        {
            
        }
        protected void ModulosSistemas(bool mode)
        {
            CargarCatJuzgado(mode);
        }
        
        protected void busquedaUsuario(bool modo)
        {
            lblNombreUsuario.Visible = modo;
            inputBuscarusuario.Visible = modo;
            btnSearchUsuario.Visible = modo;

        }
        protected void btnagregarUsuario(bool mode)
        {
            btnSearchNuevoUsuario.Visible = mode;
            btnModalGuardar.Visible = mode;
        }
        protected void obtnerPermisos(int IdPermiso, int IdPerfil)
        {
            List<PermisosAsociados> permisos = ObtenerSubPermisos(IdPermiso, IdPerfil);
            foreach (var permiso in permisos)
            {
                Debug.WriteLine("v: " + permiso.ver + permiso.editar + permiso.eliminar + permiso.administrador + permiso.superUsuario + permiso.usuario);
                PermisoVer= permiso.ver;
                PermisoEditar = permiso.editar;
                PermisoEliminar = permiso.eliminar;
                PermisoAdministrador = permiso.administrador;
                PermisoSistemas = permiso.superUsuario;
                PermisoUsuario = permiso.usuario;
                Debug.WriteLine("de: " + PermisoVer + PermisoEditar+ PermisoEliminar + PermisoAdministrador+ PermisoSistemas);
            }
        }
            
        
        protected void Page_Load(object sender, EventArgs e)
        {
            int sessionTimeout = 1 * 60; // 20 minutos
            Session.Timeout = sessionTimeout;

            // Verifica si el usuario está autenticado
            if (!User.Identity.IsAuthenticated)
            {

                Response.Redirect("~/Default.aspx");
                return;
            }
            string circuito = HttpContext.Current.Session["TCircuito"] as string;
            string Perfil = HttpContext.Current.Session["Perfil"] as string;
            List<string> enlaces = HttpContext.Current.Session["enlace"] as List<string>;
            //bool tienePermiso = enlaces.Any(enlace => enlace.Contains("/permisos"));
            bool tienePermiso = enlaces != null ? enlaces.Any(enlace => enlace.Contains("/registroUsuario")) : false;

            // Si enlaces es nulo, redirige a Default.aspx
            if (enlaces == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            if ((circuito == "a" || circuito == "d") && tienePermiso)
            {
                Visible = true;
            }
            else
            {
                Visible = false;
                Response.Redirect("~/Views/ContenidoDisponible/contenido-denegado");
            }
            if (!IsPostBack)
            {
                int IdJuzgado = int.Parse(Session["IDJuzgado"]as string);
                int Idperfil = int.Parse(Session["IdPerfil"]as string);
                

                obtnerPermisos(IdPermiso , Idperfil);

                Actions();
                GetCircuito(IdJuzgado);
                LimpiarCampos();
                formRegistroUsuario.Visible = false;
                inputBuscarusuario.Text = "";
                
                

                
            }
            
        }
        protected void grdVwBusquedaUsuarios_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdVwBusquedaUsuarios.EditIndex = -1;
            ActualizarTabla();
            // Vuelve a cargar los datos en el GridView
            
        }
        protected void grdVwBusquedaUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Encuentra el botón en la fila actual
                Button btnCustom = (Button)e.Row.FindControl("btnAlta");
                Button btnBaja = (Button)e.Row.FindControl("btnBaja");
                Button btnUsuario = (Button)e.Row.FindControl("btnIpUsuario");
                // Obtiene el ID de usuario de los datos enlazados
                string idUsuario = DataBinder.Eval(e.Row.DataItem, "IdUsuario").ToString();
                string Usuario = DataBinder.Eval(e.Row.DataItem, "Usuario").ToString();
                // Establece el ID de usuario como un atributo personalizado del botón
                btnCustom.Attributes["data-idUsuario"] = idUsuario;
                btnUsuario.Attributes["data-Usuario"] = Usuario;
                btnBaja.Attributes["data-idUsuario"] = idUsuario;
                string circuito = HttpContext.Current.Session["TCircuito"] as string;
                if (circuito == "a")
                {
                    btnUsuario.Visible = true;

                }
                else
                {
                    btnUsuario.Visible = false;

                }









            }
        }
        protected void btnCustom_BajaUsuario(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            int idUsuario = int.Parse(btn.Attributes["data-idUsuario"]);
            var respuesta = BajaDeUsuario(idUsuario);
            if (respuesta)
            {
                ActualizarTabla();                
                MensajeExito("¡Usuario fue dado de baja exitosamente!");
                UpdatePanelBusquedaUsuario.Update();
            }
            else
            {
                ActualizarTabla();
                grdVwBusquedaUsuarios.EditIndex = -1;
                MensajeError("¡Error en dar de baja tu usuario!");
                UpdatePanelBusquedaUsuario.Update();
            }
        }
        protected void btnCustom_DesbloquearIpUsuario(object sender, EventArgs e)
        {
            try {
                Button btn = (Button)sender;
                string Usuario = btn.Attributes["data-Usuario"];                
                bool respuesta = DesbloquearUsuario(Usuario);
                if (respuesta) { MensajeExito("La Ip del usuario fue desbloqueado y los accesos fueron reiniciados"); } 
                else
                {
                    MensajeError("Surgio un problema en el desbloqueo de Ip ");
                }
            }
            catch (Exception ex){
                MensajeError("Surgio un problema en el desbloqueo de Ip:" + ex);
            }
        }

        protected void btnCustom_AltaUsuario(object sender, EventArgs e)
        {            
            try
            {
                Button btn = (Button)sender;
                
                int idUsuario = int.Parse(btn.Attributes["data-idUsuario"]);                             

                var respuesta = AltaDeUsuario(idUsuario);
                if (respuesta)
                {
                    ActualizarTabla();
                    MensajeExito("¡Usuario dado de alta exitosamente!");
                }
                else
                {
                    ActualizarTabla();
                    MensajeError("¡Error al dar de alta al usuario!");
                }

                // Actualiza el UpdatePanel para reflejar los cambios en el GridView.
                UpdatePanelBusquedaUsuario.Update();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ex: " + ex.Message);
            }
        }

        protected void grdVwBusquedaUsuarios_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            
            try
            {
                GridViewRow row = grdVwBusquedaUsuarios.Rows[e.RowIndex];
                int IdUsuarioSelected = int.Parse(((Label)row.FindControl("txtIdUsuario")).Text);               
                string nuevoUsuario = ((TextBox)row.FindControl("txtUsuario")).Text;
                string nuevoContrasena = ((TextBox)row.FindControl("txtContraseñaDesencriptada")).Text;               
                string nuevoTelefono = ((TextBox)row.FindControl("txtTelefono")).Text;                
                if (
                        string.IsNullOrEmpty(nuevoUsuario) ||
                        string.IsNullOrEmpty(nuevoContrasena) ||                       
                        string.IsNullOrEmpty(nuevoTelefono) ||                        
                        (IdUsuarioSelected <= 0)

                        )
                {


                    // Determinar cuál campo está vacío
                    if (                        
                     string.IsNullOrEmpty(nuevoUsuario))
                        throw new Exception("Ingresa un usuario");
                    else if (string.IsNullOrEmpty(nuevoContrasena))
                        throw new Exception("Ingresa una contraseña");                    
                    else if (string.IsNullOrEmpty(nuevoTelefono))
                        throw new Exception("Ingresa un numero telefonico");                                         
                    else if (IdUsuarioSelected <= 0)
                        throw new Exception("El usuario no fue encontrado vuelve a buscar tu usuario");


                }
                DataUpdateUsuario datos = new DataUpdateUsuario
                {
                    
                    usuario = nuevoUsuario,
                    pass = CryptographyController.EncryptString(nuevoContrasena),
                   
                    telefono = int.Parse(nuevoTelefono)
                    
                };
                var valor = ActualizarUsuario(IdUsuarioSelected, datos);
                if (valor)
                {
                    MensajeExito("¡Usuario actualizado!");
                    grdVwBusquedaUsuarios.EditIndex = -1;
                    ActualizarTabla();
                    UpdatePanelBusquedaUsuario.Update();
                }
                else
                {
                    grdVwBusquedaUsuarios.EditIndex = -1;
                    ActualizarTabla();
                    UpdatePanelBusquedaUsuario.Update();
                    throw new Exception("¡No se pudo generar tu registro!");
                }
            }
            catch(Exception ex)
            {
                string mensajeError = $"{ex.Message}.";
                MensajeError(mensajeError);
            }
                UpdatePanelPersonal.Update();
            
           


        }
        protected bool ActualizarTabla()
        {
            string Usuario = inputBuscarusuario.Text;
            int juzgado = int.Parse(Session["IDJuzgado"] != null ? Session["IDJuzgado"].ToString() : string.Empty);
            int Perfil = int.Parse(Session["IdPerfil"] != null ? Session["IdPerfil"].ToString() : string.Empty);

            List<DataBuscarUsuario> infoUsuario = GetUsuario(Usuario, juzgado, Perfil);
            if (infoUsuario != null && infoUsuario.Any())
            {
                grdVwBusquedaUsuarios.Visible = true;
                grdVwBusquedaUsuarios.DataSource = infoUsuario;
                grdVwBusquedaUsuarios.DataBind();
                UpdatePanelBusquedaUsuario.Update();
                return true;
            }
            else
            {
                grdVwBusquedaUsuarios.Visible = true;
                grdVwBusquedaUsuarios.DataSource = infoUsuario;
                grdVwBusquedaUsuarios.DataBind();
                
                UpdatePanelBusquedaUsuario.Update();
                return false;
            }
        }
        protected void btnBuscarUsuario(object sender, EventArgs e)
        {
            formRegistroUsuario.Visible = false;
            try
            {
                bool resultado = ActualizarTabla();
                if(resultado){
                    grdVwBusquedaUsuarios.EditIndex = -1;
                    MensajeExito("Usuario encontrado.");
                }
                else
                {
                    grdVwBusquedaUsuarios.EditIndex = -1;
                    MensajeError("Usuario no fue encontrado.");
                }

                //grdVwBusquedaUsuarios.Columns[10].Visible = false; // Índice de la columna del botón de eliminar
                //grdVwBusquedaUsuarios.Columns[9].Visible = true;

            }
            catch (Exception ex)
            {
                    MensajeError("Error " + ex.Message);
                Debug.WriteLine("ERROR: "+ ex.Message);
            }
            UpdatePanelBusquedaUsuario.Update();
        }


        protected void grdVwBusquedaUsuarios_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Obtener el IdUsuario de la fila seleccionada
            //int idUsuario = Convert.ToInt32(grdVwBusquedaUsuarios.DataKeys[e.NewEditIndex].Value);
            //Debug.WriteLine("Debug: IDUSUARIO MODAL: " + idUsuario);


        }                
        protected void grdVwBusquedaUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = grdVwBusquedaUsuarios.SelectedRow;
                int idUsuario = Convert.ToInt32(row.Cells[0].Text);
                // Ajusta esta lógica según tus necesidades específicas.
               

                var respuesta = AltaDeUsuario(idUsuario);
                if (respuesta)
                {
                    ActualizarTabla();
                    MensajeExito("¡Usuario dado de alta exitosamente!");
                }
                else
                {
                    ActualizarTabla();
                    MensajeError("¡Error al dar de alta al usuario!");
                }

                // Actualiza el UpdatePanel para reflejar los cambios en el GridView.
                UpdatePanelBusquedaUsuario.Update();
            }catch(Exception ex)
            {
                Debug.WriteLine("ex: " +  ex.Message);
            }
        }

       


        protected void btnNuevoUsuario(object sender, EventArgs e)
        {
            grdVwBusquedaUsuarios.Visible = false;
            LimpiarCampos();
            formRegistroUsuario.Visible = true;
            UpdatePanelPersonal.Update();
        }
        protected void GridTable_RowEditing(object sender, GridViewEditEventArgs e)
        {
        }
        
        
        protected void inputCatPerfil_SelectedIndexChanged(object sender, EventArgs e)
        {
            string perfil = inputPerfil.SelectedItem.Text.ToUpper();
            copyPerfil.Text = perfil;
        }
        
        private void CargarCatPerfil(bool sistemas, bool usuario)
        {
            
            if (sistemas)
            {
                List<DataCatPerfil> catPerfil = GetCatPerfil();
                    inputPerfil.DataSource = catPerfil;
                    inputPerfil.DataValueField = "IdPerfil";
                    inputPerfil.DataTextField = "Perfil";
                    inputPerfil.DataBind();
            }
            else if(usuario)
            {

            List<DataCatPerfil> catPerfil = GetCatPerfilAtencionCiudadana();
                inputPerfil.DataSource = catPerfil;
                inputPerfil.DataValueField = "IdPerfil";
                inputPerfil.DataTextField = "Perfil";
                inputPerfil.DataBind();     
            }
            
        }
        private void CargarCatJuzgado(bool sistemas)
        {
            

            if (sistemas)
            {
                contenedorDrplstJuzgados.Visible = true;
                List<DataCatJuzgado> catJuzgado = GetCatJuzgado();
                inputJuzgado.DataSource = catJuzgado;
                inputJuzgado.DataValueField = "IdJuzgado";
                inputJuzgado.DataTextField = "Juzgado";
                inputJuzgado.DataBind();
            }
            else
            {
                contenedorDrplstJuzgados.Visible = false;
            }
        }
        protected void inputCatJuzgado_SelectedIndexChanged(object sender, EventArgs e)
        {
            string juzgado = inputJuzgado.SelectedItem.Text;
            copyJuzgado.Text = juzgado;
        }
        
        protected void btnEnviarUsuario_Click(object sender, EventArgs e)
        {
            
            try
            {
                string Nombre = inputNombre.Text;
                string APaterno = inputApellidoPaterno.Text;
                string AMaterno = inputApellidoMaterno.Text;
                string Usuario = inputUsuario.Text;
                string Contrasena = inputContraseña.Text;
                string contraseñaCifrada = CryptographyController.EncryptString(Contrasena);
                int Perfil = int.Parse(inputPerfil.SelectedValue);
                string TipoCircuito = Session["TCircuito"] != null ? Session["TCircuito"].ToString() : string.Empty;
                int Juzgado;
                 //si es de sistemas(superadmin) cambiara el juzgado
                if (TipoCircuito != "a" || TipoCircuito.IsEmpty()) Juzgado = int.Parse(Session["IDJuzgado"] != null ? Session["IDJuzgado"].ToString() : string.Empty);
                else Juzgado = int.Parse(inputJuzgado.SelectedValue);// sistemas (SUPER ADMINISTRADOR)                    
                
                
                string Telefono = inputTelefono.Text;
                string Domicilio = inputDomicilio.Text;
                string Email = inputEmail.Text;
                if(string.IsNullOrEmpty(Nombre) ||
                    string.IsNullOrEmpty(APaterno) ||
                    string.IsNullOrEmpty(AMaterno) ||
                    string.IsNullOrEmpty(Usuario) ||
                    string.IsNullOrEmpty(Contrasena) ||
                    string.IsNullOrEmpty(Telefono) ||
                    string.IsNullOrEmpty(Domicilio) ||
                    string.IsNullOrEmpty(Email) ||
                    string.IsNullOrEmpty(inputPerfil.SelectedValue) ||
                    (Juzgado <= 0)
                    )
                {
                    if(string.IsNullOrEmpty(Nombre))
                        throw new Exception("Nombre de usuario");
                    else if (string.IsNullOrEmpty(APaterno))
                        throw new Exception("Apellido paterno");
                    else if (string.IsNullOrEmpty(AMaterno))
                        throw new Exception("Apellido materno");
                    else if (string.IsNullOrEmpty(Usuario))
                        throw new Exception("Usuario de inicio se sesión");
                    else if (string.IsNullOrEmpty(Contrasena))
                        throw new Exception("Contraseña");
                    else if (string.IsNullOrEmpty(Telefono))
                        throw new Exception("Teléfono");
                    else if (string.IsNullOrEmpty(Domicilio))
                        throw new Exception("Domicilio");
                    else if (string.IsNullOrEmpty(Email))
                        throw new Exception("Correo electrónico");
                    else if (string.IsNullOrEmpty(inputPerfil.SelectedValue))
                        throw new Exception("Perfil");
                    else if ((Juzgado <= 0))
                        throw new Exception("Juzgado");

                }
                DataInsertUsuario infoUser = new DataInsertUsuario
                {
                    Nombre = Nombre,
                    APaterno = APaterno,
                    AMaterno = AMaterno,
                    Usuario = Usuario,
                    Contraseña = contraseñaCifrada,
                    IdPerfil = Perfil,
                    IdJuzgado = Juzgado,
                    Telefono = Telefono,
                    Domicilio = Domicilio,
                    Email = Email,
                    Status = "A",
                    Pass = "0"
                };
                List<DataInsertUsuario> listaInfoUser = new List<DataInsertUsuario> { infoUser };

                RespuestaInsertUsuario data = InsertUsuario(listaInfoUser);
                if (data.hayError)
                {
                    UpdatePanelPersonal.Update();
                    Debug.WriteLine("Error:" + data.mensaje);
                    throw new Exception(data.mensaje);
                }
                else
                {
                    MensajeExito(data.mensaje);
                    LimpiarCampos();
                    Debug.WriteLine("Error:" + data.mensaje);
                    UpdatePanelPersonal.Update();
                }
            }
            catch(Exception ex)
            {
                MensajeError($"Error: {ex.Message}");
            }
            
                
        }
        protected void MensajeExito(string Mensaje)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", $"toastInfo('{Mensaje}');", true);

        }
        protected void MensajeError(string Mensaje)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", $"toastError('{Mensaje}');", true);

        }
        protected void LimpiarCampos()
        {
            inputNombre.Text = "";
            inputApellidoPaterno.Text = "";
            inputApellidoMaterno.Text = "";
            inputUsuario.Text = "";
            inputContraseña.Text = "";

            inputPerfil.SelectedIndex = 0;
            inputJuzgado.SelectedIndex = 0;
            inputTelefono.Text = "";
            inputDomicilio.Text = "";
            inputEmail.Text = "";
        }
        
    }

}