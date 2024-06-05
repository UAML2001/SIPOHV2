using DatabaseConnection;
using Microsoft.Ajax.Utilities;
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
            public string Digitalizado { get; set;}
        }
        public class CatDelito
        {
            public string IdDelito { get; set; }
            public string DescripcionDelito { get; set; }


        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, GetType(), "MostrarModal", "$('#miModal').modal('show');", true);

            if (!IsPostBack)
            {
                inputBuscarInicial.Text = "";
                ddlTipoFiltrado.SelectedIndex = 0;
                CargarCatDelitos();
                CargarCatAnexosEnDropDownList();
                Session["Victimas"] = new List<Victima>();
                Session["Imputados"] = new List<Imputado>();
                Session["Delitos"] = new List<int>();
                //Session["NombresDelitos"] = new List<string>();
                Session["Anexos"] = new List<Anexos>();

                CleanEtiquetasForm();
                CleanEtiquetaFormImputado();
                CleanEtiquetaFormAnexo();
                CleanEtiquetaFormDelito();
                CleanEtiquetaFormVictima();
                
            }

            

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
                    busquedaInicial = new BusquedaInicial { DataNUC = valorIncial, DataImputado = "", DataVictima = "" };
                    break;
                case "V":
                    busquedaInicial = new BusquedaInicial { DataVictima = valorIncial, DataImputado = "", DataNUC = "" };
                    break;
                case "I":
                    busquedaInicial = new BusquedaInicial { DataImputado = valorIncial, DataNUC = "", DataVictima = "" };
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
                //errorConsulta.Text = "Resultados encontrados";
               
                string mensaje = "Resultados fueron encontrados.";
                string script = $"toastInfo('{mensaje}');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);

            }
            else
            {
                BusquedaIniciales.Visible = false;
                //errorConsulta.Text = "No se encontraron resultados";
                string mensaje = "No se encontraron resultados en tu busqueda.";
                MostrarMensajeError(mensaje);
                
            }
            ddlTipoFiltrado.SelectedIndex = 0;
            inputBuscarInicial.Text = "";

            updPanel.Update();

        }
        protected void btnEliminarAnexo(object sender, EventArgs e)
        {
            // Obtener el botón que activó el evento
            Button btnEliminar = (Button)sender;

            // Obtener la fila del Repeater que contiene el botón
            RepeaterItem item = (RepeaterItem)btnEliminar.NamingContainer;
            //item.Visible = false;
            // Obtener el índice de la fila en el Repeater
            int indice = item.ItemIndex;

            // Obtener la lista de la sesión
            List<Anexos> listaAnexos = (List<Anexos>)Session["Anexos"];

            // Verificar si la lista no es nula y tiene elementos
            if (listaAnexos != null && listaAnexos.Count > indice)
            {
                // Eliminar el elemento en la posición indicada por el índice
                //item.Visible = false;
                listaAnexos.RemoveAt(indice);
                //Repeater3.Controls.RemoveAt(indice);
                //Session["Anexos"] = listaAnexos;
                Repeater3.DataSource = listaAnexos;
                Repeater3.DataBind();


            }
            // Verificar si la lista no es nula y tiene elementos
            updPanel.Update();

        }

        protected void btnEliminarVictima(object sender, EventArgs e)
        {
            // Obtener el botón que activó el evento
            Button btnEliminarVictima = (Button)sender;

            // Obtener la fila del Repeater que contiene el botón
            RepeaterItem item = (RepeaterItem)btnEliminarVictima.NamingContainer;
            //item.Visible = false;
            // Obtener el índice de la fila en el Repeater
            int indiceListVictima = item.ItemIndex;

            // Obtener la lista de la sesión
            List<Victima> listaVictima = (List<Victima>)Session["Victimas"];

            // Verificar si la lista no es nula y tiene elementos
            if (listaVictima != null && listaVictima.Count > indiceListVictima)
            {
                // Eliminar el elemento en la posición indicada por el índice
                //item.Visible = false;
                listaVictima.RemoveAt(indiceListVictima);
                //Repeater1.Controls.RemoveAt(indiceListVictima);
                //Session["Victimas"] = listaVictima;
                Repeater1.DataSource = listaVictima;
                Repeater1.DataBind();


            }
            // Verificar si la lista no es nula y tiene elementos
            updPanel.Update();

        }
        protected void btnEliminarCulpado(object sender, EventArgs e)
        {
            // Obtener el botón que activó el evento
            Button btnEliminarCulpados = (Button)sender;

            // Obtener la fila del Repeater que contiene el botón
            RepeaterItem item = (RepeaterItem)btnEliminarCulpados.NamingContainer;
            //item.Visible = false;
            // Obtener el índice de la fila en el Repeater
            int indice = item.ItemIndex;

            // Obtener la lista de la sesión
            List<Imputado> listaImputados = (List<Imputado>)Session["Imputados"];

            // Verificar si la lista no es nula y tiene elementos
            if (listaImputados != null && listaImputados.Count > indice)
            {
                // Eliminar el elemento en la posición indicada por el índice
                //item.Visible = false;
                listaImputados.RemoveAt(indice);
                //Repeater2.Controls.RemoveAt(indice);
                //Session["Imputados"] = listaImputados;
                Repeater2.DataSource = listaImputados;
                Repeater2.DataBind();


            }
            // Verificar si la lista no es nula y tiene elementos
            updPanel.Update();

        }



        protected void inputQuienIngresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            string quienIngresa = inputQuienIngresa.SelectedValue;
            if (quienIngresa == "M")
            {
                quienIngresa = "MP";
            }
            else if (quienIngresa == "P")
            {
                quienIngresa = "particular";
            }
            else if (quienIngresa == "O")
            {
                quienIngresa = "otra persona";
            }
            else
            {
                quienIngresa = "";
            }

            lblTipoPersona.Text = quienIngresa;

            ScriptManager.RegisterStartupScript(this, GetType(), "mostrarValor", "mostrarValorSeleccionado();", true);
            updPanel.Update();
        }


        protected void inputTipoAsunto_SelectedIndexChanged(object sender, EventArgs e)
        {
            TicketDiv.Style["display"] = "none";
            string valorSeleccionado = inputTipoDocumento.SelectedValue;
            // Limpia el DropDownList antes de agregar nuevas opciones
            inputTipoSolicitud.Items.Clear();
            ListItem staticItem = new ListItem("Selecciona una opción", "0");
            inputTipoSolicitud.Items.Add(staticItem);

            if (valorSeleccionado == "C" || valorSeleccionado == "CP")
            {
                var (solicitudes, ids) = RegistroIniciales.GetTipoSolicitud(valorSeleccionado);

                for (int i = 0; i < solicitudes.Count; i++)
                {
                    ListItem item = new ListItem(solicitudes[i], ids[i]);
                    inputTipoSolicitud.Items.Add(item);
                    
                }
            }

            // Actualiza el UpdatePanel después de realizar la operación
            updPanel.Update();
        }
        protected void LimpiarRadioButtonList(RadioButtonList radioButtonList)
        {
            foreach (ListItem item in radioButtonList.Items)
            {
                item.Selected = false;
            }
        }
        protected void inputRadicacion_SelectedIndexChanged(object sender, EventArgs e)
        {

            string valorSeleccionado = inputTipoSolicitud.SelectedItem.Text;
            copyDropDownTipoSolicitud.Text = valorSeleccionado.ToUpper();                        
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

            if (ddlPersonaVictima.SelectedValue == "F")
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
            else if (ddlPersonaVictima.SelectedValue == "M")
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
                    ApellidoMaterno = "",
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
        protected void GetLabelPrioridad(object sender, EventArgs e)
        {
            string valorSeleccionado = inputPrioridad.SelectedValue;

            if (valorSeleccionado == "N")
            {
                valorSeleccionado = "Normal";
                copyPrioridad.Text = valorSeleccionado;

            }
            else if (valorSeleccionado == "A")
            {
                valorSeleccionado = "Alta";
                copyPrioridad.Text = valorSeleccionado;

            }

            updPanel.Update();

        }


        protected void btnGuardarAnexos_Click(object sender, EventArgs e)
        {
            //probar anexos digitalizados dependiendo del valor de session 
            string inputTipoAnexo = txtAnexosTipo.SelectedValue;
            string inputCantidadAnexo = txtCantidadAnexos.Text;
                string  Digitalizado;
            if (Session["IdSolicitudBuzon"] != null)
            {
                Digitalizado = "S";
            }
            else
            {
                Digitalizado = "N";
            }
                if (txtCantidadAnexos.Text == "0")
            {

                string script = $"toastError('{"Por favor, selecciona una cantidad válida."}');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                return;
            }
            if (inputTipoAnexo == "Otro")
            {
                string inputDescripcionAnexo = txtDescripcionAnexos.Text;

                Anexos anexo = new Anexos
                {
                    //TipoAnexo = inputTipoAnexo,
                    DescripcionAnexo = inputDescripcionAnexo.ToUpper(),
                    CantidadAnexo = inputCantidadAnexo,
                    Digitalizado = Digitalizado

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
                Session["AnexosT"] = listaDeAnexos;
                Debug.WriteLine("Anexos para ticket: " + Session["AnexosT"]);
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
                    CantidadAnexo = inputCantidadAnexo,
                    Digitalizado= Digitalizado

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
                //Session["AnexosT"] = Session["Anexos"];

                Repeater3.DataSource = listaDeAnexos;
                Repeater3.DataBind();
                CleanEtiquetaFormAnexo();
                updPanel.Update();
            }


        }


        protected void btnEnviarInicial_Click(object sender, EventArgs e)
        {
            try
            {
                string[] camposFaltantes;
                if (CamposIncompletos(out camposFaltantes))
                {
                    string camposMensaje = string.Join(", ", camposFaltantes.Where(campo => !string.IsNullOrWhiteSpace(campo)));
                    string mensaje = $"Los siguientes campos son obligatorios: {camposMensaje}";
                    MostrarMensajeError(mensaje);
                    return;
                }

                ProcesarDatos();

            }
            catch (Exception ex)
            {

                MostrarMensajeError("Sucedió un error en su consulta, verifica si es correcta.");
                Debug.WriteLine("Problemas en la consulta: " + ex);
            }
        }


        private bool CamposIncompletos(out string[] camposFaltantes)
        {
            string TipoAsunto = inputTipoDocumento.SelectedValue;
            string TipoRadicacion = inpuTipoRadicacion.SelectedValue;
            string Observaciones = inputObservaciones.Text;
            string QuienIngresa = inputQuienIngresa.SelectedValue;
            string MP = inputNombreParticular.Text;
            string Prioridad = inputPrioridad.SelectedValue;
            string Fojas = inputNumeroFojas.Text;
            string IdAudiencia = inputTipoSolicitud.SelectedValue;
            
            DateTime FeIngreso = DateTime.Parse(inputFechaRecepcion.Text);
            string NUC = inputNUC.Text;

            camposFaltantes = new string[]
            {
        string.IsNullOrWhiteSpace(TipoAsunto) ? "Tipo de Asunto" : "",
        string.IsNullOrWhiteSpace(TipoRadicacion) ? "Tipo de Radicación" : "",
        string.IsNullOrWhiteSpace(QuienIngresa) ? "Quién Ingresa" : "",
        string.IsNullOrWhiteSpace(MP) ? "Nombre Particular" : "",
        string.IsNullOrWhiteSpace(Prioridad) ? "Prioridad" : "",
        string.IsNullOrWhiteSpace(Fojas) ? "Número de Fojas" : "",
        (IdAudiencia == "0") ? "Tipo de solicitud" : "",
        FeIngreso == DateTime.MinValue ? "Fecha de Ingreso" : "",
            string.IsNullOrWhiteSpace(NUC) ? "NUC" : "",
            };

            return camposFaltantes.Any(campo => !string.IsNullOrWhiteSpace(campo));
        }


        private void MostrarMensajeError(string mensaje)
        {

            string script = $"toastError('{mensaje}');";
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);

        }
        private void ProcesarDatos()
        {
            try
            {
                string tipoAsunto = inputTipoDocumento.SelectedValue;
                string tipoRadicacion = inpuTipoRadicacion.SelectedValue;
                string observaciones = inputObservaciones.Text;
                string quienIngresa = inputQuienIngresa.SelectedValue;
                string mp = inputNombreParticular.Text;
                string prioridad = inputPrioridad.SelectedValue;
                DateTime fechaActual = DateTime.Now;

                List<Victima> listaDeUsuarios = ObtenerListaDeUsuarios();
                List<Imputado> listaDeImputados = ObtenerListaDeImputados();
                List<CatDelito> listaDeDelitos = ObtenerIdDelitos();
                List<Anexos> listaDeAnexos = ObtenerListaDeAnexos();

                if (listaDeUsuarios.Count == 0 || listaDeImputados.Count == 0 || listaDeDelitos.Count == 0)
                {
                    MostrarMensajeError("La tabla de víctima, imputado y delitos no pueden estar vacías.");
                    return;
                }

                bool transaccionExitosa;
                int actividad;
                string mensaje;
                string scriptToast;
                string Digitalizado;
                if (Session["IdSolicitudBuzon"] != null)
                {
                    string estatus = "A";
                    actividad = 3;
                    Digitalizado = "S";
                    int idSolicitudBuzon = int.Parse(Session["IdSolicitudBuzon"].ToString());
                    transaccionExitosa = RegistroIniciales.SendRegistroIniciales(fechaActual, actividad, DateTime.Parse(inputFechaRecepcion.Text), tipoAsunto, Digitalizado, inputTipoSolicitud.SelectedValue, observaciones, quienIngresa, mp, prioridad, inputNumeroFojas.Text, tipoRadicacion, inputNUC.Text, listaDeDelitos, listaDeUsuarios, listaDeImputados, listaDeAnexos);
                    bool result = RegistroIniciales.UpdateBuzonSalida(idSolicitudBuzon, fechaActual, estatus);

                    mensaje = result ? "Se actualizó correctamente la solicitud de buzón." : "Problemas al actualizar el buzón de salida.";
                    scriptToast = result ? $"toastInfo('{mensaje}');" : $"toastError('{mensaje}');";
                }
                else
                {
                    actividad = 1;
                    Digitalizado = "N";
                    transaccionExitosa = RegistroIniciales.SendRegistroIniciales(fechaActual, actividad, DateTime.Parse(inputFechaRecepcion.Text), tipoAsunto, Digitalizado, inputTipoSolicitud.SelectedValue, observaciones, quienIngresa, mp, prioridad, inputNumeroFojas.Text, tipoRadicacion, inputNUC.Text, listaDeDelitos, listaDeUsuarios, listaDeImputados, listaDeAnexos);
                    mensaje = transaccionExitosa ? "Envío exitoso. Tu registro se ha hecho correctamente." : "¡Ocurrió un error en la transacción! El folio ha sido asignado, consulta a soporte.";
                    scriptToast = transaccionExitosa ? $"toastInfo('{mensaje}');" : $"toastError('{mensaje}');";
                }

                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "toastScript", scriptToast, true);

                if (transaccionExitosa)
                {
                    string ticket = CrearTicketSELLO();
                    TicketDiv.InnerHtml = ticket.Replace(Environment.NewLine, "<br>");
                    ScriptManager.RegisterStartupScript(this, GetType(), "ImprimirScript", "imprimirTicketIniciales();", true);
                    tituloSelloIniciales.Style["display"] = "block";
                    ScriptManager.RegisterStartupScript(this, GetType(), "mostrarTituloSello", "mostrarTituloSello();", true);

                    Session.Remove("IdSolicitudBuzon");
                    LimpiarDatosDespuesDeProcesar();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error en la página ASPX: " + ex.Message);
                MostrarMensajeError("Ocurrió un error inesperado. Por favor, inténtalo de nuevo.");
            }
            finally
            {
                inputTipoSolicitud.Items.Clear();
            }
        }

       

        // Definir funciones para obtener datos
        private List<Victima> ObtenerListaDeUsuarios()
        {
            return Session["Victimas"] as List<Victima> ?? new List<Victima>();
        }

        private List<Imputado> ObtenerListaDeImputados()
        {
            return Session["Imputados"] as List<Imputado> ?? new List<Imputado>();
        }

        private List<CatDelito> ObtenerIdDelitos()
        {
            return Session["Delitos"] as List<CatDelito> ?? new List<CatDelito>();
        }

        private List<Anexos> ObtenerListaDeAnexos()
        {
            return Session["Anexos"] as List<Anexos> ?? new List<Anexos>();
        }

        private void LimpiarDatosDespuesDeProcesar()
        {
            updPanel.Update();
            CleanTablas();
            CleanEtiquetaFormImputado();
            CleanEtiquetaFormAnexo();
            CleanEtiquetaFormDelito();
            CleanEtiquetaFormVictima();
            CleanEtiquetasForm();
            LimpiarRadioButtonList(inputPrioridad);
            Session["Victimas"] = new List<Victima>();
            Session["Imputados"] = new List<Imputado>();
            Session["Delitos"] = new List<int>();

            Session["Anexos"] = new List<Anexos>();
        }




        private List<string> DividirTextoEnLineas(string texto, int maxCaracteresPorLinea)
        {
            List<string> lineas = new List<string>();
            string[] palabras = texto.Split(' ');
            string lineaActual = "";

            foreach (string palabra in palabras)
            {
                if ((lineaActual.Length > 0) && (lineaActual.Length + palabra.Length + 1 > maxCaracteresPorLinea))
                {
                    lineas.Add(lineaActual);
                    lineaActual = "";
                }

                if (lineaActual.Length > 0)
                    lineaActual += " ";

                lineaActual += palabra;
            }

            if (lineaActual.Length > 0)
                lineas.Add(lineaActual);

            return lineas;
        }

        public static List<string> listaDeAnexos = new List<string>();
        private void ImprimirCentrado(StringBuilder ticket, string texto)
        {
            int maxLength = 37;
            int totalPadding = maxLength - texto.Length;
            int padLeft = totalPadding / 2 + texto.Length;
            string centeredLine = texto.PadLeft(padLeft).PadRight(maxLength);
            ticket.AppendLine(centeredLine);
        }


        public string CrearTicketSELLO()
        {
            string TipoAsunto = inputTipoDocumento.SelectedValue;

            StringBuilder ticket = new StringBuilder();
            string nombreJuzgado = Session["NombreJuzgado"] as string;
            List<Anexos> anexos = Session["Anexos"] as List<Anexos>;

            List<string> lineasNombreJuzgado = DividirTextoEnLineas(nombreJuzgado, 32);

            int cantidadAnexos = CantidadAnexos(anexos);

            string NUC = inputNUC.Text;
            //string Causa = inputNumeroDocumento.Text;
            string Causa = Session["FolioNuevoInicial"] as string;


            ImprimirCentrado(ticket, "TRIBUNAL SUPERIOR");
            ImprimirCentrado(ticket, "DE JUSTICIA");
            ImprimirCentrado(ticket, "DEL ESTADO DE HIDALGO");
            ImprimirCentrado(ticket, "ATENCION CIUDADANA");
            ImprimirCentrado(ticket, ".........");
            foreach (string linea in lineasNombreJuzgado)
            {
                ImprimirCentrado(ticket, linea);
            }
            ImprimirCentrado(ticket, "INICIAL");
            ImprimirCentrado(ticket, ".........");
            if (TipoAsunto == "C")
            {
                var AsuntoIncial = "CAUSA";
                ticket.AppendLine($"{AsuntoIncial}: {Causa}");

            }
            else if (TipoAsunto == "CP")
            {
                var AsuntoIncial = "CUPRE";


                ticket.AppendLine($"{AsuntoIncial}:{Causa}");
            }
            ticket.AppendLine($"FOLIO: {Session["UserId"]}");
            ticket.AppendLine($"FECHA RECEPCIÒN:{GetFechaYHora()}");
            ticket.AppendLine($"NUC:{NUC.ToUpper()}");

            int maxLength = 36;
            int maxLengthT = 30;

            foreach (var anexo in anexos)
            {
                int espacioEntreColumnas = 3; // Puedes ajustar este valor según tus necesidades
                int longitudTotal = maxLength - espacioEntreColumnas;

                string linea = $"{anexo.DescripcionAnexo.ToUpper()}".PadRight(longitudTotal, '.') + $"{anexo.CantidadAnexo.ToUpper()}";
                ticket.AppendLine(linea);
            }


            int espacioEntreColumnasT = 3; // Puedes ajustar este valor según tus necesidades
            int longitudTotalT = maxLengthT - espacioEntreColumnasT;

            string separador = new string('.', longitudTotalT);
            if (cantidadAnexos != 0)
            {
                ticket.AppendLine($"TOTAL:{separador}{cantidadAnexos}");
            }
            else
            {
                Debug.WriteLine("No hay anexos");
            }





            return ticket.ToString();
        }


        private int CantidadAnexos(List<Anexos> anexos)
        {
            return anexos?.Sum(a => Convert.ToInt32(a.CantidadAnexo)) ?? 0;
        }
        protected string GetFechaYHora()
        {
            string formatoPersonalizado = "yyyy-MM-dd HH:mm:ss";
            string fechaYHoraFormateada = DateTime.Now.ToString(formatoPersonalizado);
            return fechaYHoraFormateada;
        }

        //FIN SELLO
        protected void btnEliminarDelito(object sender, EventArgs e)
        {
            // Obtener el botón que activó el evento
            Button btnEliminarDelitos = (Button)sender;

            // Obtener la fila del Repeater que contiene el botón
            RepeaterItem item = (RepeaterItem)btnEliminarDelitos.NamingContainer;
            //item.Visible = false;
            // Obtener el índice de la fila en el Repeater
            int indiceD = item.ItemIndex;

            // Obtener la lista de la sesión
            List<CatDelito> listaDelitos = (List<CatDelito>)Session["Delitos"];

            // Verificar si la lista no es nula y tiene elementos
            if (listaDelitos != null && listaDelitos.Count > indiceD)
            {
                // Eliminar el elemento en la posición indicada por el índice
                //item.Visible = false;
                listaDelitos.RemoveAt(indiceD);
                //RepeaterDelitos.Controls.RemoveAt(indiceD);
                //Session["Delitos"] = listaDelitos;
                RepeaterDelitos.DataSource = listaDelitos;
                RepeaterDelitos.DataBind();

            }
            // Verificar si la lista no es nula y tiene elementos
            updPanel.Update();

        }

        protected void btnEnviarDelito_Click(object sender, EventArgs e)
        {
            // Obtener el ID del delito seleccionado
            string inputIDDelito = inputDelitos.SelectedValue;
            string descripcionDelito = inputDelitos.SelectedItem.Text;
            if (inputDelitos.SelectedValue == "")
            {

                string script = $"toastError('{"Por favor, selecciona una opción válida."}');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                return;
            }
            // Crear un objeto CatDelito con los datos seleccionados
            CatDelito delito = new CatDelito
            {
                IdDelito = inputIDDelito,
                DescripcionDelito = descripcionDelito
            };

            // Obtener la lista actual de delitos de la sesión
            List<CatDelito> listaDelitos = Session["Delitos"] as List<CatDelito> ?? new List<CatDelito>();

            // Verificar si el delito ya existe en la lista
            if (listaDelitos.Any(v => v.IdDelito == inputIDDelito))
            {
                // Mostrar mensaje de error
                string mensaje = "Este delito ya existe en la lista.";
                string script = $"toastError('{mensaje}');";
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "mostrarToastScript", script, true);
                return;
            }

            // Agregar el delito a la lista y guardarla en la sesión
            listaDelitos.Add(delito);
            Session["Delitos"] = listaDelitos;

            // Mostrar los delitos en el Repeater
            RepeaterDelitos.DataSource = listaDelitos;
            RepeaterDelitos.DataBind();

            // Limpiar y actualizar el panel
            CleanEtiquetaFormAnexo();
            updPanel.Update();

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
            RegistroIniciales.CatDelitosResult catDelitos = RegistroIniciales.GetCatDelitos();

            // Recorres la lista de delitos y añades ListItem al DropDownList
            for (int i = 0; i < catDelitos.Delitos.Count; i++)
            {
                string delito = catDelitos.Delitos[i];
                string idDelito = catDelitos.IdDelitos[i];

                ListItem listItem = new ListItem(delito, idDelito);
                inputDelitos.Items.Add(listItem);
            }
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

            inputTipoDocumento.SelectedIndex = 0;
            inpuTipoRadicacion.SelectedIndex = 0;
            inputObservaciones.Text = "";
            inputQuienIngresa.SelectedIndex = 0;
            inputNombreParticular.Text = "";
            //inputPrioridad.SelectedIndex = ;

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
            RepeaterDelitos.DataSource = null;
            RepeaterDelitos.DataBind();

        }


    }



}