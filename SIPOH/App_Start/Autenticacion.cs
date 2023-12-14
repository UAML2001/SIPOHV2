using DatabaseConnection;
using SIPOH;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.Web;

public class Autenticacion
{

    public static bool AutenticarUsuario(string usuario, string contrasena)
    {
        using (SqlConnection connection = new ConexionBD().Connection)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand("AutenticarUsuario", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UsuarioIngresado", usuario);
                command.Parameters.AddWithValue("@ContrasenaIngresada", contrasena);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Asignación de valores a las variables de sesión 
                        HttpContext.Current.Session["IdUsuario"] = reader["IdUsuario"].ToString();
                        //Usuario && Contraseña
                        //HttpContext.Current.Session["Contraseña"] = reader["Contraseña"].ToString();                        
                        HttpContext.Current.Session["UserName"] = reader["Usuario"].ToString();
                        //  //
                        HttpContext.Current.Session["IdPerfil"] = reader["IdPerfil"].ToString();
                        HttpContext.Current.Session["Name"] = reader["Nombre"].ToString();
                        HttpContext.Current.Session["FirstName"] = reader["APaterno"].ToString();
                        HttpContext.Current.Session["LastName"] = reader["AMaterno"].ToString();
                        HttpContext.Current.Session["IDJuzgado"] = reader["IdJuzgado"].ToString();
                        HttpContext.Current.Session["PhoneNum"] = reader["telefono"].ToString();
                        HttpContext.Current.Session["Address"] = reader["Domicilio"].ToString();
                        HttpContext.Current.Session["Email"] = reader["Email"].ToString();



                        reader.Close();


                        using (SqlCommand command2 = new SqlCommand("getPerfil", connection))
                        {
                            command2.CommandType = CommandType.StoredProcedure;
                            command2.Parameters.AddWithValue("@UserName", HttpContext.Current.Session["UserName"]);
                            using (SqlDataReader reader2 = command2.ExecuteReader())
                            {
                                if (reader2.Read())
                                {
                                    HttpContext.Current.Session["Perfil"] = reader2["Perfil"].ToString();
                                    HttpContext.Current.Session["TCircuito"] = reader2["TipoCircuito"].ToString();
                                }
                                reader2.Close();
                                using (SqlCommand command3 = new SqlCommand("getEnlaces", connection))
                                {
                                    command3.CommandType = CommandType.StoredProcedure;
                                    command3.Parameters.AddWithValue("@IdPerfil", HttpContext.Current.Session["IdPerfil"]);
                                    using (SqlDataReader reader3 = command3.ExecuteReader())
                                    {
                                        List<string> enlacesRecuperados = HttpContext.Current.Session["enlace"] as List<string>;
                                        if (enlacesRecuperados == null)
                                        {
                                            enlacesRecuperados = new List<string>();
                                        }

                                        while (reader3.Read())
                                        {
                                            HttpContext.Current.Session["enlace"] = reader3["linkEnlace"].ToString();
                                            enlacesRecuperados.Add(HttpContext.Current.Session["enlace"] as string);
                                        }

                                        // Almacena la lista en la variable de sesión
                                        HttpContext.Current.Session["enlace"] = enlacesRecuperados;

                                        reader3.Close();
                                        using (SqlCommand command4 = new SqlCommand("ObtenerJuzgado", connection))
                                        {
                                            command4.CommandType = CommandType.StoredProcedure;
                                            command4.Parameters.AddWithValue("@IDJuzgado", HttpContext.Current.Session["IDJuzgado"]);

                                            // parámetros de salida
                                            SqlParameter paramNombreJuzgado = command4.Parameters.Add("@NombreJuzgado", SqlDbType.NVarChar, 255);
                                            paramNombreJuzgado.Direction = ParameterDirection.Output;

                                            SqlParameter paramIdCircuito = command4.Parameters.Add("@IdCircuito", SqlDbType.NVarChar, 255);
                                            paramIdCircuito.Direction = ParameterDirection.Output;

                                            SqlParameter paramIdDistrito = command4.Parameters.Add("@IdDistrito", SqlDbType.NVarChar, 255);
                                            paramIdDistrito.Direction = ParameterDirection.Output;

                                            command4.ExecuteNonQuery();

                                            // Actualizar la sesión con los valores obtenidos del procedimiento almacenado
                                            HttpContext.Current.Session["NombreJuzgado"] = paramNombreJuzgado.Value?.ToString();
                                            HttpContext.Current.Session["IdCircuito"] = paramIdCircuito.Value?.ToString();
                                            HttpContext.Current.Session["IdDistrito"] = paramIdDistrito.Value?.ToString();
                                        }

                                    }

                                }
                            }
                        }
                        // Devuelve true para indicar un inicio de sesión exitoso
                        return true;
                    }
                }
            }
            return false;
        }
    }

}  



