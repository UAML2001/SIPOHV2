using DatabaseConnection;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;




namespace SIPOH.Views
{
    public partial class CustomRegistroIniciales : System.Web.UI.UserControl
    {
        //List<int> listaDeIdDelitos = new List<int>();
        List<int> listaDeNombresDelitos = new List<int>();
        


        public class Imputado
        {
            public string NombreCulpado { get; set; }
            public string APCulpado { get; set; }
            public string AMCulpado { get; set; }
            public string GeneroCulpado { get; set; }
            public string AliasCulpado { get; set; }
        }

        public class Victima
        {
            public string Nombre { get; set; }
            public string ApellidoPaterno { get; set; }
            public string ApellidoMaterno { get; set; }
            public string RazonSocial { get; set; }
            public string Genero { get; set; }
           
        }
        public class BusquedaInicial
        {
            public string DataImputado { get; set; }
            public string DataVictima { get; set; }
            public string DataNUC { get; set; }
        }

        public class Anexos
        {
            public string DescripcionAnexo { get; set; }
            public string CantidadAnexo { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, GetType(), "MostrarModal", "$('#miModal').modal('show');", true);
            
            if (!IsPostBack)
            {

                
                CargarCatDelitos();
                CargarCatAnexosEnDropDownList();
                Session["Victimas"] = new List<Victima>();                
                Session["Imputados"] = new List<Imputado>();
                Session["IdDelitos"] = new List<int>();
                Session["NombresDelitos"] = new List<string>();
                Session["Anexos"] = new List<Anexos>();



                CleanEtiquetaFormImputado();
                CleanEtiquetaFormAnexo();
                CleanEtiquetaFormDelito();
                CleanEtiquetaFormVictima();
                CleanEtiquetasForm();
                
               
            }


            //List<Victima> listaDeUsuarios = Session["Victimas"] as List<Victima> ?? new List<Victima>();
            //listaDeUsuarios.Clear();
            //List<BusquedaInicial> GetInicial = Session["Inicial"] as List<BusquedaInicial> ?? new List<BusquedaInicial>();


            // Llamando a la función GetInicial con la lista


        }
        public class InfoIniciales
        {
            
            public string NUC { get; set; }
            public string Inculpados { get; set; }
            public string Delitos { get; set; }
            public string Victimas { get; set; }
        }
        protected void btnFiltrarInicial(object sender, EventArgs e)
        {
            string VFiltradoPor = ddlTipoFiltrado.SelectedValue;
            string valorIncial = inputBuscarInicial.Text;

            BusquedaInicial busquedaInicial = new BusquedaInicial();

            switch (VFiltradoPor)
            {
                case "NUC":
                    busquedaInicial = new BusquedaInicial { DataNUC = valorIncial, DataImputado = "", DataVictima = ""};
                    break;
                case "V":
                    busquedaInicial = new BusquedaInicial { DataVictima = valorIncial, DataImputado = "" , DataNUC = "" };
                    break;
                case "I":
                    busquedaInicial = new BusquedaInicial { DataImputado = valorIncial, DataNUC = "",DataVictima = "" };
                    break;
                default:
                    string mensaje = "Selecciona un filtrado";
                    string script = $"toastError('{mensaje}');";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                    return;
            }

            DataTable resultados = RegistroIniciales.GetInicial(new List<BusquedaInicial> { busquedaInicial });

            if (resultados != null && resultados.Rows.Count > 0)
            {
                BusquedaIniciales.Visible = true;
                grdTblGetPromociones.DataSource = resultados;
                grdTblGetPromociones.DataBind();
                errorConsulta.Text = "Resultados encontrados";
            }
            else
            {
                BusquedaIniciales.Visible = false;
                errorConsulta.Text = "No se encontraron resultados";
            }

            // Limpiar etiquetas innecesarias o hacer cualquier otra manipulación necesaria
            

            updPanel.Update();
        }





        protected void inputQuienIngresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            string quienIngresa = inputQuienIngresa.SelectedValue;
            if (quienIngresa == "M")
            {
                quienIngresa = "MP";
            }else if (quienIngresa == "P")
            {
                quienIngresa = "particular";
            }else if (quienIngresa == "O")
            {
                quienIngresa = "otra persona";
            }else
            {
                quienIngresa = "";
            }
               
