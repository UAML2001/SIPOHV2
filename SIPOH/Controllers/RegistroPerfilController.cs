using CrystalDecisions.ReportAppServer.DataDefModel;
using DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using static RegistroPerfilController;


public class RegistroPerfilController 
{
    public class RespuestaRegistroPerfil
    {
        public bool hayError { get; set; }
        public string mensaje { get; set; }

    }
    public class DataPerfil
    {
        public string Perfil { get; set; }
        public string TipoCircuito { get; set; }
    }
    
    public class ResultadoInsertarPermisos
    {
        public bool hayError { get; set;  }
        public string mensaje { get; set; }
    }
    public class DataPermiso
    {
        public string nombre { get; set; }
        public string icono { get; set; } 
        public string enlace { get; set; }
    }
    public class ResultadoPermisosAsociados
    {
        public bool hayError { get; set; }
        public string mensaje { get; set; }
    }
    public class DataPermisoAsociado
    {
        public int idPerfil { get; set; }
        public int IdPermiso { get; set; }
        public int idSubPermiso { get; set; }

    }
    public class DataPermisosAsociados
    {
        public int IdPerfil { get; set; }
        public string Perfil { get; set; }
    }
    public class TipoCircuito
    {
        public string Circuito { get; set; }
    }
    public class ResultadoInsertPerfil
    {
        bool hayError { get; set; }
        string mensaje { get; set; }
    }
        // GET: RegistroPerfil
    public static ResultadoPermisosAsociados AsignarPermisos(List<DataPermisoAsociado> DataPermisoAsociado)
    {
        ResultadoPermisosAsociados resultados = new ResultadoPermisosAsociados();
        using (SqlConnection connection = new ConexionBD().Connection){
            connection.Open();
            try
            {
                using(SqlCommand command = new SqlCommand("INSERT INTO P_PermisosAsociados(IdPerfil,IdPermiso, IdSubpermiso, FeCaptura)VALUES( @IdPerfil, @IdPermiso, @IdSubpermiso, GETDATE());", connection))
                {
                    foreach (var data in DataPermisoAsociado)
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@IdPerfil",data.idPerfil);
                        command.Parameters.AddWithValue("@IdPermiso", data.IdPermiso);
                        command.Parameters.AddWithValue("@IdSubpermiso", data.idSubPermiso);
                        command.ExecuteNonQuery();
                    }
                }
            }catch(Exception ex)
            {
                //return resultados.hayError = true;
                //return resultados.mensaje = "ocurrio un error con tu asignacion de permisos";
                return resultados;
                throw new Exception("Ocurrio un error en la asignacion de permisos"+  ex);
            }
        }
            return resultados;  
    }
    public static List<DataPermisosAsociados> ObtenerPermisosAsociadosToPerfil(string nombrePerfil)
    {
        List<DataPermisosAsociados> resultados = new List<DataPermisosAsociados>();
        using(SqlConnection connection = new ConexionBD().Connection)
        {
            connection.Open();
            try
            {
                using(SqlCommand command = new SqlCommand("SELECT TOP (100) PERCENT IdPerfil, Perfil, dbo.FConcatenarLink(IdPerfil) AS LinkEnlace FROM dbo.P_CatPerfiles WHERE Perfil LIKE '%' +  @NombrePerfil; + '%'; ", connection))
                {
                    command.Parameters.AddWithValue("@NombrePerfil", nombrePerfil);
                    command.ExecuteNonQuery();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataPermisosAsociados data = new DataPermisosAsociados();
                            data.IdPerfil = int.Parse(reader["IdPerfil"].ToString());
                            data.Perfil = reader["Perfil"].ToString();
                            resultados.Add(data);

                        }
                    }
                }
            }catch(Exception ex)
            {
                throw new Exception("Ocurrio un problema con traer los permisos");
            }
        }
        return resultados;
    }
    public static List<DataPermisosAsociados> ObtenerPermisosAsociados()
    {
        List<DataPermisosAsociados> resultados = new List<DataPermisosAsociados>();
        using (SqlConnection connection = new ConexionBD().Connection)
        {
            try
            {
                using(SqlCommand command = new SqlCommand("SELECT TOP (100) PERCENT IdPerfil, Perfil, dbo.FConcatenarLink(IdPerfil) AS LinkEnlace FROM dbo.P_CatPerfiles", connection))
                {
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataPermisosAsociados dato = new DataPermisosAsociados();
                            dato.IdPerfil = int.Parse(reader["IdPerfil"].ToString());
                            dato.Perfil = reader["Perfil"].ToString();
                            resultados.Add(dato);
                        }
                    }
                }
            }catch(Exception ex)
            {               
                throw new Exception("Ocurrio un error al traer los permisos: " + ex);
            }
        }
        return resultados;

    }
    public static DataTable ObtenerCatPermisos(string Tipo)
    {
        DataTable dt = new DataTable();
        using (SqlConnection connection = new ConexionBD().Connection)
        {
            try
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT Nombre, linkEnlace,Nombreicono, IdPermiso FROM dbo.P_CatPermisos WHERE (Tipo = @Tipo)", connection))
                {
                    command.Parameters.AddWithValue("@Tipo", Tipo);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {                       
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Surgió un problema al obtener los permisos: " + ex.Message);
            }
        }
    }
    public static List<TipoCircuito> ObtenerTipoCircuito()
    {
        List<TipoCircuito> resultados = new List<TipoCircuito>();
        using (SqlConnection connection = new ConexionBD().Connection)
        {
            try
            {
                connection.Open();
                using(SqlCommand command = new SqlCommand("SELECT TipoCircuito FROM P_CatPerfiles" , connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TipoCircuito circuito = new TipoCircuito();
                            circuito.Circuito = reader["TipoCircuito"].ToString();
                            resultados.Add(circuito);
                        }
                    }

                }

            }catch(Exception ex)
            {
                throw new Exception("Error: " + ex);
            }
            return resultados;
        }
    }
    public static ResultadoInsertPerfil InsertRelacionPerfil(List<DataPerfil> DataPerfil , List<DataPermiso> Permisos)
    {

        ResultadoInsertPerfil resultados = new ResultadoInsertPerfil();
        try
        {
            InsertarPerfil(DataPerfil);
            
             RegistroPermisos(Permisos);

        }catch(Exception ex)
        {   
             //resultados.hayError = true;
             //resultados.mensaje = $"ocurrio un error con tu asignacion de permisos";

            return resultados;
            throw new Exception("Error: " + ex);
        }
        
        return resultados;
    }
    public static RespuestaRegistroPerfil InsertarPerfil(List<DataPerfil> DataPerfil)
    {
        RespuestaRegistroPerfil resultados = new RespuestaRegistroPerfil();
        using(SqlConnection connection = new ConexionBD().Connection)
        {
            try { 
            connection.Open();
                using (SqlCommand command = new SqlCommand("INSERT INTO P_CatPerfiles(Perfil, TipoCircuito)VALUES(@Perfil, @TipoCircuito); ", connection))
                {
                    foreach(var data in DataPerfil)
                    {
                        command.Parameters.AddWithValue("@Perfil",data.Perfil.ToUpper());
                        command.Parameters.AddWithValue("@TipoCircuito", data.TipoCircuito);
                        resultados.hayError = false;
                        resultados.mensaje = "Se guardaron los datos correctamente.";
                        command.ExecuteNonQuery();

                    }
                }            
            }
            catch(Exception ex)
            {
                resultados.hayError = true;
                resultados.mensaje = $"El registro de tu perfil no se hizo correctamnete";
                return resultados;
                throw new Exception("Error al ingresar tu perfil: " + ex);
            }
        }
        return resultados;
     }
    public static ResultadoInsertarPermisos RegistroPermisos(List<DataPermiso> DataPermisos)
    {
        ResultadoInsertarPermisos resultados = new ResultadoInsertarPermisos();
        using (SqlConnection connection = new ConexionBD().Connection)
        {
            try
            {
                connection.Open();
                using(SqlCommand command = new SqlCommand("INSERT INTO P_CatPermisos (Nombre, Nombreicono, linkEnlace)VALUES (@Nombre, @NombreIcono, @LinkEnlace);", connection))
                {
                    foreach (var data in DataPermisos)
                    {
                        command.Parameters.AddWithValue("@Nombre", data.nombre);
                        command.Parameters.AddWithValue("@NombreIcono", data.icono);
                        command.Parameters.AddWithValue("@LinkEnlace", data.enlace);
                        resultados.hayError = false;
                        resultados.mensaje = "Registro exitoso";
                        command.ExecuteNonQuery();

                    }
                }

            }catch(Exception ex)
            {
                resultados.hayError = true;
                resultados.mensaje = "Surgio un error con el registro de tu permiso";
                return resultados;
                throw new Exception("Surgio un error con tu registro de permisos" + ex);
                
            }
        }
            return resultados;
    }
    
   

}