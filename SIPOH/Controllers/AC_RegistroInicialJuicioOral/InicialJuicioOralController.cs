using DatabaseConnection;
using SIPOH.Controllers.AC_Digitalizacion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIPOH.Controllers.AC_RegistroInicialJuicioOral
{
    public class InicialJuicioOralController : Controller
    {
        
        public class DataGetCausaImputadoJuicioOral
        {
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public int IdPartes { get; set; }
            public int IdAsuntoCausa { get; set; }


        }
        // GET: InicialJuicioOral
        public class InfoInsertJuicioOral
        {
             public int FolioNuevo { get; set; }
             public int IdTomadoFolio { get; set; }
             public string NumeroJOAsignado { get; set; }
             public int InsertedIdAsunto { get; set; }
             public int IdAsuntoDuplicado { get; set; }
            public int FolioNuevoUpdate { get; set; }
        }
       


        public class DataInsertJuicioOral
        {
            public int IdAsuntoCausa { get; set; }
            public int IdCircuitoFolio { get; set; }
            public string dNumero { get; set; }
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
            
            public int dIdActividad { get; set;}
            public int dIdPerfil { get; set; }
            public string dtipo { get; set; }
            public string dEstado { get; set; }
            public int dIdPosterior { get; set; }
            
            
            //P_CausaJO
            public int IdAsuntoJO { get; set; }
            
            public string NumCausa { get; set; }
            //P_JoPartes
            public int dIdAsuntoJO { get; set; }
            public int IdInculpadoDeli { get; set; }

            public int IdVictimaDeli { get; set; }

        }
        public class DataRegistroAnexos
        {
            
            public int IDPosterior { get; set; }
            public string Digitalizado { get; set; }
            public string Descripcion { get; set; }
            public int cantidad { get; set; }
        }
        public class DataGetCausaDelitosJuicioOral
        {
            public string Delito { get; set; }            
            public int IdDelito { get; set; }
            public int IdInculpadoDel { get; set;}
            public string NombreInculpado { get; set; }
        }
        public class DataGetCausaVictimaJuicioOral
        {
            public int IdVictimaDel { get; set; }
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public int IdDeliAsunto { get; set; }
        }
        public class DataRelacionesVI
        {
            public string nombreVictima { get; set; }
            public string apellidosVictima { get; set; }
            public int idVictima { get; set; }
            public string nombreInculpado { get; set; }
            public string apellidosInculpado { get; set;}
            public int idInculpado { get; set; }
            public string delito { get; set; }
            public int idDelito { get; set; }
           
        }
        public class DatosJO
        {
            public bool HayError { get; set; }
            public string MensajeError { get; set; }
            public string NumeroJO { get; set; }
            public int IdAsunto { get; set; }
        }
        
        //CONTROLADORES JUICIO ORAL
        public static List<DataGetCausaImputadoJuicioOral> GetCausa( string numeroCausaConsulta, int IdJuzgado)
        {
            List<DataGetCausaImputadoJuicioOral> resultados = new List<DataGetCausaImputadoJuicioOral> ();
            using(SqlConnection connection = new ConexionBD().Connection)
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("AC_GetCausaImputadoToJuicioOral", connection))
                    {
                    
                        command.Parameters.AddWithValue("@IdJuzgado", IdJuzgado);
                        command.Parameters.AddWithValue("@Numero", numeroCausaConsulta);                        
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        using(SqlDataReader readerJO = command.ExecuteReader())
                        {
                            while (readerJO.Read())
                            {
                                DataGetCausaImputadoJuicioOral infoCausa = new DataGetCausaImputadoJuicioOral();
                                infoCausa.Nombre = readerJO["Nombre"].ToString();
                                infoCausa.Apellidos = readerJO["Imputado"].ToString();
                                infoCausa.IdPartes = int.Parse(readerJO["IdPartes"].ToString());
                                infoCausa.IdAsuntoCausa = int.Parse(readerJO["IdAsunto"].ToString());
                                resultados.Add(infoCausa);

                            }
                        }
                    }

                }catch(Exception ex)
                {
                    throw new Exception($"Error en la consulta para extraer consulta causa: {ex.Message}", ex);
                }
                
            }
            return resultados;
        }
        public static List<DataGetCausaVictimaJuicioOral> GetVictima( int DelitoAsociado)
        {
            List<DataGetCausaVictimaJuicioOral> resultados = new List<DataGetCausaVictimaJuicioOral> ();
            using(SqlConnection connection = new ConexionBD().Connection)
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("AC_GetCausaVictimaToJuicioOral", connection))
                    {
                    
                        command.Parameters.AddWithValue("@IdDelito", DelitoAsociado);
                                               
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        using(SqlDataReader readerJO = command.ExecuteReader())
                        {
                            while (readerJO.Read())
                            {
                                DataGetCausaVictimaJuicioOral infoCausa = new DataGetCausaVictimaJuicioOral();
                                infoCausa.Nombre = readerJO["Nombre"].ToString();
                                infoCausa.Apellidos = readerJO["Apellidos"].ToString();
                                infoCausa.IdVictimaDel = int.Parse(readerJO["IdVictimaDel"].ToString());
                                infoCausa.IdDeliAsunto = int.Parse(readerJO["IdDeliAsunto"].ToString());
                                resultados.Add(infoCausa);

                            }
                        }
                    }

                }catch(Exception ex)
                {
                    throw new Exception($"Error en la consulta para extraer consulta causa: {ex.Message}", ex);
                }
                
            }
            return resultados;
        }
        public static List<DataGetCausaDelitosJuicioOral> GetDelitos(int IdDelito)
        {
            List<DataGetCausaDelitosJuicioOral> resultados = new List<DataGetCausaDelitosJuicioOral>();
            using(SqlConnection connection = new ConexionBD().Connection)
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
                                DataGetCausaDelitosJuicioOral infoDelito = new DataGetCausaDelitosJuicioOral();
                                infoDelito.Delito = readerJO["Nombre"].ToString();
                                infoDelito.IdDelito = int.Parse(readerJO["IdDeliAsunto"].ToString());                                
                                infoDelito.IdInculpadoDel = int.Parse(readerJO["IdInculpadoDel"].ToString()); 
                                infoDelito.NombreInculpado = readerJO["NombreInculpado"].ToString();
                                resultados.Add(infoDelito);     

                            }
                        }
                    }

                }catch(Exception ex)
                {
                    throw new Exception($"Error en la consulta para extraer consulta Delitos: {ex.Message}", ex);
                }
            }
            return resultados;
        }

        public static int Equipo;

        public static DatosJO InsertJuicioOral(List<DataInsertJuicioOral> objetoDataJuicioOral, List<DataRegistroAnexos> Anexos, List<DataRelacionesVI> Partes)
        {   
                DatosJO resultado = new DatosJO();
            using(SqlConnection connection = new ConexionBD().Connection)
            {
                try
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();
                    foreach (var DataInsertJuicioOral in objetoDataJuicioOral)
                    {
                        //Paso 1: consulta de Folio por circuito
                        InfoInsertJuicioOral infoInsertJO = new InfoInsertJuicioOral(); 
                        try
                        {
                            using (SqlCommand commandConsultaNumero = new SqlCommand("AC_AsignarFolioJO", connection, transaction))
                            {
                                commandConsultaNumero.Parameters.AddWithValue("@Circuito", DataInsertJuicioOral.IdCircuitoFolio);
                                commandConsultaNumero.CommandType = CommandType.StoredProcedure;
                                using(var reader = commandConsultaNumero.ExecuteReader())
                                {
                                    if(reader.Read())
                                    {
                                        infoInsertJO.FolioNuevo = int.Parse(reader["FolioNuevo"].ToString());
                                        infoInsertJO.IdTomadoFolio = int.Parse(reader["IdFolio"].ToString());
                                        int currentYear = DateTime.Now.Year;
                                        infoInsertJO.NumeroJOAsignado = infoInsertJO.FolioNuevo.ToString("D4") + "/" + currentYear;
                                        resultado.NumeroJO = infoInsertJO.NumeroJOAsignado;
                                    }
                                }
                          
                            }
                                                        
                            
                        }
                        catch (SqlException ex)
                        {
                            resultado.HayError = false;
                            resultado.MensajeError = $"Error en la insersion de Juicio oral {ex.Message}";
                            Debug.WriteLine($"Error en el proceso almacenado: {ex.Message}");
                            // Realizar Rollback para cancelar la transacción
                            transaction.Rollback();
                            throw new Exception("Ocurrió un error en el proceso de inserción. Mensaje de error: " +  ex.Message  );
                            
                            
                            

                        }
                        Debug.WriteLine("Consulta folio exitoso ✔️");
                                        
                        try
                        {
                           
                            using (SqlCommand commandConsultaNumero = new SqlCommand("AC_verificar_disponibilidad_folio_en_PAsuntoJO", connection, transaction))
                            {
                                commandConsultaNumero.Parameters.AddWithValue("@NuevoNumero", infoInsertJO.NumeroJOAsignado);
                                commandConsultaNumero.Parameters.AddWithValue("@Circuito", DataInsertJuicioOral.IdCircuitoFolio);
                                commandConsultaNumero.CommandType = CommandType.StoredProcedure;
                                try
                                {
                                    using (var readerJOC = commandConsultaNumero.ExecuteReader())
                                    {
                                        if (readerJOC.Read())
                                        {
                                            infoInsertJO.IdAsuntoDuplicado = int.Parse(readerJOC["IdAsunto"].ToString());
                                            if (Convert.ToInt32(infoInsertJO.IdAsuntoDuplicado) != 0)
                                            {
                                                int idAsuntoDuplicado = Convert.ToInt32(infoInsertJO.IdAsuntoDuplicado);
                                                Debug.WriteLine("Error: Número de Asunto duplicado. IdAsunto: " + idAsuntoDuplicado);
                                                // Aquí puedes manejar el caso de duplicado, por ejemplo, lanzar una excepción o tomar alguna acción específica.
                                                // Realizar Rollback para cancelar la transacción
                                                transaction.Rollback();
                                            }
                                        }
                                    }
                                    
                                }
                                catch (SqlException ex)
                                {
                                    resultado.HayError = false;
                                    resultado.MensajeError = $"Error en verificacion de tu folio oral {ex.Message}";
                                    Debug.WriteLine("Error al ejecutar el procedimiento almacenado consulta: " + ex.Message);
                                    transaction.Rollback();
                                    throw new Exception("Ocurrió un error en el proceso de inserción. Mensaje de error: " + ex.Message);

                                }
                            }
                        }
                        catch (SqlException ex)
                        {
                            resultado.HayError = false;
                            resultado.MensajeError = $"Error en la insersion de Juicio oral {ex.Message}";
                            Debug.WriteLine($"Error en el proceso almacenado: {ex.Message}");
                            // Realizar Rollback para cancelar la transacción
                            transaction.Rollback();
                            throw new Exception("Ocurrió un error en el proceso de inserción. Mensaje de error: " + ex.Message);


                        }
                        
                        Debug.WriteLine("Obtener ERROR en consulta Numero de JO ✔️" );

                        try
                        {
                            //Asignacion carga de trabjo
                            using (SqlConnection Connection = new ConexionBD().Connection)
                            {
                                using (SqlCommand command = new SqlCommand("AC_AsignacionCargaTrabajo", connection, transaction))
                                {
                                    command.Parameters.Add("@IdJuzgado", SqlDbType.Int).Value = DataInsertJuicioOral.dIdJuzgado;
                                    command.Parameters.Add("@TipoAsunto", SqlDbType.VarChar).Value = DataInsertJuicioOral.dTipoAsunto.ToUpper();
                                    command.CommandType = CommandType.StoredProcedure;
                                    using (SqlDataReader reader = command.ExecuteReader())
                                    {
                                        while (reader.Read())
                                        {
                                            Equipo = int.Parse(reader["Equipo"].ToString());

                                        }
                                    }
                                }
                            }
                            //Inicio de insert en JO: Tabla P_Asunto
                            using (SqlCommand commandIn = new SqlCommand("AC_InsertarInicial", connection, transaction))
                            {
                                //DATA TO INSERT
                                commandIn.Parameters.AddWithValue("@numeroJuicioOral", infoInsertJO.NumeroJOAsignado);
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
                                commandIn.Parameters.AddWithValue("@FeIngreso", DataInsertJuicioOral.dFeIngreso);
                                commandIn.Parameters.AddWithValue("@FeCaptura", DataInsertJuicioOral.dFeCaptura);
                                commandIn.Parameters.AddWithValue("@IdAsunto", ParameterDirection.Output);
                                commandIn.Parameters.AddWithValue("@NumCausa", DataInsertJuicioOral.NumCausa);
                                commandIn.Parameters.AddWithValue("@IdAsuntoCau", DataInsertJuicioOral.IdAsuntoCausa);
                                commandIn.Parameters.AddWithValue("@Equipo", Equipo);

                                commandIn.CommandType = CommandType.StoredProcedure;

                                using (var readerJOI = commandIn.ExecuteReader())
                                {
                                    if (readerJOI.Read())
                                    {
                                        infoInsertJO.InsertedIdAsunto = int.Parse(readerJOI["IdAsunto"].ToString());
                                        resultado.IdAsunto = infoInsertJO.InsertedIdAsunto;
                                    }
                                }
                            }
                        }
                        catch (SqlException ex)
                        {
                            resultado.HayError = false;
                            resultado.MensajeError = $"Error en la insersion de Juicio oral {ex.Message}";
                            Debug.WriteLine($"Error en el proceso almacenados: {ex.Message}");
                            
                            // Realizar Rollback para cancelar la transacción
                            transaction.Rollback();
                            throw new Exception("Ocurrió un error en el proceso de inserción. Mensaje de error: " + ex.Message);

                        }

                        
                        Debug.WriteLine("Registro tabla P_Asunto ✔️");
                        //Registro de partes JOPARTES
                        try
                        {
                            //DataRelacionesVI dataRegistroPartesJO = new DataRelacionesVI();
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
                        }catch (Exception ex)
                        {
                            resultado.HayError = false;
                            resultado.MensajeError = $"Error en la insersion de Juicio oral {ex.Message}";
                            Debug.WriteLine($"Error en el proceso almacenado: {ex.Message}");
                            // Realizar Rollback para cancelar la transacción
                            transaction.Rollback();
                            throw new Exception("Ocurrió un error en el proceso de inserción. Mensaje de error: " + ex.Message);


                        }
                        Debug.WriteLine("Registro en tabla P_JOPartes ✔️");
                        //Registro de anexos 
                        try
                        {
                            using (SqlCommand commandAnexos = new SqlCommand("AC_InsertarAnexo", connection, transaction))
                            {
                                foreach (var DataRegistroAnexos in Anexos)
                                {
                                    commandAnexos.Parameters.Clear();

                                    commandAnexos.Parameters.AddWithValue("@IDAsunto", SqlDbType.Int).Value = infoInsertJO.InsertedIdAsunto;
                                    commandAnexos.Parameters.Add("@IDPosterior", SqlDbType.Int).Value = DataRegistroAnexos.IDPosterior;
                                    commandAnexos.Parameters.Add("@Descripcion", SqlDbType.VarChar).Value = DataRegistroAnexos.Descripcion.ToUpper();
                                    commandAnexos.Parameters.Add("@Cantidad", SqlDbType.Int).Value = DataRegistroAnexos.cantidad;
                                    commandAnexos.Parameters.Add("@Digitalizado", SqlDbType.VarChar).Value = DataRegistroAnexos.Digitalizado.ToUpper();
                                    commandAnexos.CommandType = CommandType.StoredProcedure;
                                    commandAnexos.ExecuteNonQuery();
                                }
                            }
                        }catch (Exception ex) {
                            resultado.HayError = true;
                            resultado.MensajeError = $"Error en la insersion de Juicio oral {ex.Message}";
                            Debug.WriteLine($"Error en el proceso almacenado3: {ex.Message}");
                            // Realizar Rollback para cancelar la transacción
                            transaction.Rollback();
                            throw new Exception("Ocurrió un error en el proceso de inserción. Mensaje de error: " + ex.Message);


                        }
                        Debug.WriteLine("Registro de Anexos ✔️");
                        
                        //Registro datos en P_Trayecto
                        try
                        {
                            using (SqlCommand commandTrayecto = new SqlCommand("AC_InsertarTrayecto", connection, transaction))
                            {
                                commandTrayecto.Parameters.Add("@IdAsunto", SqlDbType.Int).Value = infoInsertJO.InsertedIdAsunto;
                                commandTrayecto.Parameters.Add("@IdPosterior", SqlDbType.Int).Value = DataInsertJuicioOral.dIdPosterior;
                                commandTrayecto.Parameters.Add("@IdActividad", SqlDbType.Int).Value = DataInsertJuicioOral.dIdActividad;
                                commandTrayecto.Parameters.Add("@IdPerfil", SqlDbType.Int).Value = DataInsertJuicioOral.dIdPerfil;
                                commandTrayecto.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = DataInsertJuicioOral.dIdUsuario;
                                commandTrayecto.Parameters.Add("@FeAsunto", SqlDbType.VarChar).Value = DataInsertJuicioOral.dFeCaptura;
                                commandTrayecto.Parameters.Add("@Tipo", SqlDbType.VarChar).Value = DataInsertJuicioOral.dtipo.ToUpper();
                                commandTrayecto.Parameters.Add("@Estado", SqlDbType.VarChar).Value = DataInsertJuicioOral.dEstado.ToUpper();
                                commandTrayecto.Parameters.Add("@IdEquipo", SqlDbType.Int).Value = Equipo;
                                commandTrayecto.CommandType = CommandType.StoredProcedure;
                                commandTrayecto.ExecuteNonQuery();
                            }
                        }
                        catch (SqlException ex)
                        {
                            
                            Debug.WriteLine($"Error en el proceso almacenado2: {ex.Message}");
                            // Realizar Rollback para cancelar la transacción
                            transaction.Rollback();
                            throw new Exception("Ocurrió un error en el proceso de inserción. Mensaje de error: " + ex.Message);


                        }
                        Debug.WriteLine("Registro trayecto ✔️");
                        //actualizar numero folio
                        try
                        {
                            using (SqlCommand commandUpdateFolio = new SqlCommand("AC_UpdateFolioJO", connection, transaction))
                            {
                                commandUpdateFolio.Parameters.AddWithValue("@FolioNuevoI", infoInsertJO.FolioNuevo);
                                commandUpdateFolio.Parameters.AddWithValue("@IdFolio", infoInsertJO.IdTomadoFolio);

                                commandUpdateFolio.CommandType = CommandType.StoredProcedure;
                                commandUpdateFolio.ExecuteNonQuery();
                                using (var readerJOU = commandUpdateFolio.ExecuteReader())
                                {
                                    while (readerJOU.Read())
                                    {
                                        infoInsertJO.FolioNuevoUpdate = int.Parse(readerJOU["FolioNuevoUpdated"].ToString());
                                    }
                                }
                            }
                        }
                        catch (SqlException ex)
                        {
                            Debug.WriteLine($"Error en el proceso almacenado1: {ex.Message}");
                            resultado.HayError = false;
                            resultado.MensajeError = $"Error en la insersion de Juicio oral {ex.Message}";

                            transaction.Rollback();
                            throw new Exception($"Ocurrió un error en el proceso de inserción. Mensaje de error: {ex.Message}" );


                        }

                        Debug.WriteLine("Update tabla P_folio ✔️" );

                        

                    }
                    transaction.Commit();
                    Debug.WriteLine("Transaccion Finalizada con exito");
                    resultado.HayError = true;
                    resultado.MensajeError = "Formulario enviado";

                }
                catch(Exception ex)
                {
                    resultado.HayError = false;
                    resultado.MensajeError = $"Error en la insersion de Juicio oral {ex.Message}";
                    // Realizar Rollback para cancelar la transacción
                    return resultado;
                    throw new Exception($"Ocurrió un error en el proceso de inserción. Mensaje de error: {ex.Message}");

                }
                

            }
                return resultado;
            //return true;
        } 
        
    }
}