            lblTipoPersona.Text = quienIngresa;

            ScriptManager.RegisterStartupScript(this, GetType(), "mostrarValor", "mostrarValorSeleccionado();", true);
            updPanel.Update();
        }


        protected void inputTipoAsunto_SelectedIndexChanged(object sender, EventArgs e)
        {
            string valorSeleccionado = inputTipoAsunto.SelectedValue;

            // Limpia el DropDownList antes de agregar nuevas opciones
            inputRadicacion.Items.Clear();

            if (valorSeleccionado == "C" || valorSeleccionado == "CP")
            {
                var (solicitudes, ids) = RegistroIniciales.GetTipoSolicitud(valorSeleccionado);

                for (int i = 0; i < solicitudes.Count; i++)
                {
                    ListItem item = new ListItem(solicitudes[i], ids[i]);
                    inputRadicacion.Items.Add(item);
                    //Debug.WriteLine("solicitud: " + solicitudes[i], "IDS: " + ids[i]);
                }
            }

            // Actualiza el UpdatePanel después de realizar la operación
            updPanel.Update();
        }



        protected void btnGuardarVictima_Click(object sender, EventArgs e)
        {
            string nombreVictima = txtNombreVictima.Text;
            string apellidoMaternoVictima = txtAMVictima.Text;
            string apellidoPaternoVictima = txtAPVictima.Text;
            string generoVictima = ddlSexoVictima.SelectedValue;
            string razonSocialVictima = txtRazonSocialVictima.Text;

            // Validar que todos los campos estén llenos

            // Validar que la víctima no se repita en la lista
            List<Victima> listaDeUsuarios = Session["Victimas"] as List<Victima> ?? new List<Victima>();
            if (listaDeUsuarios.Any(v => string.Equals(v.Nombre, nombreVictima, StringComparison.OrdinalIgnoreCase) &&
                                           string.Equals(v.ApellidoMaterno, apellidoMaternoVictima, StringComparison.OrdinalIgnoreCase) &&
                                           string.Equals(v.ApellidoPaterno, apellidoPaternoVictima, StringComparison.OrdinalIgnoreCase) &&
                                           string.Equals(v.ApellidoPaterno, razonSocialVictima, StringComparison.OrdinalIgnoreCase)))
            {
                // Mostrar mensaje de error o lanzar una excepción según tus necesidades

                string mensaje = "Esta víctima ya existe en la lista.";
                string script = $"toastError('{mensaje}');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                return;
            }

            if (ddlPersonaVictima.SelectedValue == "fisica")
            {
                if (string.IsNullOrWhiteSpace(nombreVictima) ||
                    string.IsNullOrWhiteSpace(apellidoMaternoVictima) ||
                    string.IsNullOrWhiteSpace(apellidoPaternoVictima) ||
                    string.IsNullOrWhiteSpace(generoVictima))
                {
                    

                    string mensaje = "Todos los campos de la víctima son obligatorios.";
                    string script = $"toastError('{mensaje}');";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                    return;
                }
                Victima nuevoUsuario = new Victima
                {
                    Nombre = nombreVictima,
                    ApellidoMaterno = apellidoMaternoVictima,
                    ApellidoPaterno = apellidoPaternoVictima,
                    Genero = generoVictima,
                };
                // Agregar el nuevo usuario a la lista
                listaDeUsuarios.Add(nuevoUsuario);

                // Guardar la lista actualizada en la sesión
                Session["Victimas"] = listaDeUsuarios;

                // Actualizar el Repeater con la nueva lista de víctimas
                Repeater1.DataSource = listaDeUsuarios;
                Repeater1.DataBind();

                // Limpiar etiquetas y actualizar el panel
                updPanel.Update();
                CleanEtiquetaFormVictima();
            }
            else if (ddlPersonaVictima.SelectedValue == "moral")
            {
                if (string.IsNullOrWhiteSpace(razonSocialVictima))
                {
                    string mensaje = "El campo de razón social debe estar lleno.";
                    string script = $"toastError('{mensaje}');";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                    return;
                }

                Victima nuevoUsuario = new Victima
                {
                    Nombre = "",
                    ApellidoMaterno = ".",
                    ApellidoPaterno = razonSocialVictima,
                    Genero = "O",
                };
                // Agregar el nuevo usuario a la lista
                listaDeUsuarios.Add(nuevoUsuario);

                // Guardar la lista actualizada en la sesión
                Session["Victimas"] = listaDeUsuarios;

                // Actualizar el Repeater con la nueva lista de víctimas
                Repeater1.DataSource = listaDeUsuarios;
                Repeater1.DataBind();

                // Limpiar etiquetas y actualizar el panel
                CleanEtiquetaFormVictima();
                updPanel.Update();
            }
            else
            {
                string mensaje = "Selecciona el tipo de persona.";
                string script = $"toastError('{mensaje}');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
            }
        }

