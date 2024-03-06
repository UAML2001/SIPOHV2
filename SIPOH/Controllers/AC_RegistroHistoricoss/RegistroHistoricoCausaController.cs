using CrystalDecisions.ReportAppServer.DataDefModel;
using CrystalDecisions.Shared.Json;
using DatabaseConnection;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using static SIPOH.Controllers.AC_CatalogosCompartidos.CatDelitosController;

namespace SIPOH.Controllers.AC_RegistroHistoricos
{
    public class RegistroHistoricoCausaController : Controller
    {
        public class DataResultadoHistorico
        {
            public bool HayError { get; set; }
            public string MensajeResultado { get; set; }
            public string NumeroCausa { get; set; }
        }
        public class DataInsertHistoricoAsunto
        {
            public string NumeroDocumento {get; set;}
            public int IdJuzgado { get; set; }
            public string FeIngreso { get; set; }
            public string TipoAsunto { get; set; }
            public string Digitalizado { get; set; }
            
            public int IdUsuario { get; set; }
            public int IdAudiencia { get; set; }
            public string Observaciones { get; set; }
            public string QuienIngresa { get; set; }
            public string MP { get; set; }
            public string Prioridad { get; set; }
            public int Fojas { get; set; }

            public string TipoRadicacion { get; set; }
            public string NUC { get; set; }

            public int IdPerfil { get; set; }
        }
        
        public class DataInsertHistoricoVictimas
        {
            
            public string NombreParte { get; set; }
            public string ApellidoPaterno { get; set; }
            public string ApellidoMaterno { get; set; }
            public string Genero { get; set; }

        }
        public class DataInsertHistoricoInculpados
        {
            
            public string NombreParte { get; set; }
            public string ApellidoPaterno { get; set; }
            public string ApellidoMaterno { get; set; }
            public string Genero { get; set; }
            public string Alias { get; set; }
        }
        public class DatoIdAsunto
        {
           public int IdAsunto { get; set; }
        }
        // GET: RegistroHistoricoCausa

