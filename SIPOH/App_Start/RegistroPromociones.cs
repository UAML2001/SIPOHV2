
using DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.SessionState;
using System.Web;
using System.Web.UI;
using static SIPOH.Views.CustomRegistroIniciales;  
using System.Net.NetworkInformation;
using System.Diagnostics;
using SIPOH.Views;
using Microsoft.Ajax.Utilities;
using SIPOH.Controllers.AC_Digitalizacion;

namespace SIPOH
{
    class RegistroPromociones
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
        public static string Anexo
        {
            get { return GetString("Anexo"); }
            set { SetString("Anexo", value); }
        }
        public static string DataNumero
        {
            get { return GetString("DataNumero"); }
            set { SetString("DataNumero", value); }
        }
        public static string DataTipoAsunto
        {
            get { return GetString("DataTipoAsunto"); }
            set { SetString("DataTipoAsunto", value); }
        }
        public static bool GetDataPromocion(string DataNumero,string DataTipoAsunto )
        {
            using (SqlConnection connection = new ConexionBD().Connection)
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string IdJuzgado = HttpContext.Current.Session["IDJuzgado"] as string;
                    using (SqlCommand command = new SqlCommand("P_GetPromocion", connection, transaction))
                    {
                        command.Parameters.AddWithValue("@IdJuzgado", IdJuzgado);
                        

                        command.Parameters.AddWithValue("@DataNumero",DataNumero);
                        command.Parameters.AddWithValue("@DataTipoAsunto", DataTipoAsunto);
                        command.CommandType = CommandType.StoredProcedure;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                HttpContext.Current.Session["TipoAsuntoPromocion"] = reader["TipoAsunto"].ToString();
                                HttpContext.Current.Session["NumeroPromocion"] = reader["Numero"].ToString();
                                HttpContext.Current.Session["NUCPromocion"] = reader["NUC"].ToString();
                                HttpContext.Current.Session["DelitosPromocion"] = reader["Delitos"].ToString();
                                HttpContext.Current.Session["InculpadosPromocion"] = reader["Inculpados"].ToString();
                                HttpContext.Current.Session["VictimasPromocion"] = reader["Victimas"].ToString();
                                HttpContext.Current.Session["IdAsuntoPromocion"] = reader["IdAsunto"].ToString();
                                HttpContext.Current.Session["NumeroAmparoPromocion"] = reader["NumeroAmparo"].ToString();
                                HttpContext.Current.Session["AutoridadResponsablePromocion"] = reader["AutoridadResponsable"].ToString();
                                HttpContext.Current.Session["EstatusPromocion"] = reader["Estatus"].ToString();
                                HttpContext.Current.Session["EtapaPromocion"] = reader["Etapa"].ToString();                                   
                                

                            }
                        }
                    }
                    
                }catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                    return true;
            }
        }

        public static int Equipo;
        public static int idPosterior;

        public static bool UpdateBuzonSalidaPromocion(int IdSolicitudBuzon, DateTime CapturaActual, string Estatus, string TipoDocumento)
        {
                using (SqlConnection connection = new ConexionBD().Connection)
                {
                SqlTransaction transaction = null;
                    try
                    {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    using (SqlCommand command = new SqlCommand("UPDATE P_BuzonSolicitud SET IdAsunto = @IdAsunto, FeAceptacion = @FeAceptacion, Estatus = @Estatus WHERE IdSolicitudBuzon = @IdSolicitudBuzon", connection, transaction))
                    {
                        command.Parameters.AddWithValue("@IdSolicitudBuzon", IdSolicitudBuzon);
                        command.Parameters.AddWithValue("@FeAceptacion", CapturaActual);
                        command.Parameters.AddWithValue("@Estatus", Estatus);
                        command.Parameters.AddWithValue("@IdAsunto", Session["IdAsuntoPromocion"]);
                        command.ExecuteNonQuery();
                    }


                    using (SqlCommand command2 = new SqlCommand("INSERT INTO P_Documentos(IdAsunto, IdPosterior, FechaDigitaliza, IdUsuarios, URL, NombrePDF, Descripcion) VALUES(@IdAsunto, @IdPosterior, @FechaDigitaliza, @IdUsuarios, @URL, @NombrePDF, @Descripcion);", connection, transaction))
                    {
                        command2.Parameters.Add("@IdAsunto", SqlDbType.Int).Value = Session["IdAsuntoPromocion"];
                        command2.Parameters.Add("@IdPosterior", SqlDbType.Int).Value = idPosterior;
                        command2.Parameters.Add("@FechaDigitaliza", SqlDbType.DateTime).Value = CapturaActual;
                        command2.Parameters.Add("@IdUsuarios", SqlDbType.Int).Value = Session["IdUsuario"];
                        command2.Parameters.Add("@URL", SqlDbType.NVarChar).Value = BuzonControl.urlBuzonDigital;
                        command2.Parameters.Add("@NombrePDF", SqlDbType.NVarChar).Value = BuzonControl.documentoBuzonDigital;
                        command2.Parameters.Add("@Descripcion", SqlDbType.NVarChar).Value = TipoDocumento.ToUpper();
                        command2.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
            catch (Exception ex)
            {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }
                    throw new Exception("Surgió un problema en actualizar BuzonSalida: " + ex.Message);
                }
            }
            return true;
        }


        public static bool SendRegistroPromocion(List<DataPromocion> InfoPromocion, List<AnexosPromocion> AnexosP)
        {
            using (SqlConnection connection = new ConexionBD().Connection)
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (SqlCommand command = new SqlCommand("AC_AsignacionCargaTrabajo", connection, transaction))
                    {
                        command.Parameters.Add("@IdJuzgado", SqlDbType.Int).Value = Session["IDJuzgado"].ToString();
                        command.Parameters.Add("@TipoAsunto", SqlDbType.VarChar).Value = InfoPromocion.Select(b => b.TipoDocumento).FirstOrDefault().ToUpper();
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Equipo = int.Parse(reader["Equipo"].ToString());
                                Debug.WriteLine("EQUIPO: " + Equipo);
                            }
                        }
                    }

                    using (SqlCommand command = new SqlCommand("AC_InsertarPromocion", connection, transaction))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@IdAsunto", Session["IdAsuntoPromocion"]);
                        command.Parameters.AddWithValue("@Promovente", InfoPromocion.Select(b => b.Promovente).FirstOrDefault().ToUpper());
                        command.Parameters.AddWithValue("@Digitalizado", InfoPromocion.Select(b => b.Digitalizado).FirstOrDefault().ToUpper());
                        command.Parameters.AddWithValue("@IdUsuario", Session["IdUsuario"]);
                        command.Parameters.AddWithValue("@FechaIngreso", InfoPromocion.Select(b => b.FechaIngreso).FirstOrDefault());
                        command.Parameters.AddWithValue("@TipoPromocion", InfoPromocion.Select(b => b.TipoPromocion).FirstOrDefault().ToUpper());
                        command.Parameters.AddWithValue("@FechaRecepcion", InfoPromocion.Select(b => b.FechaRecepcion).FirstOrDefault());
                        command.Parameters.AddWithValue("@Tipo", InfoPromocion.Select(b => b.Tipo).FirstOrDefault().ToUpper());
                        command.Parameters.AddWithValue("@IdActividad", InfoPromocion.Select(b => b.IdActividad).FirstOrDefault());
                        command.Parameters.AddWithValue("@IdPerfil", Session["IdPerfil"]);
                        command.Parameters.AddWithValue("@FeAsunto", InfoPromocion.Select(b => b.FeAsunto).FirstOrDefault());
                        command.Parameters.AddWithValue("@Estado", InfoPromocion.Select(b => b.EstadoPromocion).FirstOrDefault().ToUpper());
                        command.Parameters.AddWithValue("@Equipo", SqlDbType.Int).Value = Equipo;

                        SqlParameter idPosteriorParam = new SqlParameter("@IDPosterior", SqlDbType.Int);
                        idPosteriorParam.Direction = ParameterDirection.Output;
                        command.Parameters.Add(idPosteriorParam);
                        command.ExecuteNonQuery();
                        idPosterior = Convert.ToInt32(idPosteriorParam.Value);

                        // Guardar idPosterior en la sesión
                        Session["IdPosterior"] = idPosterior;
                    }

                    foreach (var anexo in AnexosP)
                    {
                        using (SqlCommand command = new SqlCommand("AC_InsertarAnexo", connection, transaction))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@IdAsunto", Session["IdAsuntoPromocion"]);
                            command.Parameters.AddWithValue("@IdPosterior", idPosterior);
                            command.Parameters.AddWithValue("@Descripcion", anexo.DescripcionAnexo.ToUpper());
                            command.Parameters.AddWithValue("@Cantidad", anexo.CantidadAnexo);
                            command.Parameters.AddWithValue("@Digitalizado", anexo.DigitalizadoAnexo.ToUpper());
                            command.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    Debug.WriteLine("Registro de promoción fue correcta!");
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Debug.WriteLine("Error SQL: " + ex.Message);
                    throw new Exception("Surgió un problema en registrar la promoción: " + ex.Message);
                }
            }
        }



        public static List<string> GetCatAnexo()
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
    }
    //Session["IdAsuntoPromocion"]
}