        protected void btnGuardarImputado_Click(object sender, EventArgs e)
        {
            string inputNombreImputado = txtNombreImputado.Text;
            string inputAPaternoImputado = txtAPaternoImputado.Text;
            string inputAMaternoImputado = txtAMaternoImputado.Text;
            string inputGeneroImputado = txtGeneroImputado.SelectedValue;
            string inputAliasImputado = txtAliasImputado.Text;

            if (string.IsNullOrWhiteSpace(inputNombreImputado) ||
                string.IsNullOrWhiteSpace(inputAPaternoImputado) ||
                string.IsNullOrWhiteSpace(inputAMaternoImputado) ||
                string.IsNullOrWhiteSpace(inputGeneroImputado))
            {
                // Mostrar mensaje de error o lanzar una excepción según tus necesidades
                // Por ejemplo, puedes mostrar un mensaje y no continuar con el proceso

                string mensaje = "Todos los campos son obligatorios.";
                string script = $"toastError('{mensaje}');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                return;
            }

            List<Imputado> listaDeImputados = Session["Imputados"] as List<Imputado> ?? new List<Imputado>();

            if (listaDeImputados.Any(v => string.Equals(v.NombreCulpado, inputNombreImputado, StringComparison.OrdinalIgnoreCase) &&
                                           string.Equals(v.APCulpado, inputAPaternoImputado, StringComparison.OrdinalIgnoreCase) &&
                                           string.Equals(v.AMCulpado, inputAMaternoImputado, StringComparison.OrdinalIgnoreCase) &&
                                           string.Equals(v.AliasCulpado, inputAliasImputado, StringComparison.OrdinalIgnoreCase)))
            {
                // Mostrar mensaje de error o lanzar una excepción según tus necesidades

                string mensaje = "Este imputado ya existe en la lista.";
                string script = $"toastError('{mensaje}');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                return;
            }

            Imputado culpado = new Imputado
            {
                NombreCulpado = inputNombreImputado,
                APCulpado = inputAPaternoImputado,
                AMCulpado = inputAMaternoImputado,
                GeneroCulpado = inputGeneroImputado,
                AliasCulpado = inputAliasImputado
            };

            listaDeImputados.Add(culpado);
            Session["Imputados"] = listaDeImputados;

            Repeater2.DataSource = listaDeImputados;
            Repeater2.DataBind();

            CleanEtiquetaFormImputado();
            updPanel.Update();
        }


