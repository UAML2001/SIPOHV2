using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using SIPOH;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;

public class MostrarCedulaImputado
{
    public void MostrarCedula(int idAsunto, int idPartes, string apPaterno, string apMaterno, string nombre, string genero, string tipoParte, string rfc, string curp, string edad, DateTime feNacimiento, string aliasImp, int idContNacido, int idPaisNacido,
            int idEstadoNacido, string idMunicipioNacido, int idNacionalidad, int idCondicion, int idEstadoCivil,
            int idGradoEstudios, int idAlfabet, int idiomaEspañol, int idPueblo, int hablaIndigena,
            int idDialecto, int idOcupacion, int idProfesion, string domOcupacion, int discapacidad, int idLengExtra, int idRelacImput, int idAsisMigra, int idContiResidencia, int idPaisResidencia,
            int idEstadoResidencia, string idMunicipioResidencia, string domResidencia, int idDefensor, int interprete,
            int ordenProteccion, DateTime feIndividualizacion, int idDocIdentificador, string numDocumento, string privacidad,
            string telefono, string correo, string fax, string domNotificacion, string otroTipo, int idUser, List<string> idsDiscapacidades,
            Page page, int idEstadoPsi, int idaccipenal, int idReinci, int idTipoDeten, int idOrdenJudi, int idCondFamiliar, int depEcon, int idSustancias)
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();

                // Ejecutar el procedimiento almacenado LlenarCedulaImputado
                using (SqlCommand cmd = new SqlCommand("LlenarCedulaImputado", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", idUser);
                    cmd.Parameters.AddWithValue("@IdAsunto", idAsunto);
                    cmd.Parameters.AddWithValue("@IdPartes", idPartes); // Asegúrate de definir idPartes
                    cmd.ExecuteNonQuery();

                    // Crear un DataTable para almacenar los resultados
                    DataTable dt = new DataTable();

                    // Recoger los resultados
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Llenar el DataTable con los resultados
                        dt.Load(reader);
                    }

                    // Configura la ruta del informe Crystal Reports (.rpt)
                    string rutaInforme = System.Web.HttpContext.Current.Server.MapPath("~/ExpedienteDigital/Imputados/CedulaImputados.rpt");

                    // Crea el informe
                    ReportDocument reporte = new ReportDocument();
                    reporte.Load(rutaInforme);

                    // Asignar el DataTable como fuente de datos del informe
                    reporte.SetDataSource(dt);

                    // Configura los parámetros del informe (asegúrate de que los nombres de los parámetros coincidan con los del informe)
                    reporte.SetParameterValue("@IdUsuario", idUser);
                    reporte.SetParameterValue("@IdAsunto", idAsunto);
                    reporte.SetParameterValue("@IdPartes", idPartes);

                    // Si el informe tiene más parámetros, configúralos también aquí
                    // reporte.SetParameterValue("OtroParametro", otroValor);

                    // Configura el formato de salida como PDF
                    reporte.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    reporte.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    string rutaArchivoPDF = System.Web.HttpContext.Current.Server.MapPath("~/ExpedienteDigital/Imputados/CedulaImputados.pdf");
                    reporte.ExportOptions.DestinationOptions = new DiskFileDestinationOptions { DiskFileName = rutaArchivoPDF };

                    // Exporta el informe a PDF
                    reporte.Export();
                }

                    // Llama al método en el archivo .aspx para mostrar el PDF
                    ((Imputados)page).MostrarCedula("~/ExpedienteDigital/Imputados/CedulaImputados.pdf");

                // Registro del script de Toastr después de la inserción
                ScriptManager.RegisterStartupScript(page, page.GetType(), "alertMessage", "toastr.success('Cedula generada correctamente.', 'Éxito');", true);
        }
    }
}