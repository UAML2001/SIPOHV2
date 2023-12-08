using DatabaseConnection;
using SIPOH;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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
                        HttpContext.Current.Session["UserName"] = reader["Usuario"].ToString();
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

                                            foreach (string enlace in enlacesRecuperados)
                                            {
                                                Debug.WriteLine(enlace + " enlaces");
                                            }

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
        }

        // Devuelve false para indicar un inicio de sesión fallido
        return false;
    }



    public static string getJuzgado()
    {
        // Obtén el ID del juzgado de la sesión
        string idJuzgado = HttpContext.Current.Session["IDJuzgado"] as string;

        if (string.IsNullOrEmpty(idJuzgado))
        {
            return "No se encontró el juzgado"; // Opcional: manejar el caso en el que no se encuentra el juzgado
        }

        using (SqlConnection connection = new ConexionBD().Connection)
        {
            connection.Open();
            string query = "SELECT Nombre FROM P_CatJuzgados WHERE IdJuzgado = @IDJuzgado;";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("IDJuzgado", idJuzgado);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader["Nombre"].ToString(); // Devuelve el nombre del juzgado
                    }
                    else
                    {
                        return "No se encontró el juzgado"; // Opcional: manejar el caso en el que no se encuentra el juzgado
                    }
                }
            }
        }
    }
}