        protected void btnGuardarAnexos_Click(object sender, EventArgs e)
        {
            string inputTipoAnexo = txtAnexosTipo.SelectedValue;
                string inputCantidadAnexo = txtCantidadAnexos.Text;
            if (inputTipoAnexo == "Otro")
            {
                string inputDescripcionAnexo = txtDescripcionAnexos.Text;
                

                Anexos anexo = new Anexos
                {
                    //TipoAnexo = inputTipoAnexo,
                    DescripcionAnexo = inputDescripcionAnexo,
                    CantidadAnexo = inputCantidadAnexo

                };
                List<Anexos> listaDeAnexos = Session["Anexos"] as List<Anexos> ?? new List<Anexos>();

                if (listaDeAnexos.Any(v => v.DescripcionAnexo == inputDescripcionAnexo))
                {
                    // Mostrar mensaje de error o lanzar una excepción según tus necesidades

                    string mensaje = "Este anexo ya existe en la lista.";
                    string script = $"toastError('{mensaje}');";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                    return;
                }

                listaDeAnexos.Add(anexo);
                Session["Anexos"] = listaDeAnexos;
                Repeater3.DataSource = listaDeAnexos;
                Repeater3.DataBind();
                CleanEtiquetaFormAnexo();
                updPanel.Update();  
            }
            else
            {
                if (string.IsNullOrWhiteSpace(inputTipoAnexo) ||
                    string.IsNullOrWhiteSpace(inputCantidadAnexo))
                {
                    // Mostrar mensaje toast de error o lanzar una excepción según tus necesidades
                    // Por ejemplo, puedes mostrar un mensaje y no continuar con el proceso

                    string mensaje = "Ingresa la cantidad de anexos.";
                    string script = $"toastError('{mensaje}');";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                    return;
                }
                Anexos anexo = new Anexos
                {
                    //TipoAnexo = inputTipoAnexo,
                    DescripcionAnexo = inputTipoAnexo,
                    CantidadAnexo = inputCantidadAnexo

                };
                List<Anexos> listaDeAnexos = Session["Anexos"] as List<Anexos> ?? new List<Anexos>();

                if (listaDeAnexos.Any(v => v.DescripcionAnexo == inputTipoAnexo))
                {
                    // Mostrar mensaje de error o lanzar una excepción según tus necesidades

                    string mensaje = "Este anexo ya existe en la lista.";
                    string script = $"toastError('{mensaje}');";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                    return;
                }

                listaDeAnexos.Add(anexo);
                Session["Anexos"] = listaDeAnexos;
                Repeater3.DataSource = listaDeAnexos;
                Repeater3.DataBind();
                CleanEtiquetaFormAnexo();
                updPanel.Update();
            }
                
            
        }


