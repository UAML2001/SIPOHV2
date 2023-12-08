using DatabaseConnection;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;




namespace SIPOH.Views
{
    public partial class CustomRegistroIniciales : System.Web.UI.UserControl
    {
        List<int> listaDeIdDelitos = new List<int>();
        List<string> listaDeDelitos = new List<string>();
        List<Victima> listaDeUsuarios = new List<Victima>();
        

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



                CleanEtiquetas();
                
            }
            //List<Victima> listaDeUsuarios = Session["Victimas"] as List<Victima> ?? new List<Victima>();
            //listaDeUsuarios.Clear();
                
        }
        private void CleanEtiquetas()
        {
            txtNombreVictima.Text = "";
            txtAPVictima.Text = "";
            txtAMVictima.Text = "";
            txtRazonSocialVictima.Text = "";
            ddlSexoVictima.SelectedIndex = 0;
            txtNombreImputado.Text = "";
            txtAPaternoImputado.Text = "";
            txtAMaternoImputado.Text = "";
            txtGeneroImputado.SelectedIndex = 0;
            txtAliasImputado.Text = "";
            txtDescripcionAnexos.Text = "";
            txtCantidadAnexos.Text = "";
            txtAnexosTipo.SelectedIndex = 0;

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
       


        protected void btnGuardarVictima_Click(object sender, EventArgs e)
        {
            string nombreVictima = txtNombreVictima.Text;
            string apellidoMaternoVictima = txtAMVictima.Text;
            string apellidoPaternoVictima = txtAPVictima.Text;
            string razonSocialVictima = txtRazonSocialVictima.Text;
            string generoVictima = ddlSexoVictima.SelectedValue;

            Victima nuevoUsuario = new Victima
            {
                Nombre = nombreVictima,
                ApellidoMaterno = apellidoMaternoVictima,
                ApellidoPaterno = apellidoPaternoVictima,
                RazonSocial = razonSocialVictima,
                Genero = generoVictima,
            };

            // Intenta obtener la lista de la sesión
            List<Victima> listaDeUsuarios = Session["Victimas"] as List<Victima> ?? new List<Victima>();

            listaDeUsuarios.Add(nuevoUsuario);

            // Guardar la lista actualizada en la sesión
            Session["Victimas"] = listaDeUsuarios;

            // Actualizar el Repeater con la nueva lista de víctimas
            Repeater1.DataSource = listaDeUsuarios;
            Repeater1.DataBind();

            CleanEtiquetas();
            updPanel.Update();
        }
        protected void btnGuardarAnexos_Click(object sender, EventArgs e)
        {
            string inputTipoAnexo = txtAnexosTipo.SelectedValue;
            //string inputDescripcionAnexo = txtDescripcionAnexos.Text;
            string inputCantidadAnexo = txtCantidadAnexos.Text;
            Anexos anexo = new Anexos
            {
                //TipoAnexo = inputTipoAnexo,
                DescripcionAnexo = inputTipoAnexo,
                CantidadAnexo = inputCantidadAnexo

            };
            List<Anexos> listaDeAnexos = Session["Anexos"] as List<Anexos> ?? new List<Anexos>();
            listaDeAnexos.Add(anexo);
            Session["Anexos"] = listaDeAnexos;
            Repeater3.DataSource = listaDeAnexos;
            Repeater3.DataBind();
            CleanEtiquetas();
            updPanel.Update();
        }
        protected void btnGuardarImputado_Click(object sender, EventArgs e)
        {
            string inputNombreImputado = txtNombreImputado.Text;
            string inputAPaternoImputado = txtAPaternoImputado.Text;
            string inputAMaternoImputado = txtAMaternoImputado.Text;
            string inputGeneroImputado = txtGeneroImputado.SelectedValue;
            string inputAliasImputado = txtAliasImputado.Text;
            Imputado culpado = new Imputado
            {
                NombreCulpado = inputNombreImputado,
                APCulpado = inputAPaternoImputado,
                AMCulpado = inputAMaternoImputado,
                GeneroCulpado = inputGeneroImputado,
                AliasCulpado = inputAliasImputado
            };
            List<Imputado> listaDeImputados = Session["Imputados"] as List<Imputado> ?? new List<Imputado>();
            listaDeImputados.Add(culpado);
            Session["Imputados"] = listaDeImputados;
            Repeater2.DataSource = listaDeImputados;
            Repeater2.DataBind();
            CleanEtiquetas();
            updPanel.Update();
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
       string.IsNullOrWhiteSpace(Observaciones) || string.IsNullOrWhiteSpace(QuienIngresa) ||
       string.IsNullOrWhiteSpace(MP) || string.IsNullOrWhiteSpace(Prioridad) ||
       string.IsNullOrWhiteSpace(Fojas) || string.IsNullOrWhiteSpace(IdAudiencia) ||
       string.IsNullOrWhiteSpace(FeIngreso) || string.IsNullOrWhiteSpace(NUC))
            {
                // Mostrar un mensaje al usuario indicando que debe llenar todos los campos
                // Puedes usar un control como Label para mostrar el mensaje en tu interfaz
                lblError.Text = "Datos no estan correctamnte insertado";
                Debug.WriteLine("Datos no estan correctamnte insertados");



            return;  // Salir del método si no se llenaron todos los campos
            }
            try
            { 


                List<Victima> listaDeUsuarios = Session["Victimas"] as List<Victima> ?? new List<Victima>();
                List<Imputado> listaDeImputados = Session["Imputados"] as List<Imputado> ?? new List<Imputado>();
                List<Anexos> listaDeAnexos = Session["Anexos"] as List<Anexos> ?? new List<Anexos>();
                // Puedes usar la listaDeUsuarios según sea necesario en tu otra función
                foreach (var usuario in listaDeUsuarios)
                {
                    // Hacer algo con cada usuario, por ejemplo, imprimir en la consola de depuración
                    Debug.WriteLine($"Nombre: {usuario.Nombre}, Apellido Paterno: {usuario.ApellidoPaterno}, Apellido Materno: {usuario.ApellidoMaterno}, Razon Social: {usuario.RazonSocial}, Genero: {usuario.Genero}");
                }
                List<int> idDelitos = (List<int>)Session["IdDelitos"];

                RegistroIniciales.SendRegistroIniciales(FeIngreso, TipoAsunto, IdAudiencia, Observaciones, QuienIngresa, MP, Prioridad, Fojas,TipoRadicacion, NUC, idDelitos, listaDeUsuarios, listaDeImputados, listaDeAnexos);
               
                listaDeIdDelitos.Clear();
                listaDeUsuarios.Clear();
                listaDeImputados.Clear();
                listaDeAnexos.Clear();
                // método para realizar las inserciones
                //RegistroIniciales.SendDelitosIniciales(delitos);
                //MostrarResultadosAsuntoDelito();
                lblError.Visible = false;
                lblSuccess.Text = "Tu peticion fue exitosa";
                //List<Victima> listaDeUsuarios = new List<Victima>();
            }
            catch (Exception ex)
            {
                //  error genérico al usuario
                Debug.WriteLine("Problemas en la consulta: " + ex);
                lblError.Text = "Sucedio un error con su consulta, tu registro es incompleto o ya se ha generado anteriormente";
                lblSuccess.Visible = false;
            }

            //Debug.WriteLine("Tipo Asunto "+ TipoAsunto+"Valor seleccionado: " + IdAudienciaSelect + "fechas" + FeIngresoSelect+ "observaciones: " + Observaciones+ "Radicacion: "+ TipoRadicacion + "Quien ingresa: " + QuienIngresa + "MPName :" + MP + "NUM FOJAS: " + Fojas + "Prioridad: "+ Prioridad );
            
            CleanEtiquetas();
            updPanel.Update();
            

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
                    tablaHtml.AppendLine("<div class=\"table-responsive mt-2\">");
                    tablaHtml.AppendLine("<table class=\"table table-striped table-hover mb-0 table-sm\">");
                    tablaHtml.AppendLine("<thead class=\" text-center\">");
                    tablaHtml.AppendLine("<tr>");
                    tablaHtml.AppendLine("<th scope=\"col\" class=\"bg-success text-white\">IdDelito</th>");
                    tablaHtml.AppendLine("<th scope=\"col\" class=\"bg-success text-white\">Delito</th>");
                    tablaHtml.AppendLine("<th scope=\"col\" class=\"bg-success text-white\">Acciones</th>");
                    tablaHtml.AppendLine("</tr>");
                    tablaHtml.AppendLine("</thead>");
                    tablaHtml.AppendLine("<tbody class=\"table table-striped text-center \">");

                    // Iterar sobre las listas y agregar filas a la tabla
                    for (int i = 0; i < listaDeIdDelitos.Count; i++)
                    {
                        tablaHtml.AppendLine("<tr>");
                        tablaHtml.AppendLine($"<th scope=\"row\">{listaDeIdDelitos[i]}</th>");
                        tablaHtml.AppendLine($"<td class=\"text-secondary \">{listaDeNombresDelitos[i]}</td>");
                        tablaHtml.AppendLine($"<td class=\"text-secondary\"><i class=\"bi bi-trash-fill text-danger\"></i></td>");
                        tablaHtml.AppendLine("</tr>");
                    }

                    tablaHtml.AppendLine("</tbody>");
                    tablaHtml.AppendLine("</table>");
                    tablaHtml.AppendLine("</div>");

                    // Asignar la cadena HTML a un control en tu página (puede ser un LiteralControl o un literal en tu página)
                    litTablaDelitos.Text = tablaHtml.ToString();
                    lblIdDelitos.Text = "";
                }
                else
                {
                    // Mostrar un mensaje si el idDelito ya está en la lista
                    lblIdDelitos.Text = "El delito ya ha sido seleccionado";
                }

            }
            else
            {
                // Manejar el caso en el que la conversión falla
                // Puedes mostrar un mensaje de error o realizar alguna acción específica.
                // Por ejemplo:
                lblIdDelitos.Text = "No tiene delitos seleccionados";
                
            }

            // Actualizar el contenido del UpdatePanel
            updPanel.Update();
        }

        // Método para obtener el nombre del delito por su ID
        private string ObtenerNombreDelito(int idDelito)
        {
            (List<string> catDelitos, List<string> catIdsDelitos) = RegistroIniciales.GetCatDelitos();

            int index = catIdsDelitos.IndexOf(idDelito.ToString());

            return index != -1 ? catDelitos[index] : "Nombre no encontrado";
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






    }


}