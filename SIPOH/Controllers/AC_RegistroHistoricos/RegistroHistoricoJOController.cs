using DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIPOH.Controllers.AC_RegistroHistoricos
{
    public class RegistroHistoricoJOController : Controller
    {
        public class DataGetCausaImputadoJuicioOralHistorico
        {
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public int IdPartes { get; set; }
            public int IdAsuntoCausa { get; set; }
        }
        // GET: InicialJuicioOral
        public class InfoInsertJuicioOralHistorico
        {
            public int FolioNuevo { get; set; }
            public int IdTomadoFolio { get; set; }
            public string NumeroJOAsignado { get; set; }
            public int InsertedIdAsunto { get; set; }
            public int IdAsuntoDuplicado { get; set; }
            public int FolioNuevoUpdate { get; set; }
        }



        public class DataInsertJuicioOralHistorico
        {
            public int IdAsuntoCausa { get; set; }
            public int IdCircuitoFolio { get; set; }
   
            public int dIdJuzgado { get; set; }
            public string dFeIngreso { get; set; } //GETDATE()
            public string dTipoAsunto { get; set; }
            public string dDigitalizado { get; set; }
            public string dFeCaptura { get; set; } //GETDATE()
            public int dIdUsuario { get; set; }
            public int dIdAudiencia { get; set; }
            public string Observaciones { get; set; }
            public string QuienIngresa { get; set; }
            public string dMP { get; set; }
            public string dPrioridad { get; set; }
            public int dFojas { get; set; }
            //tabla p_Trayecto

            


            //P_CausaJO
            public int IdAsuntoJO { get; set; }

            public string NumCausa { get; set; }
            //P_JoPartes
            public int dIdAsuntoJO { get; set; }
            public int IdInculpadoDeli { get; set; }

            public int IdVictimaDeli { get; set; }
            public string NumeroArchivo { get; set; }
        }
       
        public class DataGetCausaDelitosJuicioOralHistorico
        {
            public string Delito { get; set; }
            public int IdDelito { get; set; }
            public int IdInculpadoDel { get; set; }
            public string NombreInculpado { get; set; }
        }
        public class DataGetCausaVictimaJuicioOralHistorico
        {
            public int IdVictimaDel { get; set; }
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public int IdDeliAsunto { get; set; }
        }
        public class DataRelacionesVIHistorico
        {
            public string nombreVictima { get; set; }
            public string apellidosVictima { get; set; }
            public int idVictima { get; set; }
            public string nombreInculpado { get; set; }
            public string apellidosInculpado { get; set; }
            public int idInculpado { get; set; }
            public string delito { get; set; }
            public int idDelito { get; set; }

        }
        public class DatosJOHistorico
        {
            public bool HayError { get; set; }
            public string MensajeError { get; set; }
            public string NumeroJO { get; set; }
        }

        //CONTROLADORES JUICIO ORAL
        public static List<DataGetCausaImputadoJuicioOralHistorico> GetCausaHistorico(string numeroCausaConsulta, int IdJuzgado)
        {
            List<DataGetCausaImputadoJuicioOralHistorico> resultados = new List<DataGetCausaImputadoJuicioOralHistorico>();
            using (SqlConnection connection = new ConexionBD().Connection)
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("AC_GetCausaImputadoToJuicioOral", connection))
                    {

                        command.Parameters.AddWithValue("@IdJuzgado", IdJuzgado);
                        command.Parameters.AddWithValue("@Numero", numeroCausaConsulta);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        using (SqlDataReader readerJO = command.ExecuteReader())
                        {
                            while (readerJO.Read())
                            {
                                DataGetCausaImputadoJuicioOralHistorico infoCausa = new DataGetCausaImputadoJuicioOralHistorico();
                                infoCausa.Nombre = readerJO["Nombre"].ToString();
                                infoCausa.Apellidos = readerJO["Imputado"].ToString();
                                infoCausa.IdPartes = int.Parse(readerJO["IdPartes"].ToString());
                                infoCausa.IdAsuntoCausa = int.Parse(readerJO["IdAsunto"].ToString());
                                resultados.Add(infoCausa);

                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception($"Error en la consulta para extraer consulta causa: {ex.Message}", ex);
                }

            }
            return resultados;
        }
        public static List<DataGetCausaVictimaJuicioOralHistorico> GetVictimaHistorico(int DelitoAsociado)
        {
            List<DataGetCausaVictimaJuicioOralHistorico> resultados = new List<DataGetCausaVictimaJuicioOralHistorico>();
            using (SqlConnection connection = new ConexionBD().Connection)
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("AC_GetCausaVictimaToJuicioOral", connection))
                    {

                        command.Parameters.AddWithValue("@IdDelito", DelitoAsociado);

                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        using (SqlDataReader readerJO = command.ExecuteReader())
                        {
                            while (readerJO.Read())
                            {
                                DataGetCausaVictimaJuicioOralHistorico infoCausa = new DataGetCausaVictimaJuicioOralHistorico();
                                infoCausa.Nombre = readerJO["Nombre"].ToString();
                                infoCausa.Apellidos = readerJO["Apellidos"].ToString();
                                infoCausa.IdVictimaDel = int.Parse(readerJO["IdVictimaDel"].ToString());
                                infoCausa.IdDeliAsunto = int.Parse(readerJO["IdDeliAsunto"].ToString());
                                resultados.Add(infoCausa);

                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception($"Error en la consulta para extraer consulta causa: {ex.Message}", ex);
                }

            }
            return resultados;
        }
        public static List<DataGetCausaDelitosJuicioOralHistorico> GetDelitosHistorico(int IdDelito)
        {
            List<DataGetCausaDelitosJuicioOralHistorico> resultados = new List<DataGetCausaDelitosJuicioOralHistorico>();
            using (SqlConnection connection = new ConexionBD().Connection)
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("AC_GetCausaDelitosToJuicioOral", connection))
                    {
                        command.Parameters.AddWithValue("@IdDelito", IdDelito);
                        ;
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        using (SqlDataReader readerJO = command.ExecuteReader())
                        {
                            while (readerJO.Read())
                            {
                                DataGetCausaDelitosJuicioOralHistorico infoDelito = new DataGetCausaDelitosJuicioOralHistorico();
                                infoDelito.Delito = readerJO["Nombre"].ToString();
                                infoDelito.IdDelito = int.Parse(readerJO["IdDeliAsunto"].ToString());
                                infoDelito.IdInculpadoDel = int.Parse(readerJO["IdInculpadoDel"].ToString());
                                infoDelito.NombreInculpado = readerJO["NombreInculpado"].ToString();
                                resultados.Add(infoDelito);

                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception($"Error en la consulta para extraer consulta Delitos: {ex.Message}", ex);
                }
            }
            return resultados;
        }



        public static DatosJOHistorico InsertJuicioOralHistorico(List<DataInsertJuicioOralHistorico> objetoDataJuicioOral,  List<DataRelacionesVIHistorico> Partes)
        {
            DatosJOHistorico resultado = new DatosJOHistorico();
            using (SqlConnection connection = new ConexionBD().Connection)
            {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    foreach (var DataInsertJuicioOral in objetoDataJuicioOral)
                    {
                        //Paso 1: consulta de Folio por circuito
                        InfoInsertJuicioOralHistorico infoInsertJO = new InfoInsertJuicioOralHistorico();
                        using (SqlCommand commandConsultaNumero = new SqlCommand("AC_verificar_disponibilidad_folio_en_PAsuntoJO", connection, transaction))
                        {
                            commandConsultaNumero.Parameters.AddWithValue("@NuevoNumero", DataInsertJuicioOral.NumeroArchivo);
                            commandConsultaNumero.Parameters.AddWithValue("@Circuito", DataInsertJuicioOral.IdCircuitoFolio);
                            commandConsultaNumero.CommandType = CommandType.StoredProcedure;

                            using (var readerJOC = commandConsultaNumero.ExecuteReader())
                            {
                                if (readerJOC.Read())
                                {
                                    infoInsertJO.IdAsuntoDuplicado = Convert.ToInt32(readerJOC["IdAsunto"]);
                                    if (infoInsertJO.IdAsuntoDuplicado != 0)
                                    {
                                        int idAsuntoDuplicado = infoInsertJO.IdAsuntoDuplicado;
                                        Debug.WriteLine("Error: Número de Asunto duplicado. IdAsunto: " + idAsuntoDuplicado);
                                        
                                        resultado.MensajeError = "Tu Asunto ya ha sido ingresado anteriormente";
                                        throw new Exception("El juicio oral ya ha sido ingresado anteriormente, verifica tu petición");
                                        transaction.Rollback();
                                        return resultado;
                                    }
                                }

                                // Cerrar el primer DataReader antes de abrir otro
                                readerJOC.Close();
                            }
                        }


                        Debug.WriteLine("Obtener ERROR en consulta Numero de JO ✔️");

                        
                            //Inicio de insert en JO: Tabla P_Asunto
                            using (SqlCommand commandIn = new SqlCommand("AC_InsertarInicial", connection, transaction))
                            {
                                //DATA TO INSERT
                                commandIn.Parameters.AddWithValue("@numeroJuicioOral", DataInsertJuicioOral.NumeroArchivo);
                                commandIn.Parameters.AddWithValue("@idJuzgado", DataInsertJuicioOral.dIdJuzgado);
                                commandIn.Parameters.AddWithValue("@TipoAsunto", DataInsertJuicioOral.dTipoAsunto.ToUpper());
                                commandIn.Parameters.AddWithValue("@digitalizado", DataInsertJuicioOral.dDigitalizado.ToUpper());
                                commandIn.Parameters.AddWithValue("@IdUsuario", DataInsertJuicioOral.dIdUsuario);
                                commandIn.Parameters.AddWithValue("@IdAudiencia", DataInsertJuicioOral.dIdAudiencia);
                                commandIn.Parameters.AddWithValue("@Observaciones", DataInsertJuicioOral.Observaciones.ToUpper());
                                commandIn.Parameters.AddWithValue("@QuienIngresa", DataInsertJuicioOral.QuienIngresa.ToUpper());
                                commandIn.Parameters.AddWithValue("@MP", DataInsertJuicioOral.dMP.ToUpper());
                                commandIn.Parameters.AddWithValue("@Prioridad", DataInsertJuicioOral.dPrioridad.ToUpper());
                                commandIn.Parameters.AddWithValue("@Fojas", DataInsertJuicioOral.dFojas);
                                commandIn.Parameters.Add("@FeIngreso", SqlDbType.DateTime).Value =DataInsertJuicioOral.dFeIngreso;
                                commandIn.Parameters.AddWithValue("@FeCaptura", DataInsertJuicioOral.dFeCaptura);
                                commandIn.Parameters.AddWithValue("@IdAsunto", ParameterDirection.Output);
                                commandIn.Parameters.AddWithValue("@NumCausa", DataInsertJuicioOral.NumCausa);
                                commandIn.Parameters.AddWithValue("@IdAsuntoCau", DataInsertJuicioOral.IdAsuntoCausa);

                                commandIn.CommandType = CommandType.StoredProcedure;

                                using (var readerJOI = commandIn.ExecuteReader())
                                {
                                    if (readerJOI.Read())
                                    {
                                        infoInsertJO.InsertedIdAsunto = int.Parse(readerJOI["IdAsunto"].ToString());
                                        
                                    }
                                }
                            }
                        
                        


                        Debug.WriteLine("Registro tabla P_Asunto ✔️");
                        //Registro de partes JOPARTES
                        try
                        {

                            using (SqlCommand commandPartes = new SqlCommand("AC_InsertarPartesJO", connection, transaction))
                            {
                                foreach (var parte in Partes)
                                {
                                    commandPartes.Parameters.Clear();
                                    commandPartes.Parameters.AddWithValue("@IdAsuntoJO", infoInsertJO.InsertedIdAsunto);
                                    commandPartes.Parameters.Add("@IdInculpadoDel", SqlDbType.Int).Value = parte.idInculpado;
                                    commandPartes.Parameters.Add("@IdVictimaDel", SqlDbType.Int).Value = parte.idVictima;
                                    commandPartes.CommandType = CommandType.StoredProcedure;
                                    commandPartes.ExecuteNonQuery();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error: Tus partes ya han sido relacionadas, verifica tu consulta por favor.");
                        }
                       
                            //DataRelacionesVI dataRegistroPartesJO = new DataRelacionesVI();
                        
                        
                        Debug.WriteLine("Registro en tabla P_JOPartes ✔️");                        

                        //Registro datos en P_Trayecto
                        
                                              


                    }
                    transaction.Commit();
                    resultado.HayError = true;
                    resultado.MensajeError = "Formulario enviado";
                    Debug.WriteLine("Transaccion Finalizada con exito");

                }
                catch (Exception ex)
                {
                    resultado.HayError = false;
                    resultado.MensajeError = ex.Message;
                    transaction.Rollback();
                    

                }


            }
            return resultado;
            //return true;
        }
    }
}





        