        protected void btnEnviarInicial_Click(object sender, EventArgs e)
        {
            
            string TipoAsunto = inputTipoAsunto.SelectedValue;
            string TipoRadicacion = inpuTipoRadicacion.SelectedValue;
            string Observaciones = inputObservaciones.Text;
            string QuienIngresa = inputQuienIngresa.SelectedValue;
            string MP = inputNombreParticular.Text;
            string Prioridad = inputPrioridad.SelectedValue;
            //Prioridad 
            string Fojas = inputNumeroFojas.Text;
            string IdAudiencia = inputRadicacion.SelectedValue;
            string FeIngreso= inputFechaRecepcion.Text;
            string NUC = inputNUC.Text;
            //RegistroIniciales.GetNumeroIniciales("cp");
            if (string.IsNullOrWhiteSpace(TipoAsunto) || string.IsNullOrWhiteSpace(TipoRadicacion) || 
                string.IsNullOrWhiteSpace(QuienIngresa) ||
       string.IsNullOrWhiteSpace(MP) || string.IsNullOrWhiteSpace(Prioridad) ||
       string.IsNullOrWhiteSpace(Fojas) || string.IsNullOrWhiteSpace(IdAudiencia) ||
       string.IsNullOrWhiteSpace(FeIngreso) || string.IsNullOrWhiteSpace(NUC))
            {
                // Mostrar un mensaje al usuario indicando que debe llenar todos los campos
                // Puedes usar un control como Label para mostrar el mensaje en tu interfaz
                string mensaje = "Datos no estan correctamente insertados";
                string script = $"toastError('{mensaje}');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                
                Debug.WriteLine("Datos no estan correctamnte insertados");



            return;  // Salir del método si no se llenaron todos los campos
            }
            try
            { 


                List<Victima> listaDeUsuarios = Session["Victimas"] as List<Victima> ?? new List<Victima>();
                List<Imputado> listaDeImputados = Session["Imputados"] as List<Imputado> ?? new List<Imputado>();
                //List<int> idDelitos = (List<int>)Session["IdDelitos"];
                List<int> idDelitos = Session["IdDelitos"] as List <int> ?? new List<int>();
                List<Anexos> listaDeAnexos = Session["Anexos"] as List<Anexos> ?? new List<Anexos>();
                // Puedes usar la listaDeUsuarios según sea necesario en tu otra función
                foreach (var usuario in listaDeUsuarios)
                {
                    
                    //Debug.WriteLine($"Nombre: {usuario.Nombre}, Apellido Paterno: {usuario.ApellidoPaterno}, Apellido Materno: {usuario.ApellidoMaterno}, Razon Social: {usuario.RazonSocial}, Genero: {usuario.Genero}");
                }
                if (listaDeUsuarios.Count == 0 || listaDeImputados.Count == 0 || idDelitos.Count == 0)
                {
                    throw new InvalidOperationException("Las listas no pueden estar vacías.");
                    
                }
                    RegistroIniciales.SendRegistroIniciales(FeIngreso, TipoAsunto, IdAudiencia, Observaciones, QuienIngresa, MP, Prioridad, Fojas,TipoRadicacion, NUC, idDelitos, listaDeUsuarios, listaDeImputados, listaDeAnexos);
               
                //listaDeIdDelitos.Clear();
                listaDeUsuarios.Clear();
                
                listaDeImputados.Clear();
                listaDeAnexos.Clear();
                // método para realizar las inserciones
                
                string mensaje = "Tu peticion fue correcta!, tu Registro se ha hecho correctamente. ";
                
                string scriptToast = $"toastInfo('{mensaje}');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "toastInfoScript", scriptToast, true);
                
                //List<Victima> listaDeUsuarios = new List<Victima>();
            }
            catch (Exception ex)
            {
                //  error genérico al usuario
                Debug.WriteLine("Problemas en la consulta: " + ex);
                //lblError.Text = "Sucedio un error con su consulta, tu registro es incompleto o ya se ha generado anteriormente";
                
                string mensaje = "Sucedio un error en su consulta, verifica si es correcta! ";
                string script = $"toastError('{mensaje}');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
            }

            //Debug.WriteLine("Tipo Asunto "+ TipoAsunto+"Valor seleccionado: " + IdAudienciaSelect + "fechas" + FeIngresoSelect+ "observaciones: " + Observaciones+ "Radicacion: "+ TipoRadicacion + "Quien ingresa: " + QuienIngresa + "MPName :" + MP + "NUM FOJAS: " + Fojas + "Prioridad: "+ Prioridad );

           
            
            updPanel.Update();
            CleanTablas();
            CleanEtiquetaFormImputado();
            CleanEtiquetaFormAnexo();
            CleanEtiquetaFormDelito();
            CleanEtiquetaFormVictima();
            CleanEtiquetasForm();
            Session["Victimas"] = new List<Victima>();
            Session["Imputados"] = new List<Imputado>();
            Session["IdDelitos"] = new List<int>();
            Session["NombresDelitos"] = new List<string>();
            Session["Anexos"] = new List<Anexos>();

        }



        
        protected void btnEnviarDelito_Click(object sender, EventArgs e)
        {
            // Intentar convertir el valor seleccionado a un entero
            if (int.TryParse(inputDelitos.SelectedValue, out int idDelitoSeleccionado))
            {
                // Obtener la lista actual de la sesión o crear una nueva si aún no existe
                List<int> listaDeIdDelitos = Session["IdDelitos"] as List<int> ?? new List<int>();
                List<string> listaDeNombresDelitos = Session["NombresDelitos"] as List<string> ?? new List<string>();
               
                if (!listaDeIdDelitos.Contains(idDelitoSeleccionado))
                {
                    // Agregar a las listas solo si la conversión fue exitosa
                    listaDeIdDelitos.Add(idDelitoSeleccionado);

                    // Obtener el nombre del delito seleccionado
                    string nombreDelitoSeleccionado = ObtenerNombreDelito(idDelitoSeleccionado);
                    listaDeNombresDelitos.Add(nombreDelitoSeleccionado);

                    // Actualizar las listas en la sesión
                    Session["IdDelitos"] = listaDeIdDelitos;
                    Session["NombresDelitos"] = listaDeNombresDelitos;

                    // Otro código que necesites hacer después de guardar


                    StringBuilder tablaHtml = new StringBuilder();


                    // Iterar sobre las listas y agregar filas a la tabla
                    for (int i = 0; i < listaDeIdDelitos.Count; i++)
                    {
                        tablaHtml.AppendLine("<tr>");
                        tablaHtml.AppendLine($"<th scope=\"row\">{listaDeIdDelitos[i]}</th>");
                        tablaHtml.AppendLine($"<td class=\"text-secondary \">{listaDeNombresDelitos[i]}</td>");
                        tablaHtml.AppendLine($"<td class=\"text-secondary\"><i class=\"bi bi-trash-fill text-danger\"></i></td>");
                        tablaHtml.AppendLine("</tr>");
                    }



                    // Asignar la cadena HTML a un control en tu página (puede ser un LiteralControl o un literal en tu página)
                    litTablaDelitos.Text = tablaHtml.ToString();
                    //lblIdDelitos.Text = "";
                    
                }
                else
                {
                    // Mostrar un mensaje si el idDelito ya está en la lista
                    
                    string mensaje = "El delito ya ha sido seleccionado! ";
                    string script = $"toastError('{mensaje}');";
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                }

            }
            else
            {
                // Manejar el caso en el que la conversión falla
                // Puedes mostrar un mensaje de error o realizar alguna acción específica.
                // Por ejemplo:
                
                string mensaje = "No tiene delitos seleccionados! ";
                string script = $"toastError('{mensaje}');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);

            }

            // Actualizar el contenido del UpdatePanel
            CleanEtiquetaFormDelito();
            
            updPanel.Update();
        }
        protected void LimpiarTabla()
        {
            // Limpiar el contenido de la tabla
            StringBuilder tablaHtml = new StringBuilder();
            litTablaDelitos.Text = tablaHtml.ToString();

            // También puedes limpiar las listas en la sesión si es necesario
            Session["IdDelitos"] = new List<int>();
            Session["NombresDelitos"] = new List<string>();
        }




