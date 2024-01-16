
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
                                HttpContext.Current.Session["TipoAsuntoPromocion"] = reader["TipoAsunto"];
                                HttpContext.Current.Session["NumeroPromocion"] = reader["Numero"];
                                //HttpContext.Current.Session["NUCPromocion"] = reader["NUC"];
                                HttpContext.Current.Session["DelitosPromocion"] = reader["Delitos"];
                                HttpContext.Current.Session["InculpadosPromocion"] = reader["Inculpados"];
                                HttpContext.Current.Session["VictimasPromocion"] = reader["Victimas"];
                                HttpContext.Current.Session["IdAsuntoPromocion"] = reader["IdAsunto"];
                                HttpContext.Current.Session["NumeroAmparoPromocion"] = reader["NumeroAmparo"];
                                HttpContext.Current.Session["AutoridadResponsablePromocion"] = reader["AutoridadResponsable"];
                                HttpContext.Current.Session["EstatusPromocion"] = reader["Estatus"];
                                HttpContext.Current.Session["EtapaPromocion"] = reader["Etapa"];                                   
                                

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
        
        public static bool SendRegistroPromocion( List<DataPromocion> InfoPromocion, List<AnexosPromocion> AnexosP)
        {
            using(SqlConnection connection = new ConexionBD().Connection)
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.Transaction = transaction;

                        command.CommandText = "AC_InsertarPromocion";
                        command.CommandType = CommandType.StoredProcedure;

                        //modificacion de variables (BACKEND) app
                        command.Parameters.AddWithValue("@IdAsunto", Session["IdAsuntoPromocion"]);
                        command.Parameters.AddWithValue("@Promovente", InfoPromocion.Select(b => b.Promovente).FirstOrDefault());
                        command.Parameters.AddWithValue("@Digitalizado", InfoPromocion.Select(b => b.Digitalizado).FirstOrDefault());
                        command.Parameters.AddWithValue("@IdUsuario", Session["IdUsuario"]);

                        command.Parameters.AddWithValue("@TipoPromocion", InfoPromocion.Select(b => b.TipoPromocion).FirstOrDefault());
                        command.Parameters.AddWithValue("@FechaRecepcion", InfoPromocion.Select(b => b.FechaRecepcion).FirstOrDefault());
                        command.Parameters.AddWithValue("@Tipo",InfoPromocion.Select(b => b.Tipo).FirstOrDefault());
                        command.Parameters.AddWithValue("@IdActividad", InfoPromocion.Select(b => b.IdActividad).FirstOrDefault());
                        command.Parameters.AddWithValue("@IdPerfil", Session["IdPerfil"]);
                        command.Parameters.AddWithValue("@FeAsunto", InfoPromocion.Select(b => b.FeAsunto).FirstOrDefault());                        
                        command.Parameters.AddWithValue("@Estado", InfoPromocion.Select(b => b.EstadoPromocion).FirstOrDefault());

    
	
                        SqlParameter idPosteriorParam = new SqlParameter("@IDPosterior", SqlDbType.Int);
                        idPosteriorParam.Direction = ParameterDirection.Output;
                        command.Parameters.Add(idPosteriorParam);
                        command.ExecuteNonQuery();
                        int idPosterior = Convert.ToInt32(idPosteriorParam.Value);

                        Debug.WriteLine("Tarea 1 completada ✔️");
                        command.CommandText = "AC_InsertarAnexo";
                        foreach(var anexo in AnexosP)
                        {
                           command.Parameters.Clear();
                           command.Parameters.AddWithValue("@IdAsunto", Session["IdAsuntoPromocion"]);
                           SqlParameter paramsAnexoPromocion = command.Parameters.AddWithValue("@IdPosterior", idPosterior);
                           command.Parameters.AddWithValue("@Descripcion", anexo.DescripcionAnexo);
                           command.Parameters.AddWithValue("@Cantidad", anexo.CantidadAnexo);
                            command.Parameters.AddWithValue("@Digitalizado", anexo.DigitalizadoAnexo);
                           command.ExecuteNonQuery();
                        }
                        Debug.WriteLine("Tarea 2 completada ✔️");



                    }
                    transaction.Commit();
                    Debug.WriteLine("Registro de promocion fue correcta!");
                    return true;

                }catch (Exception ex)
                {
                    Debug.WriteLine("Error SQL: " +  ex);
                    
                }

                return true;
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