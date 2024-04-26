using DatabaseConnection;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System;
using System.Web.SessionState;
using System.Web;

using System.Linq;
using Microsoft.Ajax.Utilities;
using System.Data;
using static SIPOH.Views.CustomRegistroIniciales;
using System.Web.UI;
using Antlr.Runtime.Tree;

namespace SIPOH
{
    public class RegistroIniciales 
    {
        
        private static HttpSessionState Session => HttpContext.Current.Session;
        

        private static string GetString(string key)
        {
            return Session[key] as string;
        }

        private static void SetString(string key, string value)
        {
            Session[key] = value;
        }
        





        public static string TipoSolicitud
        {
            get { return GetString("TipoSolicitud"); }
            set { SetString("TipoSolicitud", value); }
        }
        public static string IdDelitos
        {
            get { return GetString("IdDelitos"); }
            set { SetString("IdDelitos", value); }
        }
        public static string FeIngresoAsunto
        {
            get { return GetString("FechaRecepcion"); }
            set { SetString("FechaRecepcion", value); }
        }
        public static string NumeroFojas
        {
            get { return GetString("NumeroFojas"); }
            set { SetString("NumeroFojas", value); }
        }

        public static string QuienIngresa
        {
            get { return GetString("QuienIngresa"); }
            set { SetString("QuienIngresa", value); }
        }

        public static string Prioridad
        {
            get { return GetString("Prioridad"); }
            set { SetString("Prioridad", value); }
        }
        public static string Anexos
        {
            get { return GetString("Anexos"); }
            set { SetString("Anexos", value); }
        }
        public static string Delitos
        {
            get { return GetString("Delitos"); }
            set { SetString("Delitos", value); }
        }
        public static string TipoAsunto
        {
            get { return GetString("TipoAsunto"); }
            set { SetString("TipoAsunto", value); }
        }
        public static string IdAudiencia
        {
            get { return GetString("IdAudiencia"); }
            set { SetString("IdAudiencia", value); }
        }
        public static string Observaciones
        {
            get { return GetString("Observaciones"); }
            set { SetString("Observaciones", value); }
        }
        public static string MP
        {
            get { return GetString("MP"); }
            set { SetString("MP", value); }
        }
        public static string Fojas
        {
            get { return GetString("Fojas"); }
            set { SetString("Fojas", value); }
        }
        public static string NumeroAsignado
        {
            get { return GetString("NumeroAsignado"); }
            set { SetString("NumeroAsignado", value); }
        }public static string Exepciones
        {
            get { return GetString("Exepciones"); }
            set { SetString("Exepciones", value); }
        }



        public class CatDelitosResult
        {
            public List<string> Delitos { get; set; }
            public List<string> IdDelitos { get; set; }
        }



        //obtencion de catalogos 
        public class TipoSolicitudData
        {
            public List<string> Solicitudes { get; set; }
            public List<string> Ids { get; set; }
            public List<string> IdDelitos { get; set; }
        }
        
        public static (List<string> solicitudes, List<string> ids) GetTipoSolicitud(string Asunto)
        {
            List<string> solicitudes = new List<string>();
            List<string> ids = new List<string>();

            using (SqlConnection connection = new ConexionBD().Connection)
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetTipoSolicitud", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TipoAsunto", Asunto);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string TipoSolicitud = reader["Descripcion"].ToString();
                            string IdAudiencia = reader["IdAudiencia"].ToString();

