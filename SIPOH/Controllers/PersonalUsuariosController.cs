using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using CrystalDecisions.ReportAppServer.DataDefModel;
using DatabaseConnection;
using Microsoft.Ajax.Utilities;
using SIPOH;
using SIPOH.Controllers.AC_Digitalizacion;

public class PersonalUsuariosController
{
    public class DataCatPerfil
    {
        public int IdPerfil { get; set; }
        public string Perfil { get; set; }
        public string tipoCircuito { get; set; }
    }
    public class DataCatJuzgado
    {
        public int IdJuzgado { get; set; }
        public string Juzgado { get; set; }
    }
    public class RespuestaInsertUsuario
    {
        public bool hayError { get; set; }
        public string mensaje { get; set; }

    }
    public class DataInsertUsuario
    {
        public string Nombre { get; set; }
        public string APaterno { get; set; }
        public string AMaterno { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public int IdJuzgado { get; set; }
        public int IdPerfil { get; set; }
        public string Status { get; set; }
        public string Domicilio { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Pass { get; set; }
    }
    public class DataBuscarUsuario
    {
        public string Nombre { get; set; }
        public string APaterno { get; set; }
        public string AMaterno { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public string ContraseñaDesencriptada { get; set; }
        public string IdJuzgado { get; set; }

        public string Perfil { get; set; }
        public string Status { get; set; }
        public string Domicilio { get; set; }
        public string telefono { get; set; }
        public string Email { get; set; }


        public string TipoCircuito { get; set; }
        public int IdUsuario { get; set; }
    }
    public class DataUpdateUsuario
    {
        
        public string usuario { get; set; }
        public string pass { get; set; }
        
        public int telefono { get; set; }
       
        
    }
    public class PermisosAsociados
    {
       public bool ver { get; set; }
       public bool editar { get; set; }
       public bool eliminar { get; set; }
       public bool superUsuario { get; set; }
       public bool administrador { get; set; }
       public bool usuario { get; set; }
    }
    // GET: PersonalUsuarios
    //ObtenerCatalogo de juzgados
    public static List<DataCatJuzgado> GetCatJuzgado()
    {
        List<DataCatJuzgado> resultados = new List<DataCatJuzgado>();
        using(SqlConnection connection = new ConexionBD().Connection)
        {
            connection.Open();
            using(SqlCommand command = new SqlCommand("SELECT IdJuzgado, Nombre FROM P_CatJuzgados WHERE( SubTipo = 'A')", connection))
            {                
                using(SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DataCatJuzgado juzgado = new DataCatJuzgado();
                        juzgado.IdJuzgado = Convert.ToInt32(reader["IdJuzgado"]);
                        juzgado.Juzgado = reader["Nombre"].ToString();
                        resultados.Add(juzgado);
                    }
                }
            }
        }
        return resultados;
    }
   
    //Obtener perfil de logeos
    public static List<DataCatPerfil> GetCatPerfil()
    {
        List<DataCatPerfil> resultados = new List<DataCatPerfil>();
        using(SqlConnection connection = new ConexionBD().Connection)
        {
            connection.Open();
            using(SqlCommand command = new SqlCommand("SELECT IdPerfil, Perfil, TipoCircuito FROM P_CatPerfiles", connection))
            {
                
                using(SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DataCatPerfil perfil = new DataCatPerfil();
                        perfil.IdPerfil = Convert.ToInt32(reader["IdPerfil"]);
                        perfil.Perfil = reader["Perfil"].ToString();
                        perfil.tipoCircuito = reader["TipoCircuito"].ToString();
                        resultados.Add(perfil);
                    }
                }
            }
        }
        return resultados;
    }
    public static List<DataCatPerfil> GetCatPerfilAtencionCiudadana()
    {
        List<DataCatPerfil> resultados = new List<DataCatPerfil>();
        using (SqlConnection connection = new ConexionBD().Connection)
        {
            connection.Open();
            using (SqlCommand command = new SqlCommand("SELECT IdPerfil, Perfil, TipoCircuito FROM P_CatPerfiles WHERE TipoCircuito != 'a' AND TipoCircuito != 'e'", connection))
            {

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DataCatPerfil perfil = new DataCatPerfil();
                        perfil.IdPerfil = Convert.ToInt32(reader["IdPerfil"]);
                        perfil.Perfil = reader["Perfil"].ToString();
                        perfil.tipoCircuito = reader["TipoCircuito"].ToString();
                        resultados.Add(perfil);
                    }
                }
            }
        }
        return resultados;
    }

    //obtener circuito login
    public static void GetCircuito(int Juzgado)
    {
        using (SqlConnection connection = new ConexionBD().Connection)
        {            
            connection.Open();
            using (SqlCommand command = new SqlCommand("SELECT IdCircuito FROM dbo.P_CatJuzgados WHERE (IdJuzgado = @IdJuzgado)", connection))
            {
                command.Parameters.AddWithValue("@IdJuzgado", Juzgado);
                using(SqlDataReader reader = command.ExecuteReader()) {
                    if (reader.Read())
                    {
                        HttpContext.Current.Session["IdCircuito"] = reader["IdCircuito"].ToString();
                    }
                }                                       
            }                        
        }
        
        
    }
    
    
    //Busqueda de usuarios proceso almacenado
    public static List<DataBuscarUsuario> GetUsuario(string nombre, int Juzgado,  int Perfil)
    {
        List<DataBuscarUsuario> resultados = new List<DataBuscarUsuario>();
        using(SqlConnection connection = new ConexionBD().Connection)
        {
            try {
                connection.Open();
                using(SqlCommand command = new SqlCommand("AC_ObtenerUsuario", connection))
                {
                    command.Parameters.AddWithValue("@Usuario", nombre);
                    command.Parameters.AddWithValue("@Juzgado", Juzgado);
                    command.Parameters.AddWithValue("@Perfil", Perfil);
                    
                    command.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataBuscarUsuario infoUser = new DataBuscarUsuario();
                            infoUser.IdUsuario = int.Parse(reader["IdUsuario"].ToString());
                            infoUser.Nombre = reader["Nombre"].ToString();
                            infoUser.APaterno = reader["APaterno"].ToString();
                            infoUser.AMaterno = reader["AMaterno"].ToString();
                            infoUser.Contraseña = reader["Contraseña"].ToString();
                            infoUser.Status = reader["Status"].ToString() == "I" ? "INACTIVO" : "ACTIVO";
                            var Pass = reader["Contraseña"].ToString();
                            string contraseñaEncriptada = CryptographyController.EncryptString(Pass);                           
                            string contraseñaCifrada = CryptographyController.DecryptString(Pass);
                            infoUser.ContraseñaDesencriptada = contraseñaCifrada;
                            infoUser.Usuario = reader["Usuario"].ToString();
                            infoUser.Perfil = reader["Perfil"].ToString();
                            infoUser.Domicilio = reader["Domicilio"].ToString();
                            infoUser.telefono = reader["telefono"].ToString();
                            infoUser.Email = reader["Email"].ToString();
                            infoUser.IdJuzgado = reader["IdJuzgado"].ToString(); 
                            infoUser.TipoCircuito = reader["TipoCircuito"].ToString();
                            resultados.Add(infoUser);
                        }

                    }
                }
            }
            catch(Exception ex) {
                throw new Exception("Ocurrio un error" + ex.Message);
            
            }
        }
        return resultados;
    }
    //RespuestaInsertUsuario USIARIOS
    public static RespuestaInsertUsuario InsertUsuario(List<DataInsertUsuario> DataUsuario)
    {
        RespuestaInsertUsuario resultados = new RespuestaInsertUsuario();
        using(SqlConnection connection = new ConexionBD().Connection)
        {
            try
            {
                connection.Open();
                using (SqlCommand command =  new SqlCommand("INSERT INTO P_Usuarios (Nombre, APaterno, AMaterno, Usuario, Contraseña, IdJuzgado, IdPerfil, Status , FechaAlta , Domicilio, Email, telefono,Pass) VALUES(@Nombre,@APaterno, @AMaterno, @Usuario, @Contraseña, @IdJuzgado, @IdPerfil, @Status, GETDATE(), @Domicilio, @Email, @Telefono, @Pass );", connection) )
                {
                    foreach (var infoUsuario in DataUsuario)
                    {
                        command.Parameters.AddWithValue("@Nombre", infoUsuario.Nombre.ToUpper());
                        command.Parameters.AddWithValue("@APaterno", infoUsuario.APaterno.ToUpper());
                        command.Parameters.AddWithValue("@AMaterno", infoUsuario.AMaterno.ToUpper());
                        command.Parameters.AddWithValue("@Usuario", infoUsuario.Usuario);
                        command.Parameters.AddWithValue("@Contraseña", infoUsuario.Contraseña);
                        command.Parameters.AddWithValue("@IdJuzgado", infoUsuario.IdJuzgado);
                        command.Parameters.AddWithValue("@IdPerfil", infoUsuario.IdPerfil);
                        command.Parameters.AddWithValue("@Status", infoUsuario.Status);
                        command.Parameters.AddWithValue("@Domicilio", infoUsuario.Domicilio.ToUpper());
                        command.Parameters.AddWithValue("@Email", infoUsuario.Email.ToUpper());
                        command.Parameters.AddWithValue("@Telefono", infoUsuario.Telefono);
                        command.Parameters.AddWithValue("@Pass", infoUsuario.Pass);
                        resultados.hayError = false;
                        resultados.mensaje = "Se guardaron los datos correctamente.";
                        command.ExecuteNonQuery(); 
                    }
                }

            }catch (Exception ex) {
                resultados.hayError = true;
                resultados.mensaje = $"No se pudo generar el registro de tu usuario.";
                Debug.WriteLine("Error: " + ex.Message);
                return resultados;
            }
        }
        return resultados;
    }
    public static bool ActualizarUsuario(int idUsuario, DataUpdateUsuario info)
    {
        using (SqlConnection connection = new ConexionBD().Connection)
        {
            try
            {
                connection.Open();
                string query = "UPDATE P_Usuarios SET Usuario = @usuario, Contraseña = @contraseña, telefono= @telefono WHERE IdUsuario = @idUsuario";

                SqlCommand command = new SqlCommand(query, connection);
                
                command.Parameters.AddWithValue("@usuario", info.usuario);
                command.Parameters.AddWithValue("@contraseña", info.pass);                
                command.Parameters.AddWithValue("@telefono", info.telefono);
                
                command.Parameters.AddWithValue("@idUsuario", idUsuario);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex);
                return false;
            }
        }
    }


    public static bool EditarÍnformacionUsuario(int IdUsuario, Dictionary<string, string> newData)
    {
        using (SqlConnection connection = new ConexionBD().Connection)
        {
            connection.Open();
            try
            {
                using (SqlCommand command = new SqlCommand("UPDATE ", connection))
                {
                    command.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción
                Console.WriteLine("Error al editar la información del usuario: " + ex.Message);
            }
            return false;
        }
    }

    // Desbloquear usuario de ip sin acceso
    public static bool DesbloquearUsuario( string Usuario)
    {
        using (SqlConnection connection = new ConexionBD().Connection)
        {
            connection.Open();
            try{
                using(SqlCommand command = new SqlCommand("DELETE P_IPBloqueadas WHERE RegistroAcceso = @Usuario;", connection))
                {
                    command.Parameters.AddWithValue("@Usuario", Usuario);
                    command.ExecuteNonQuery();
                    using(SqlCommand commandAcceso = new SqlCommand("UPDATE P_Usuarios SET Accesos = 0 WHERE Usuario = @Usuario;", connection))
                    {
                        commandAcceso.Parameters.AddWithValue("@Usuario", Usuario);
                        commandAcceso.ExecuteNonQuery();
                    }
                }
                return true;
            }catch(Exception ex)
            {
                new Exception("Ocurrio un error: " + ex);
                return false;
            }
        }
    }
    public static List<PermisosAsociados> ObtenerSubPermisos(int IdPermiso, int IdPerfil)
    {
        List<PermisosAsociados> resultados = new List<PermisosAsociados> ();
        using(SqlConnection connection = new ConexionBD().Connection)
        {
            try
            {
                connection.Open();
                using(SqlCommand command = new SqlCommand(" SELECT dbo.P_SubPermisosAsociados.Ver,  dbo.P_SubPermisosAsociados.Editar, dbo.P_SubPermisosAsociados.Eliminar, dbo.P_SubPermisosAsociados.SuperUser, dbo.P_SubPermisosAsociados.Administrador, dbo.P_SubPermisosAsociados.Usuario FROM dbo.P_CatPerfiles INNER JOIN dbo.P_PermisosAsociados ON dbo.P_CatPerfiles.IdPerfil = dbo.P_PermisosAsociados.IdPerfil INNER JOIN dbo.P_SubPermisosAsociados ON dbo.P_PermisosAsociados.IdSubpermiso = dbo.P_SubPermisosAsociados.IdSubpermiso WHERE (dbo.P_PermisosAsociados.IdPermiso = @IdPermiso) AND (dbo.P_CatPerfiles.IdPerfil = @IdPerfil)", connection))
                {
                    command.Parameters.AddWithValue("@IdPermiso", IdPermiso);
                    command.Parameters.AddWithValue("@IdPerfil", IdPerfil);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PermisosAsociados permisos = new PermisosAsociados();
                            permisos.ver = Convert.ToBoolean(reader["Ver"]);
                            permisos.editar = Convert.ToBoolean(reader["Editar"]);
                            permisos.eliminar = Convert.ToBoolean(reader["Eliminar"]);
                            permisos.superUsuario = Convert.ToBoolean(reader["SuperUser"]);
                            permisos.administrador = Convert.ToBoolean(reader["Administrador"]);
                            permisos.usuario = Convert.ToBoolean(reader["Usuario"]);
                            resultados.Add(permisos);
                        }
                    }
                    
                }
            }catch (Exception ex)
            {
                throw new Exception("Ocurrio un error" + ex.Message);
            }

        }
        return resultados;
    }


    //Dar de alta y dar de baja a aun usuario
    public static bool BajaDeUsuario(int IdUsuario)
    {
        using (SqlConnection connection = new ConexionBD().Connection)
        {
            connection.Open();
            try{
                using(SqlCommand command = new SqlCommand("UPDATE P_Usuarios SET Status = 'I', FechaBaja = GETDATE() WHERE IdUsuario = @IdUsuario;", connection))
                {
                    command.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    command.ExecuteNonQuery();
                }
                return true;
            }catch(Exception ex)
            {
                new Exception("Ocurrio un error en dar de baja al usuario");
                return false;
            }
        }
    }
    public static bool AltaDeUsuario(int IdUsuario)
    {
        using(SqlConnection connection = new ConexionBD().Connection)
        {
            connection.Open();
            try
            {
                using(SqlCommand command = new SqlCommand("UPDATE P_Usuarios SET Status = 'A', FechaAlta = GETDATE() WHERE IdUsuario = @IdUsuario;", connection))
                {
                    command.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    command.ExecuteNonQuery();
                }
                return true;
            }catch(Exception ex)
            {
                new Exception("Lo siento ocurrio un error al dar de alta un usuario");
                return false;
            }
        }
        
    }
}
