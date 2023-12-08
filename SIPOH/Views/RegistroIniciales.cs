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
        public static string NumeroInicial
        {
            get { return GetString("NumeroInicial"); }
            set { SetString("NumeroInicial", value); }
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
                string query = "SELECT CA.IdAudiencia, IdAudi,  Descripcion FROM P_CatAudiencias CA JOIN P_CatAudiAsunto CAA ON CAA.IdAudiencia = CA.IdAudiencia WHERE TipoAsunto = @TipoAsunto";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TipoAsunto", Asunto);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string TipoSolicitud = reader["Descripcion"].ToString();
                            string IdAudiencia = reader["IdAudiencia"].ToString();

                            solicitudes.Add(TipoSolicitud);
                            ids.Add(IdAudiencia);
                            //Debug.WriteLine("TipoSolicitud: " + TipoSolicitud + " IdAudiencia: " + IdAudiencia);
                        }
                    }
                }
            }

            return (solicitudes, ids);
        }


        public static List<String> GetCatAnexos()
        {
            List<string> CatAnexos = new List<String>();
            using (SqlConnection connection = new ConexionBD().Connection)
            {
                connection.Open();
                string query = "SELECT * FROM P_CatAnexos";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Anexos = reader["descripcion"].ToString();
                            CatAnexos.Add(Anexos);
                            //Debug.WriteLine("CatAnexos" + Anexos);
                        }
                    }
                }
            }
            return CatAnexos;
        }

        public static (List<string> Delitos, List<string> IdDelitos) GetCatDelitos()
        {
            List<string> CatDelitos = new List<string>();
            List<string> CatIdDelitos = new List<string>();
            using (SqlConnection connection = new ConexionBD().Connection)
            {
                connection.Open();
                string query = "SELECT  IdDelito ,Nombre FROM P_CatDelitos";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Delitos = reader["Nombre"].ToString();
                            IdDelitos = reader["IdDelito"].ToString();
                            CatDelitos.Add(Delitos);
                            CatIdDelitos.Add(IdDelitos);
                            //Debug.WriteLine(Delitos);
                        }
                    }
                }
            }
            return (CatDelitos, CatIdDelitos);
        }

        //public static bool GetNumeroIniciales(string TipoInicial)
        //{
        //    using (SqlConnection connection = new ConexionBD().Connection)
        //    {
        //        connection.Open();
        //        SqlTransaction transaction = connection.BeginTransaction();
        //        try
        //        {
        //            using (SqlCommand command = new SqlCommand())
        //            {
        //                command.Connection = connection;
        //                command.Transaction = transaction;
        //                // Paso 1
        //                string IdJuzgado = HttpContext.Current.Session["IDJuzgado"] as string;
        //                string query = "SELECT Folio, IdFolio FROM P_Folios WHERE IdJuzgado = @IdJuzgado AND Tipo = @TipoInicial";
        //                using (SqlCommand selectCommand = new SqlCommand(query, connection))
        //                {
        //                    selectCommand.Parameters.AddWithValue("@IdJuzgado", IdJuzgado);
        //                    selectCommand.Parameters.AddWithValue("@TipoInicial", TipoInicial);
        //                    selectCommand.Transaction = transaction; // Mover la asignación de transacción aquí
        //                    using (SqlDataReader reader = selectCommand.ExecuteReader())
        //                    {
        //                        if (reader.Read())
        //                        {
        //                            HttpContext.Current.Session["IdFolio"] = reader["IdFolio"].ToString();
        //                            Debug.WriteLine("Folio: " + HttpContext.Current.Session["IdFolio"]);

        //                        }
        //                    }
        //                    // Folio
        //                    object numeroActual = selectCommand.ExecuteScalar();
        //                    Debug.WriteLine("Numero Obtenido Query: " + numeroActual);
        //                    // Nuevo número
        //                    int nuevoNumero = Convert.ToInt32(numeroActual) + 1;
        //                    int añoActual = DateTime.Now.Year;

        //                    // Formatea el número con ceros a la izquierda y agrega el año actual
        //                    string numeroFormateado = nuevoNumero.ToString("D4") + "/" + añoActual;

        //                    Debug.WriteLine("Tarea 1 Completada! Numero Siguiente: " + numeroFormateado);
        //                    HttpContext.Current.Session["NuevoFolio"] = numeroFormateado;

        //                    // Paso 2
        //                    string queryUpdate = "UPDATE P_Folios SET Folio = @NuevoNumero WHERE IdFolio = @IdFolio";
        //                    command.Parameters.AddWithValue("@IdFolio", Session["IdFolio"]);
        //                    command.Parameters.AddWithValue("@NuevoNumero", nuevoNumero);
        //                    command.CommandText = queryUpdate;
        //                    // No es necesario asignar la transacción aquí

        //                    // Ejecuta la consulta de actualización
        //                    command.ExecuteNonQuery();
        //                    Debug.WriteLine("Tarea 2 completada.");

        //                    transaction.Commit();
        //                    Debug.WriteLine("Transacción fue ejecutada!");
        //                    return true;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            // Si ocurre un error, revertir la transacción y manejar la excepción
        //            transaction.Rollback();
        //            Debug.WriteLine("Error: " + ex.Message);
        //            return false;
        //        }
        //        finally
        //        {
        //            connection.Close();
        //        }
        //    }
        //}

        //public static bool SendDelitosIniciales(List<int> listaIdDelito)
        //{
        //    using(SqlConnection connection = new ConexionBD().Connection)
        //    {
        //        connection.Open();

        //        foreach (int idDelito in listaIdDelito)
        //        {
        //            using (SqlCommand command = new SqlCommand())
        //            {
        //                command.Connection = connection;
        //                command.CommandText = "INSERT INTO P_AsuntoDelito(IdAsunto, IdDelito) VALUES (@IdAsunto, @IdDelito)";
        //                command.Parameters.AddWithValue("@IdAsunto", Session["UserId"]);
        //                command.Parameters.AddWithValue("@IdDelito", idDelito);

        //                command.ExecuteNonQuery();
        //            }
        //        }

        //    }
        //        return true;
        //}




        public static bool SendRegistroIniciales(string FeIngresoAsunto, string TipoAsunto, string IdAudiencia, string Observaciones, string QuienIngresa, string MP, string Prioridad, string Fojas, string TipoRadicacion, string NUC, List<int> listaIdDelito, List<Victima> usuarios, List<Imputado> culpados, List<Anexos> Anexos)
        {
            using (SqlConnection connection = new ConexionBD().Connection)
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.Transaction = transaction;



                        // Paso 1: Inserción en P_Asunto
                        command.CommandText = "INSERT INTO P_Asunto(Numero, IdJuzgado, FeIngreso, TipoAsunto, Digitalizado, FeCaptura, IdUsuario, IdAudiencia, Observaciones, QuienIngresa, MP, Prioridad, Fojas) OUTPUT INSERTED.IdAsunto VALUES ('0000/2023', @IdJuzgado, @FeIngreso, @TipoAsunto, '0', GETDATE(), @IdUsuario, @IdAudiencia, @Observaciones, @QuienIngresa, @MP, @Prioridad, @Fojas)";
                        //command.Parameters.AddWithValue("@NumeroInicial", NumeroInicial);
                        command.Parameters.AddWithValue("@IdJuzgado", Session["IDJuzgado"]);
                        command.Parameters.AddWithValue("@FeIngreso", FeIngresoAsunto);
                        command.Parameters.AddWithValue("@TipoAsunto", TipoAsunto);
                        //command.Parameters.AddWithValue("@Digitalizado", Digitalizado);
                        //command.Parameters.AddWithValue("@FeCaptura", FeCaptura);
                        command.Parameters.AddWithValue("@IdUsuario", Session["IdUsuario"]);
                        command.Parameters.AddWithValue("@IdAudiencia", IdAudiencia);
                        command.Parameters.AddWithValue("@Observaciones", Observaciones);
                        command.Parameters.AddWithValue("@QuienIngresa", QuienIngresa);
                        command.Parameters.AddWithValue("@MP", MP);
                        command.Parameters.AddWithValue("@Prioridad", Prioridad);
                        command.Parameters.AddWithValue("@Fojas", Fojas);
                        object insertedId = command.ExecuteScalar();
                        Session["UserId"] = insertedId;

                        Debug.WriteLine("Tarea 1 completada ✔️");

                        //// Paso 2: Inserción en P_Causa
                        command.CommandText = "INSERT INTO P_Causa(IdAsunto, TipoRadicacion, Remision, IdRazonIncom, FeImcompeten, IncomEntidad, IncomJuzgado, Acumulada, NUC) VALUES (@IdAsunto, @TipoRadicacion, 'N', NULL, NULL, NULL, NULL, 'N', @NUC)";
                        command.Parameters.AddWithValue("@IdAsunto", insertedId);
                        command.Parameters.AddWithValue("@TipoRadicacion", TipoRadicacion);
                        command.Parameters.AddWithValue("@NUC", NUC);
                        command.ExecuteNonQuery();
                        Debug.WriteLine("Tarea 2 completada ✔️");

                        // Paso 3: Inserción en P_AsuntoDelito


                        command.CommandText = "INSERT INTO P_AsuntoDelito(IdAsunto, IdDelito) VALUES (@IdAsunto, @IdDelito)";
                        SqlParameter paramIdDelito = command.Parameters.AddWithValue("@IdDelito", 0); // @IdDelito será establecido más adelante
                        foreach (int idDelito in listaIdDelito) // recorre lista de los IdDelito que deseas insertar
                        {
                            // IdAsunto que obtuvo anteriormente
                            paramIdDelito.Value = idDelito;
                            // Ejecutar la consulta
                            command.ExecuteNonQuery();
                        }
                        Debug.WriteLine("Tarea 3 completada ✔️");

                        // Paso 4: Inserción en P_PartesAsunto
                        command.CommandText = "INSERT INTO P_PartesAsunto(IdAsunto, Nombre, APaterno, AMaterno, Genero, TipoParte, Alias) VALUES (@IdAsunto, @NombreVictimas, @ApellidoPaternoVictima, @ApellidoMaternoVictima, @GeneroVictima, 'V', NULL)";
                        foreach (var usuario in usuarios)
                        {
                            // Configurar el comando con la consulta SQL
                            command.Parameters.Clear(); // Limpiar los parámetros antes de cada iteración
                                                        // Configurar los parámetros con los valores del usuario actual

                            command.Parameters.AddWithValue("@IdAsunto", Session["UserId"]);
                            command.Parameters.AddWithValue("@NombreVictimas", usuario.Nombre);
                            command.Parameters.AddWithValue("@ApellidoPaternoVictima", usuario.ApellidoPaterno);
                            command.Parameters.AddWithValue("@ApellidoMaternoVictima", usuario.ApellidoMaterno);
                            command.Parameters.AddWithValue("@GeneroVictima", usuario.Genero);

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
                            command.Parameters.AddWithValue("@NombreImputado", culpado.NombreCulpado);
                            command.Parameters.AddWithValue("@ApellidoPaternoImputado", culpado.APCulpado);
                            command.Parameters.AddWithValue("@ApellidoMaternoImputado", culpado.AMCulpado);
                            command.Parameters.AddWithValue("@GeneroImputado", culpado.GeneroCulpado);
                            command.Parameters.AddWithValue("@AliasImputado", culpado.AliasCulpado);
                            //Ejecutar consulta
                            command.ExecuteNonQuery();
                        }
                        Debug.WriteLine("Tarea 4 completado - Imputado ✔️");


                        // Paso 5: Inserción en P_Anexos
                        command.CommandText = "INSERT INTO P_Anexos(IdAsunto, Descripcion, Cantidad) VALUES (@IdAsunto, @Descripcion, @Cantidad)";
                        foreach (var anexo in Anexos)
                        {
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@IdAsunto", Session["UserId"]);
                            command.Parameters.AddWithValue("@Descripcion", anexo.DescripcionAnexo);
                            command.Parameters.AddWithValue("@Cantidad", anexo.CantidadAnexo);
                            //command.Parameters.AddWithValue("@Tipo", anexo.Tipo);
                            command.ExecuteNonQuery();
                        }
                        Debug.WriteLine("Paso 7 completado ✔️");
                        //// Paso 5: Inserción en P_Posterior
                        //command.CommandText = "INSERT INTO P_Posterior(IdAsunto, Promovente, FechaIngreso, Digitalizado, IdUsuario, TipoPromocion, FechaRecepcion) VALUES (@IdAsunto, 'Nombre Prueba', GETDATE(), 'N', 5, 'p', '2002/2/2')";
                        //command.ExecuteNonQuery();
                        //Debug.WriteLine("Tarea 5 completado.");

                        //// Paso 6: Obtener el IdPosterior
                        //command.CommandText = "SELECT SCOPE_IDENTITY() AS IdPosterior";
                        //object idPosterior = command.ExecuteScalar();
                        //Debug.WriteLine("Paso 6 completado. IdPosterior: " + idPosterior);
                    }

                    // Confirmar la transacción si todo ha ido bien
                    transaction.Commit();
                    Debug.WriteLine("Transacción completada con éxito.");
                    return true;
                }
                catch (Exception ex)
                {
                    // Si ocurre un error, revertir la transacción y manejar la excepción
                    transaction.Rollback();
                    Debug.WriteLine("Error: " + ex.Message);
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
        }





    }




}