                            solicitudes.Add(TipoSolicitud);
                            ids.Add(IdAudiencia);
                            //Debug.WriteLine("Los datos de tipo de asunto son: " + TipoSolicitud + IdAudiencia);
                        }
                    }
                }
            }

            return (solicitudes, ids);
        }



        public static List<string> GetCatAnexos()
        {
            List<string> CatAnexos = new List<string>();

            using (SqlConnection connection = new ConexionBD().Connection)
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetCatAnexos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string Anexo = reader["descripcion"].ToString();
                            CatAnexos.Add(Anexo);
                        }
                    }
                }
            }

            return CatAnexos;
        }
        public static CatDelitosResult GetCatDelitos()
        {
            List<string> CatDelitos = new List<string>();
            List<string> CatIdDelitos = new List<string>();

            using (SqlConnection connection = new ConexionBD().Connection)
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("GetCatDelitos", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string Delito = reader["Nombre"].ToString();
                            string IdDelito = reader["IdDelito"].ToString();

                            CatDelitos.Add(Delito);
                            CatIdDelitos.Add(IdDelito);
                        }
                    }
                }
            }

            return new CatDelitosResult
            {
                Delitos = CatDelitos,
                IdDelitos = CatIdDelitos
            };
        }



        
        public static bool UpdateBuzonSalida(int IdSolicitudBuzon, DateTime CapturaActual, string Estatus)
        {
            using(SqlConnection connection = new ConexionBD().Connection)
            {
                try
                {
                    connection.Open();
                    using(SqlCommand command = new SqlCommand("UPDATE P_BuzonSolicitud SET IdAsunto = @IdAsunto, FeAceptacion =  @FeAceptacion , Estatus = @Estatus WHERE IdSolicitudBuzon = @IdSolicitudBuzon", connection))
                    {
                        command.Parameters.AddWithValue("@IdSolicitudBuzon", IdSolicitudBuzon);
                        command.Parameters.AddWithValue("@FeAceptacion", CapturaActual);
                        command.Parameters.AddWithValue("@Estatus", Estatus);
                        command.Parameters.AddWithValue("@IdAsunto", Session["UserId"]);
                        command.ExecuteNonQuery();

                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Surgio un problema en actualizar BuzonSalida: " + ex.Message);
                }

            }
            return true;
        }
        public static bool SendRegistroIniciales(DateTime FeCaptura, int Actividad,string FeIngresoAsunto, string TipoAsunto, string Digitalizado ,string IdAudiencia, string Observaciones, string QuienIngresa, string MP, string Prioridad, string Fojas, string TipoRadicacion, string NUC, List<CatDelito> listaIdDelito, List<Victima> usuarios, List<Imputado> culpados, List<Anexos> Anexos)
        {
            using (SqlConnection connection = new ConexionBD().Connection)
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {   
                    //obtener folio para ser asignado a una Inicial
                    //Paso 0 insert AsignarFolio
                    using (SqlCommand command2 = new SqlCommand("AC_AsignarFolio", connection, transaction))
                    {
                       command2.Parameters.AddWithValue("@IdJuzgado", Session["IDJuzgado"]);
                       command2.Parameters.AddWithValue("@TipoDocumento", TipoAsunto);
                       
                       command2.CommandType = CommandType.StoredProcedure;
                        using (var reader = command2.ExecuteReader())
                        {
                           if (reader.Read())
                           {
                               //Folio nuevo 
                                var FolioNuevo = reader["FolioNuevo"];
                                HttpContext.Current.Session["IdFolioInicial"] = reader["IdFolio"];
                                HttpContext.Current.Session["FolioNuevoPInicial"] = FolioNuevo;

                                int folio = Convert.ToInt32(FolioNuevo);
                                
                                ///Convierte el valor a int
                                int añoActual = DateTime.Now.Year;
                                string NumeroAsignado = folio.ToString("D4") + "/" + añoActual;
                                HttpContext.Current.Session["FolioNuevoInicial"] = NumeroAsignado;
                                
                                
                                




                           }
                        }
                        
                    }
                    Debug.WriteLine("Inicio Procedimiento: ✔️ Obtener Folio");
                    // Confirmar la transacción si todo ha ido bien
                    using (SqlCommand command3 = new SqlCommand("AC_verificar_disponibilidad_folio_en_PAsunto", connection, transaction))
                    {
                        command3.Parameters.AddWithValue("@NuevoNumero", Session["FolioNuevoInicial"]);
                        command3.Parameters.AddWithValue("@IdJuzgado", Session["IDJuzgado"]);
                        command3.Parameters.AddWithValue("@TipoAsunto", TipoAsunto);
                        command3.CommandType = CommandType.StoredProcedure;

                        try
                        {
                            using (var reader = command3.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    var IdAsuntoDuplicado = reader["IdAsunto"];

                                    if (IdAsuntoDuplicado != DBNull.Value && Convert.ToInt32(IdAsuntoDuplicado) != 0)
                                    {
                                        int idAsuntoDuplicado = Convert.ToInt32(IdAsuntoDuplicado);
                                        Debug.WriteLine("Error: Número de Asunto duplicado. IdAsunto: " + idAsuntoDuplicado);
                                        // Aquí puedes manejar el caso de duplicado, por ejemplo, lanzar una excepción o tomar alguna acción específica.
                                        // Realizar Rollback para cancelar la transacción
                                        transaction.Rollback();
                                    }
                                    else
                                    {
                                        // No hay duplicado, puedes realizar otras acciones aquí.
                                    }
                                }
                            }
                        }
                        catch (SqlException ex)
                        {
                            // Manejar la excepción aquí
                            // ex.Message contendrá el mensaje de error lanzado desde el procedimiento almacenado
                            // Puedes agregar lógica específica para tratar el caso de duplicados
                            Debug.WriteLine("Error al ejecutar el procedimiento almacenado: " + ex.Message);
                            Exepciones = "Error en consulta";
                            


                            // Realizar Rollback para cancelar la transacción
                            transaction.Rollback();
                        }
                    }


                    Debug.WriteLine("✔️ Consulta Folio  ");
                    using (SqlCommand command = new SqlCommand())
                    {

                        command.Connection = connection;
                        command.Transaction = transaction;
                        // Paso 1: Inserción en P_Asunto
                        command.CommandText = "INSERT INTO P_Asunto(Numero, IdJuzgado, FeIngreso, TipoAsunto, Digitalizado, FeCaptura, IdUsuario, IdAudiencia, Observaciones, QuienIngresa, MP, Prioridad, Fojas) OUTPUT INSERTED.IdAsunto VALUES (@NumeroDocumento, @IdJuzgado, @FeIngreso, @TipoAsunto, @Digitalizado, @FechaCaptura, @IdUsuario, @IdAudiencia, @Observaciones, @QuienIngresa, @MP, @Prioridad, @Fojas)";
                        command.Parameters.AddWithValue("@NumeroDocumento", Session["FolioNuevoInicial"]);
                        command.Parameters.AddWithValue("@IdJuzgado", Session["IDJuzgado"]);
                        command.Parameters.AddWithValue("@FeIngreso", FeIngresoAsunto);
                        command.Parameters.AddWithValue("@TipoAsunto", TipoAsunto.ToUpper());
                        command.Parameters.AddWithValue("@Digitalizado", Digitalizado);
                        command.Parameters.AddWithValue("@FechaCaptura", FeCaptura);
                        command.Parameters.AddWithValue("@IdUsuario", Session["IdUsuario"]);
                        command.Parameters.AddWithValue("@IdAudiencia", IdAudiencia);
                        command.Parameters.AddWithValue("@Observaciones", Observaciones.ToUpper());
                        command.Parameters.AddWithValue("@QuienIngresa", QuienIngresa.ToUpper());
                        command.Parameters.AddWithValue("@MP", MP.ToUpper());
                        command.Parameters.AddWithValue("@Prioridad", Prioridad.ToUpper());
                        command.Parameters.AddWithValue("@Fojas", Fojas);
                        object insertedId = command.ExecuteScalar();
                        Session["UserId"] = insertedId;
                        
                        
                        Debug.WriteLine("Tarea 1 completada ✔️");

                        //// Paso 2: Inserción en P_Causa
                        command.CommandText = "INSERT INTO P_Causa(IdAsunto, TipoRadicacion, Remision, IdRazonIncom, FeImcompeten, IncomEntidad, IncomJuzgado, Acumulada, NUC) VALUES (@IdAsunto, @TipoRadicacion, 'N', NULL, NULL, NULL, NULL, 'N', @NUC)";
                        command.Parameters.AddWithValue("@IdAsunto", insertedId);
                        command.Parameters.AddWithValue("@TipoRadicacion", TipoRadicacion.ToUpper());
                        command.Parameters.AddWithValue("@NUC", NUC.ToUpper());
                        command.ExecuteNonQuery();
                        Debug.WriteLine("Tarea 2 completada ✔️");

                        // Paso 3: Inserción en P_AsuntoDelito

                        ////SE AÑADIO 2 LINEAS DE CODIGO  PARA LIMPIAR LOS PARAMETROS y IDASUNTO !IMPORTANTE PARA PODER REGISTRAR UNA INICIAL
                        command.CommandText = "INSERT INTO P_AsuntoDelito(IdAsunto, IdDelito) VALUES (@IdAsunto, @IdDelito)";
                        foreach (var Delito in listaIdDelito) // recorre lista de los IdDelito que deseas insertar
                        {
                            command.Parameters.Clear();
                            // IdAsunto que obtuvo anteriormente
                            command.Parameters.AddWithValue("@IdAsunto", Session["UserId"]);
                            command.Parameters.AddWithValue("@IdDelito", Delito.IdDelito); // @IdDelito será establecido más adelante
                            
                            // Ejecutar la consulta
                            command.ExecuteNonQuery();
                        }
                        ////SE AÑADIO 2 LINEAS DE CODIGO  PARA LIMPIAR LOS PARAMETROS y IDASUNTO !IMPORTANTE PARA PODER REGISTRAR UNA INICIAL
                        Debug.WriteLine("Tarea 3 completada ✔️");

                        // Paso 4: Inserción en P_PartesAsunto
                        command.CommandText = "INSERT INTO P_PartesAsunto(IdAsunto, Nombre, APaterno, AMaterno, Genero, TipoParte, Alias) VALUES (@IdAsunto, @NombreVictimas, @ApellidoPaternoVictima, @ApellidoMaternoVictima, @GeneroVictima, 'V', NULL)";
                        foreach (var usuario in usuarios)
                        {
                            // Configurar el comando con la consulta SQL
                            command.Parameters.Clear(); // Limpiar los parámetros antes de cada iteración
                                                        // Configurar los parámetros con los valores del usuario actual

                            command.Parameters.AddWithValue("@IdAsunto", Session["UserId"]);
                            command.Parameters.AddWithValue("@NombreVictimas", usuario.Nombre.ToUpper());
                            command.Parameters.AddWithValue("@ApellidoPaternoVictima", usuario.ApellidoPaterno.ToUpper());
                            command.Parameters.AddWithValue("@ApellidoMaternoVictima", usuario.ApellidoMaterno.ToUpper());
                            command.Parameters.AddWithValue("@GeneroVictima", usuario.Genero.ToUpper());

                            // Ejecutar la consulta
                            command.ExecuteNonQuery();
                        }

                        Debug.WriteLine("Tarea 4 completado - Victima ✔️");

                        //List<Imputado> culpados = new List<Imputado>();
                        command.CommandText = "INSERT INTO P_PartesAsunto(IdAsunto, Nombre, APaterno, AMaterno, Genero, TipoParte, Alias) VALUES (@IdAsunto, @NombreImputado, @ApellidoPaternoImputado, @ApellidoMaternoImputado, @GeneroImputado, 'I', @AliasImputado)";
                        foreach (var culpado in culpados)
                        {
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@IdAsunto", Session["UserId"]);
                            command.Parameters.AddWithValue("@NombreImputado", culpado.NombreCulpado.ToUpper());
                            command.Parameters.AddWithValue("@ApellidoPaternoImputado", culpado.APCulpado.ToUpper());
                            command.Parameters.AddWithValue("@ApellidoMaternoImputado", culpado.AMCulpado.ToUpper());
                            command.Parameters.AddWithValue("@GeneroImputado", culpado.GeneroCulpado.ToUpper());
                            command.Parameters.AddWithValue("@AliasImputado", culpado.AliasCulpado.ToUpper());
                            //Ejecutar consulta
                            command.ExecuteNonQuery();
                        }
                        Debug.WriteLine("Tarea 4 completado - Imputado ✔️");


                        // Paso 5: Inserción en P_Anexos
                        command.CommandText = "INSERT INTO P_Anexos(IdAsunto, Descripcion, Cantidad, Digitalizado) VALUES (@IdAsunto, @Descripcion, @Cantidad, @Digitalizado)";
                        foreach (var anexo in Anexos)
                        {
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@IdAsunto", Session["UserId"]);
                            command.Parameters.AddWithValue("@Descripcion", anexo.DescripcionAnexo.ToUpper());
                            command.Parameters.AddWithValue("@Cantidad", anexo.CantidadAnexo);
                            command.Parameters.AddWithValue("@FolioNuevo", 0);
                            command.Parameters.AddWithValue("@Digitalizado", anexo.Digitalizado);
                            //command.Parameters.AddWithValue("@Tipo", anexo.Tipo);
                            command.ExecuteNonQuery();
                        }
                        Debug.WriteLine("Tarea 7 completado ✔️");
                        // Paso 5: Inserción en P_Posterior

                        command.CommandText = "INSERT INTO P_Trayecto(IdAsunto, IdActividad, IdPerfil,IdUsuario, FeAsunto, Tipo, FeRecepcion, IdatividadProvienede, IdUsuarioProvienede,IdPerfilProvienede,Estado) VALUES (@IdAsunto,@Actividad, @IdPerfil,@IdUsuario,@FeAsunto, 'I', GETDATE(), NULL, NULL,NULL, 'A' )";


                        command.Parameters.AddWithValue("@IdPerfil", Session["IdPerfil"]);
                        command.Parameters.AddWithValue("@IdUsuario", Session["IdUsuario"]);
                        command.Parameters.AddWithValue("@FeAsunto", FeIngresoAsunto);
                        command.Parameters.AddWithValue("@Actividad", Actividad);

                        command.ExecuteNonQuery();
                        Debug.WriteLine("Tarea 8 completado ✔️" + Session["FolioNuevoPInicial"] + " "+ Session["IdFolioInicial"]);


                        using (SqlCommand command4 = new SqlCommand("AC_UpdateFolio", connection, transaction))
                        {
                            // valores de Session al tipo de datos correcto
                            int folioNuevo = Convert.ToInt32(Session["FolioNuevoPInicial"]);
                            int idFolio = Convert.ToInt32(Session["IdFolioInicial"]);
                            int idJuzgado = Convert.ToInt32(Session["IDJuzgado"]);

                            command4.Parameters.AddWithValue("@FolioNuevoI", Session["FolioNuevoPInicial"]);
                            command4.Parameters.AddWithValue("@IdFolio", idFolio);
                            command4.Parameters.AddWithValue("@IdJuzgado", idJuzgado);
                            command4.CommandType = CommandType.StoredProcedure;


                            command4.ExecuteNonQuery();

                            // Recuperar el valor de salida si es necesario
                            

                            Debug.WriteLine("Tarea 9 completado ✔️");
                            //Debug.WriteLine("Folio actualizado: " + folioNuevoActualizado);
                        }
                    }


                    transaction.Commit();
                        Debug.WriteLine("Transacción completada con éxito.");
                        return true;

                } 
                catch (Exception ex)
                {
                    // Si ocurre un error, revertir la transacción y manejar la excepción
                    Exepciones = "Error en consulta";
                    transaction.Rollback();
                    Debug.WriteLine("Error Transaction: " + ex.Message);

                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        


        public static DataTable GetInicial(List<BusquedaInicial> BusquedaInicial)
        {
            using (SqlConnection connection = new ConexionBD().Connection)
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    string IdJuzgado = HttpContext.Current.Session["IDJuzgado"] as string;

                    using (SqlCommand command = new SqlCommand("P_GetInicial", connection, transaction))
                    {
                        command.Parameters.AddWithValue("@IDJuzgado", IdJuzgado);
                        command.Parameters.AddWithValue("@DataImputado", BusquedaInicial.Select(b => b.DataImputado).FirstOrDefault());
                        command.Parameters.AddWithValue("@DataVictima", BusquedaInicial.Select(b => b.DataVictima).FirstOrDefault());
                        command.Parameters.AddWithValue("@DataNUC", BusquedaInicial.Select(b => b.DataNUC).FirstOrDefault());
                        
                        
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            DataTable dataTable = new DataTable();
                            dataTable.Load(reader);

                            return dataTable;
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Debug.WriteLine("Error en Obtener Inicial: " + ex.Message);
                    return null;
                }
                finally
                {
                    transaction.Dispose();
                    connection.Close();
                }
            }
        }





        //public static bool GetInicial(List<BusquedaInicial> BusquedaInicial)
        //{

        //        using (SqlConnection connection = new ConexionBD().Connection)
        //        {
        //            connection.Open();

        //        try
        //        {
        //            string IdJuzgado = HttpContext.Current.Session["IDJuzgado"] as string;
        //            using (SqlCommand command = new SqlCommand("P_GetInicial", connection))
        //            {
        //                command.Parameters.AddWithValue("@IDJuzgado", IdJuzgado);
        //                command.Parameters.AddWithValue("@DataImputado",DataImputado);

        //                command.Parameters.AddWithValue("@DataVictima", DataVictima);
        //                command.Parameters.AddWithValue("@DataNUC", DataNUC);
        //                command.CommandType = CommandType.StoredProcedure;

        //                using (SqlDataReader reader = command.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        string tipoAsunto = reader["TipoAsunto"].ToString();
        //                        string numero = reader["Numero"].ToString();
        //                        string nuc = reader["Delitos"].ToString();
        //                        string inculpados = reader["Inculpados"].ToString();
        //                        string victimas = reader["Victimas"].ToString();
        //                        Debug.WriteLine("Datos de tu consulta son los siguientes: "+ tipoAsunto + numero+ nuc +inculpados+victimas);
        //                    }

        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Debug.WriteLine("Error en Obtener Inicial: " + ex);
        //        }
        //    }
        //    return true;
        //}



    }




}