        public static List<DataResultadoHistorico> InsertHistoricoCausa(List<DataInsertHistoricoAsunto> DataHistorico, List<DataInsertHistoricoVictimas> DataVictima, List<DataInsertHistoricoInculpados> DataInculpado, List<DataCatDelitos> DataDelito)
        {
            List<DataResultadoHistorico> resultado = new List<DataResultadoHistorico>();
            using (SqlConnection connection = new ConexionBD().Connection)
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try { 
                DatoIdAsunto Asunto = new DatoIdAsunto();

                    foreach(var Historico in DataHistorico)
                    {
                        
                            using (SqlCommand commandInsertInicial = new SqlCommand("AC_InsertarInicial", connection, transaction))
                            {
                                commandInsertInicial.CommandType = CommandType.StoredProcedure;
                                commandInsertInicial.Parameters.Add("@numeroJuicioOral", SqlDbType.VarChar).Value = Historico.NumeroDocumento.ToUpper();
                                commandInsertInicial.Parameters.Add("@idJuzgado", SqlDbType.Int).Value = Historico.IdJuzgado;
                                commandInsertInicial.Parameters.Add("@TipoAsunto", SqlDbType.VarChar).Value = "C";
                                commandInsertInicial.Parameters.Add("@digitalizado", SqlDbType.VarChar).Value = Historico.Digitalizado.ToUpper();
                                commandInsertInicial.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = Historico.IdUsuario;
                                commandInsertInicial.Parameters.Add("@IdAudiencia", SqlDbType.Int).Value = Historico.IdAudiencia;
                                commandInsertInicial.Parameters.Add("@Observaciones", SqlDbType.VarChar).Value = Historico.Observaciones.ToUpper();
                                commandInsertInicial.Parameters.Add("@QuienIngresa", SqlDbType.VarChar).Value = Historico.QuienIngresa.ToUpper();
                                commandInsertInicial.Parameters.Add("@MP", SqlDbType.VarChar).Value = Historico.MP.ToUpper();
                                commandInsertInicial.Parameters.Add("@Prioridad", SqlDbType.VarChar).Value = Historico.Prioridad.ToUpper();
                                commandInsertInicial.Parameters.Add("@Fojas", SqlDbType.Int).Value = Historico.Fojas;
                                commandInsertInicial.Parameters.Add("@FeIngreso", SqlDbType.DateTime).Value = Historico.FeIngreso;
                                commandInsertInicial.Parameters.Add("@FeCaptura", SqlDbType.DateTime).Value = DateTime.Now;
                                commandInsertInicial.Parameters.AddWithValue("@IdAsunto", ParameterDirection.Output);
                                commandInsertInicial.Parameters.AddWithValue("@NumCausa", "0");
                                commandInsertInicial.Parameters.AddWithValue("@IdAsuntoCau", 1);

                                using (var reader = commandInsertInicial.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        var IdAsunto = reader["IdAsunto"];
                                        Asunto.IdAsunto = int.Parse(IdAsunto.ToString());
                                        resultado.Add(new DataResultadoHistorico { NumeroCausa = IdAsunto.ToString() });
                                    }
                                }

                            }
                            Debug.WriteLine("Asunto Ejecutado ✔️");
                       
                        
                        using (SqlCommand commandInsertCausa = new SqlCommand("INSERT INTO P_Causa(IdAsunto, TipoRadicacion, Remision, IdRazonIncom, FeImcompeten, IncomEntidad, IncomJuzgado, Acumulada, NUC) VALUES(@IdAsunto, @TipoRadicacion, 'N', NULL, NULL, NULL, NULL, 'N', @NUC)", connection, transaction))
                        {
                            commandInsertCausa.Parameters.Add("@IdAsunto", SqlDbType.Int).Value = Asunto.IdAsunto;
                            commandInsertCausa.Parameters.Add("@TipoRadicacion", SqlDbType.VarChar).Value = Historico.TipoRadicacion.ToUpper();
                            commandInsertCausa.Parameters.Add("@NUC", SqlDbType.VarChar).Value = Historico.NUC.ToUpper();
                            commandInsertCausa.ExecuteNonQuery();

                        }
                        Debug.WriteLine("Causa Ejecutado ✔️");
                        

                    }
                    using(SqlCommand commandInsertPartes = new SqlCommand("INSERT INTO P_PartesAsunto(IdAsunto, Nombre, APaterno, AMaterno, Genero, TipoParte, Alias)VALUES(@IdAsunto,@NombreParte,@APaterno,@AMaterno,@Genero, 'V',' ')", connection, transaction)){
                            
                        foreach(var victima in DataVictima)
                        {
                            commandInsertPartes.Parameters.Clear();
                            commandInsertPartes.Parameters.Add("@IdAsunto", SqlDbType.Int).Value = Asunto.IdAsunto;
                            commandInsertPartes.Parameters.Add("@NombreParte", SqlDbType.VarChar).Value = victima.NombreParte.ToUpper();
                            commandInsertPartes.Parameters.Add("@APaterno", SqlDbType.VarChar).Value = victima.ApellidoPaterno.ToUpper();
                            commandInsertPartes.Parameters.Add("@AMaterno", SqlDbType.VarChar).Value = victima.ApellidoMaterno.ToUpper();
                            commandInsertPartes.Parameters.Add("@Genero", SqlDbType.VarChar).Value = victima.Genero.ToUpper(); ;
                            commandInsertPartes.ExecuteNonQuery();
                        }

                    }
                    Debug.WriteLine("Victimas Ejecutado ✔️");
                    using (SqlCommand commandInsertPartes = new SqlCommand("INSERT INTO P_PartesAsunto(IdAsunto, Nombre, APaterno, AMaterno, Genero, TipoParte, Alias)VALUES(@IdAsuntoC,@NombreParteC,@APaternoC,@AMaternoC,@GeneroC, 'I',@Alias);", connection, transaction))
                    {
                        
                        foreach (var inculpado in DataInculpado)
                        {
                            commandInsertPartes.Parameters.Clear();
                            commandInsertPartes.Parameters.Add("@IdAsuntoC", SqlDbType.Int).Value = Asunto.IdAsunto;
                            commandInsertPartes.Parameters.Add("@NombreParteC", SqlDbType.VarChar).Value = inculpado.NombreParte.ToUpper();
                            commandInsertPartes.Parameters.Add("@APaternoC", SqlDbType.VarChar).Value = inculpado.ApellidoPaterno.ToUpper();
                            commandInsertPartes.Parameters.Add("@AMaternoC", SqlDbType.VarChar).Value = inculpado.ApellidoMaterno.ToUpper();
                            commandInsertPartes.Parameters.Add("@GeneroC", SqlDbType.VarChar).Value = inculpado.Genero.ToUpper();
                            commandInsertPartes.Parameters.Add("@Alias", SqlDbType.VarChar).Value = inculpado.Alias.ToUpper();
                            commandInsertPartes.ExecuteNonQuery();
                        }

                    }
                    Debug.WriteLine("Inculpado Ejecutado ✔️");
                    
                    using(SqlCommand commandInsertDelitos = new SqlCommand("INSERT INTO P_AsuntoDelito(IdDelito,IdAsunto)VALUES (@IdDelito,@IdAsunto);", connection, transaction))
                    {
                        foreach(var delito in DataDelito)
                        {
                            commandInsertDelitos.Parameters.Clear();
                            commandInsertDelitos.Parameters.Add("@IdAsunto", SqlDbType.Int).Value = Asunto.IdAsunto;
                            commandInsertDelitos.Parameters.Add("@IdDelito", SqlDbType.Int).Value = delito.IdDelito;
                            commandInsertDelitos.ExecuteNonQuery();
                        }
                    }
                    Debug.WriteLine("Delitos Ejecutado ✔️");
                    // Si no hubo excepciones, se agrega un resultado exitoso
                    

                    resultado.Add(new DataResultadoHistorico { HayError = false, MensajeResultado = "¡El guardado de datos fue exitoso!" });
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // En caso de excepción, se agrega un resultado con error
                    resultado.Add(new DataResultadoHistorico { HayError = true, MensajeResultado = $"no se pudieron guardar los datos, surgio un problema con su petición " });
                    transaction.Rollback();
                    Debug.WriteLine($"Error en la trasacción: {ex.Message}");
                }

                

            }
            return resultado;
        }
    }
}