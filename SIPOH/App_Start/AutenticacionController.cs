﻿using DatabaseConnection;
using SIPOH;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;

public class AutenticacionController
{
    public static bool AutenticarUsuarioSeguridad(string usuario, string contrasena)
    {
        using (SqlConnection connection = new ConexionBD().Connection)
        {
            connection.Open();
            //Verifica si la ip actual no esta bloqueada
            string ipUsuario = HttpContext.Current.Request.UserHostAddress;
            if (IPBloqueadaUser(ipUsuario))
            {
                return false; // Retorna false indicando que el inicio de sesión falló debido al bloqueo de IP
            }
            // Lógica para verificar el número de intentos fallidos de inicio de sesión para la IP actual Si excede un cierto umbral, bloquea la IP
            if (UsuarioExcedeIntentosFallidos(usuario))
            {
                // lógica para bloquear la IP
                BloquearIP(HttpContext.Current.Request.UserHostAddress, usuario);
                return false; // Retorna false indicando que el inicio de sesión falló debido al bloqueo de IP
            }
                RegistroAcceso(usuario, HttpContext.Current.Request.UserHostAddress);
            using (SqlCommand command = new SqlCommand("AutenticarUsuario", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UsuarioIngresado", usuario);
                command.Parameters.AddWithValue("@ContrasenaIngresada", contrasena);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ResetearIntentosFallidos(usuario);
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
                        HttpContext.Current.Session["PerfilNombre"] = reader["Perfil"].ToString();
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
                    else
                    {
                        // Incrementar el contador de intentos fallidos
                        IncrementarIntentosFallidos(usuario);
                    }
                }
            }
        }
        return true;
    }
    // Función para verificar si un usuario ha excedido el número máximo de intentos fallidos de inicio de sesión
    private static bool UsuarioExcedeIntentosFallidos(string usuario)
    {        
        int intentosFallidos = ObtenerIntentosFallidos(usuario); // Función ficticia para obtener el número de intentos fallidos
        return intentosFallidos >= 5; // Por ejemplo, si el usuario excede 5 intentos fallidos, retorna true
    }
    // Función para bloquear la IP en la base de datos
    private static void BloquearIP(string ip, string usuario)
    {        
        using (SqlConnection connection = new ConexionBD().Connection)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand("INSERT INTO P_IPBloqueadas (IP, FechaBloqueo, Estado, RegistroAcceso) VALUES (@IP, GETDATE(), 'BLOQUEADO' , @Usuario)", connection))
            {
                command.Parameters.AddWithValue("@IP", ip);
                command.Parameters.AddWithValue("@Usuario", usuario);
                command.ExecuteNonQuery();
            }
        }
    }
    private static void RegistroAcceso(string usuario, string ip)
    {
        using (SqlConnection connection = new ConexionBD().Connection)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand("INSERT INTO P_RegistroAccesos (UsuarioAcceso, IP, Estado, FechaAcceso) VALUES (@Usuario, @IP ,1, GETDATE() )", connection))
            {
                command.Parameters.AddWithValue("@Usuario", usuario);
                command.Parameters.AddWithValue("@IP", ip);
                command.ExecuteNonQuery();
            }
        }
    }
    // Función para obtener el número de intentos fallidos de inicio de sesión para un usuario dado
    public static int ObtenerIntentosFallidos(string usuario)
    {
        int intentosFallidos = 0;
        try
        {
            using (SqlConnection connection = new ConexionBD().Connection)
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("AC_GetIntentosFallidosPorUsuario", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Usuario", usuario);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            intentosFallidos = Convert.ToInt32(reader["Accesos"]);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Manejar la excepción según sea necesario (p. ej., registro, notificación, etc.)
            Console.WriteLine("Error al obtener los intentos fallidos: " + ex.Message);
        }
        return intentosFallidos;
    }
    private static bool IPBloqueadaUser(string ip)
    {
        using (SqlConnection connection = new ConexionBD().Connection)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand("AC_GetIPBloqueada", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IP", ip);
                int count = (int)command.ExecuteScalar();   
                return count > 0; // Retorna true si la IP está en la lista de IPs bloqueadas
            }
        }
    }    
    // Función para incrementar el contador de intentos fallidos de inicio de sesión para un usuario dado
    private static void IncrementarIntentosFallidos(string usuario)
    {
        using (SqlConnection connection = new ConexionBD().Connection)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand("UPDATE P_Usuarios SET Accesos = Accesos + 1 WHERE Usuario = @Usuario", connection))
            {

                command.Parameters.AddWithValue("@Usuario", usuario);
                command.ExecuteNonQuery();
            }
        }
    }
    // Función para restablecer el contador de intentos fallidos de inicio de sesión para un usuario dado
    private static void ResetearIntentosFallidos(string usuario)
    {
        using (SqlConnection connection = new ConexionBD().Connection)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand("UPDATE P_Usuarios SET Accesos = 0 WHERE Usuario = @Usuario", connection))
            {
                command.Parameters.AddWithValue("@Usuario", usuario);
                command.ExecuteNonQuery();
            }
        }
    }
}