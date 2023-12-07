using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System;

namespace DatabaseConnection
{
    public class ConexionBD
    {
        
            private readonly string connectionString;
            public SqlConnection Connection { get; private set; }
            public ConexionBD()
            {
                string connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
                Connection = new SqlConnection(connectionString);
            }
            public bool Conectar()
            {
                try
                {
                    Connection.Open();
                    Debug.WriteLine("Conexion Exitosa!");
                    return true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Hay problemas con la conexion" + ex.Message);
                    
                return false;
                }
            }
            public void Desconectar()
            {
                if (Connection != null && Connection.State != ConnectionState.Closed)
                {
                    Connection.Close();
                }
            }        
    }
}