        // Método para obtener el nombre del delito por su ID
        private string ObtenerNombreDelito(int idDelito)
        {
            (List<string> catDelitos, List<string> catIdsDelitos) = RegistroIniciales.GetCatDelitos();

            int index = catIdsDelitos.IndexOf(idDelito.ToString());

            return index != -1 ? catDelitos[index] : "Nombre no encontrado";
        }









        private void CargarCatAnexosEnDropDownList()
        {
            // Llama a tu método para obtener la lista de elementos
            List<string> catAnexos = RegistroIniciales.GetCatAnexos();

            // Asigna la lista de elementos al DropDownList
            txtAnexosTipo.DataSource = catAnexos;
            txtAnexosTipo.DataBind();
        }
        private void CargarCatDelitos()
        {
            (List<string> catDelitos, List<string> catIdsDelitos) = RegistroIniciales.GetCatDelitos();

            // Crear una lista de ListItem
            List<ListItem> items = new List<ListItem>();

            for (int i = 0; i < catDelitos.Count; i++)
            {
                // Crear ListItem con el texto y el valor correspondientes
                ListItem item = new ListItem(catDelitos[i], catIdsDelitos[i]);
                items.Add(item);
                
            }

            // Asignar la lista de ListItem al DropDownList
            inputDelitos.Items.AddRange(items.ToArray());
        }




        private void CleanEtiquetaFormImputado()
        {
            txtNombreImputado.Text = "";
            txtAPaternoImputado.Text = "";
            txtAMaternoImputado.Text = "";
            txtGeneroImputado.SelectedIndex = 0;
            txtAliasImputado.Text = "";

        }
        private void CleanEtiquetaFormAnexo()
        {
            txtDescripcionAnexos.Text = "";
            txtCantidadAnexos.Text = "";
            txtAnexosTipo.SelectedIndex = 0;


        }

        private void CleanEtiquetaFormDelito()
        {
            inputDelitos.SelectedIndex = 0;
        }
        private void CleanEtiquetaFormVictima()
        {
            ddlPersonaVictima.SelectedIndex = 0;
            txtNombreVictima.Text = "";
            txtAPVictima.Text = "";
            txtAMVictima.Text = "";
            txtRazonSocialVictima.Text = "";
            ddlSexoVictima.SelectedIndex = 0;

        }
        private void CleanEtiquetasForm()
        {

            inputTipoAsunto.SelectedIndex = 0;
            inpuTipoRadicacion.SelectedIndex = 0;
            inputObservaciones.Text = "";
            inputQuienIngresa.SelectedIndex = 0;
            inputNombreParticular.Text = "";
            inputPrioridad.SelectedIndex = 0;
            inputNumeroFojas.Text = "";
            //inputRadicacion.SelectedValue = "";
            inputFechaRecepcion.Text = "";
            inputNUC.Text = "";
        }
        private void CleanTablas()
        {
            Repeater1.DataSource = null;
            Repeater1.DataBind();
            Repeater2.DataSource = null;
            Repeater2.DataBind();
            Repeater3.DataSource = null;
            Repeater3.DataBind();
            LimpiarTabla();
        }


    }



}