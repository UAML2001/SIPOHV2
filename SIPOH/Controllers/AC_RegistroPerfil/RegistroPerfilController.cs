using CrystalDecisions.ReportAppServer.DataDefModel;
using DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using static PersonalUsuariosController;
using static RegistroPerfilController;
//Dataasing
using System.Threading.Tasks;


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
        public bool hayError { get; set; }
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
        public int IdPermiso { get; set; }
        public int idSubPermiso { get; set; }

    }
    public class DataPermisosAsociados
    {
        public int IdPerfil { get; set; }
        public string Perfil { get; set; }
        public string Enlaces { get; set; }
    }
    public class TipoCircuito
    {
        public string Circuito { get; set; }
    }
    public class ResultadoInsertCreacionPerfil
    {
        public bool hayError { get; set; }
        public string mensaje { get; set; }
    }
    public class ResultadosInsertDataPermisosAsociados
    {

    }
    public static DataTable ObtenerPermisosAsociados(int perfil, string tipoPermiso)
    {
        DataTable dt = new DataTable();
        using (SqlConnection connection = new ConexionBD().Connection)
        {
            try {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT dbo.P_PermisosAsociados.IdSubpermiso, dbo.P_CatPermisos.linkEnlace, dbo.P_SubPermisosAsociados.Editar, dbo.P_SubPermisosAsociados.Ver, dbo.P_SubPermisosAsociados.Eliminar, dbo.P_SubPermisosAsociados.SuperUser, dbo.P_SubPermisosAsociados.Administrador, dbo.P_SubPermisosAsociados.Usuario, dbo.P_CatPermisos.Tipo FROM dbo.P_CatPerfiles INNER JOIN dbo.P_PermisosAsociados ON dbo.P_CatPerfiles.IdPerfil = dbo.P_PermisosAsociados.IdPerfil INNER JOIN dbo.P_CatPermisos ON dbo.P_PermisosAsociados.IdPermiso = dbo.P_CatPermisos.IdPermiso INNER JOIN dbo.P_SubPermisosAsociados ON dbo.P_PermisosAsociados.IdSubpermiso = dbo.P_SubPermisosAsociados.IdSubpermiso WHERE (dbo.P_CatPerfiles.IdPerfil = @IdPerfil) AND (dbo.P_CatPermisos.Tipo = @TipoEnlace)", connection))
                {
                    command.Parameters.Add("@IdPerfil", SqlDbType.Int);
                    command.Parameters["@IdPerfil"].Value = perfil;
                    command.Parameters.AddWithValue("@TipoEnlace", tipoPermiso);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }catch(Exception ex)
            {
                throw new Exception("Error en traer permisos Asociados: " + ex.Message);
            }
        }
    }
    // GET: RegistroPerfil
    public static List<DataPermisosAsociados> ObtenerPermisosAsociadosToPerfil(string nombrePerfil)
    {
        List<DataPermisosAsociados> resultados = new List<DataPermisosAsociados>();
        using (SqlConnection connection = new ConexionBD().Connection)
        {
            connection.Open();
            try
            {
                using (SqlCommand command = new SqlCommand("SELECT TOP (100) PERCENT IdPerfil, Perfil, dbo.FConcatenarLink(IdPerfil) AS LinkEnlace FROM dbo.P_CatPerfiles WHERE Perfil LIKE '%' +  @NombrePerfil + '%'; ", connection))
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
                            data.Enlaces = reader["LinkEnlace"].ToString();
                            resultados.Add(data);

                        }
                    }
                }
            } catch (Exception ex)
            {
                throw new Exception("Ocurrio un problema con traer los permisos" +  ex.Message);
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
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT TOP (100) PERCENT IdPerfil, Perfil, dbo.FConcatenarLink(IdPerfil) AS LinkEnlace FROM dbo.P_CatPerfiles", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DataPermisosAsociados dato = new DataPermisosAsociados();
                            dato.IdPerfil = int.Parse(reader["IdPerfil"].ToString());
                            dato.Perfil = reader["Perfil"].ToString();
                            dato.Enlaces = reader["LinkEnlace"].ToString();
                            resultados.Add(dato);
                        }
                    }
                }
            } catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al traer los permisos: " + ex);
            }
        }
        return resultados;

    }





    //Obtener enlaces por tipo de circuito
    public static DataTable ObtenerCatPermisos(string Tipo)
    {
        DataTable dt = new DataTable();
        using (SqlConnection connection = new ConexionBD().Connection)
        {
            try
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT  Nombre, linkEnlace,Nombreicono, IdPermiso FROM dbo.P_CatPermisos WHERE (Tipo = @Tipo)", connection))
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

    //Obtener tipo circuitos
    public static List<TipoCircuito> ObtenerTipoCircuito()
    {
        List<TipoCircuito> resultados = new List<TipoCircuito>();
        using (SqlConnection connection = new ConexionBD().Connection)
        {
            try
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT DISTINCT TipoCircuito FROM P_CatPerfiles", connection))
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

            } catch (Exception ex)
            {
                throw new Exception("Error: " + ex);
            }
            return resultados;
        }
    }


    //Creacion de un perfil como a si mismo la relacion de enlaces en(P_PermisosAsociados) y relacion con los enlaces en P_CatPermisos
    public static ResultadoInsertCreacionPerfil InsertCreacionPerfil(List<DataPerfil> perfil, List<DataPermisoAsociado> permisoAsociado)
    {
        ResultadoInsertCreacionPerfil resultados = new ResultadoInsertCreacionPerfil();

        using (SqlConnection connection = new ConexionBD().Connection)
        {
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                int perfilId = 0;

                using (SqlCommand commandPerfil = new SqlCommand("INSERT INTO P_CatPerfiles(Perfil, TipoCircuito) VALUES (@Perfil, @TipoCircuito); SELECT SCOPE_IDENTITY();", connection, transaction))
                {
                    foreach (var datos in perfil)
                    {
                        commandPerfil.Parameters.Clear(); // Limpiar parámetros antes de cada iteración
                        commandPerfil.Parameters.AddWithValue("@Perfil", datos.Perfil);
                        commandPerfil.Parameters.AddWithValue("@TipoCircuito", datos.TipoCircuito);
                        
                        perfilId = Convert.ToInt32(commandPerfil.ExecuteScalar());
                    }
                }

                using (SqlCommand commandPermisos = new SqlCommand("INSERT INTO P_PermisosAsociados(IdPerfil, IdPermiso, IdSubpermiso) VALUES (@IdPerfil, @IdPermiso, 1);", connection, transaction))
                {                        
                    foreach (var permiso in permisoAsociado)
                    {
                        commandPermisos.Parameters.Clear(); // Limpiar parámetros antes de cada iteración
                        commandPermisos.Parameters.AddWithValue("@IdPerfil", perfilId);
                        commandPermisos.Parameters.AddWithValue("@IdPermiso", permiso.IdPermiso);
                        commandPermisos.ExecuteNonQuery();
                    }
                }

                transaction.Commit(); // Confirmar la transacción si todo va bien
                resultados.hayError = false;
            }
            catch (Exception ex)
            {
                  
                resultados.hayError = true;
                resultados.mensaje = "Surgió un error al realizar la transacción: " + ex.Message;
                transaction.Rollback(); // Revertir la transacción si hay algún error
                throw new Exception("Error: " + ex.Message);
            }
        }

        return resultados;
    }


    //Registro subpermisos para ver, editar, eliminar, usuario
    public static RespuestaRegistroPerfil RegistroSubPermisosAsociados(int IdPerfil,  List<ResultadosInsertDataPermisosAsociados> permisos)
    {
        RespuestaRegistroPerfil resultados = new RespuestaRegistroPerfil();
        using (SqlConnection connection  = new ConexionBD().Connection)
        {
            connection.Open();
            try
            {
                using(SqlCommand command = new SqlCommand("INSERT INTO ", connection))
                {
                    
                }
            }
            catch (Exception ex)
            {
                resultados.hayError = true;
                resultados.mensaje  = "Error en creacion de perfil.";
                return resultados;
                throw new Exception("Error en registro de  sub permisos asociados:  " + ex);
            }
        }
        return resultados;
    }
    
    
    
    //funcion para el RegistroEnlaces en P_CatPermiso
    public static ResultadoInsertarPermisos RegistroEnlaces(List<DataPermiso> DataPermisos)
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