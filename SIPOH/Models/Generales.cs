using DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace SIPOH.Models
{
    public class Generales
    {
        public static DropDownList GenerarCatalogo(string StoredProcedure)
        {
            //Creando objeto a devolver
            DropDownList catalogo = new DropDownList();

            //Creando objeto de conexión
            SqlConnection conexion = new SqlConnection();
            SqlCommand command = new SqlCommand(StoredProcedure, conexion);
            SqlDataReader dr = null;

            ListItem item = new ListItem();
            item.Value = "0";
            item.Text = "--Seleccione aquí--";
            catalogo.Items.Add(item);

            try
            {
                conexion.ConnectionString = ConexionBD.Obtener();
                conexion.Open();

                command.CommandType = CommandType.StoredProcedure;

                dr = command.ExecuteReader();

                //Creando y añadiendo cada item con los valores del result
                while (dr.Read())
                {
                    ListItem li = new ListItem();
                    li.Value = dr[0].ToString();
                    li.Text = dr[1].ToString();
                    catalogo.Items.Add(li);
                }
            }
            catch (Exception e)
            {
            }
            finally
            {
                command.Connection.Close();
                //int.TryParse(command.ExecuteScalar.ToString(), out )
                conexion.Close();
                conexion.Dispose();
                conexion = null;
                dr = null;
            }

            return catalogo;
        }

        public static DropDownList GenerarCatalogo(string StoredProcedure, string parametro, int valor)
        {
            //Creando objeto a devolver
            DropDownList catalogo = new DropDownList();

            //Creando objeto de conexión
            SqlConnection conexion = new SqlConnection();
            SqlCommand command = new SqlCommand(StoredProcedure, conexion);
            SqlDataReader dr = null;
          
            ListItem item = new ListItem();
            item.Value = "0";
            item.Text = "--Seleccione aquí--";
            catalogo.Items.Add(item);

            try
            {
               conexion.ConnectionString = ConexionBD.Obtener();
               conexion.Open();


                //Añadiendo parametro al sp y su valor 
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue(parametro, valor);

                dr = command.ExecuteReader();

                //Creando y añadiendo cada item con los valores del result
                while (dr.Read())
                {
                    ListItem li = new ListItem();
                    li.Value = dr[0].ToString();
                    li.Text = dr[1].ToString();
                    catalogo.Items.Add(li);
                }
            }
            catch (Exception error)
            {

            }
            finally
            {
                command.Connection.Close();
                //int.TryParse(command.ExecuteScalar.ToString(), out )
                conexion.Close();
                conexion.Dispose();
                conexion = null;
                dr = null;
            }

            return catalogo;
        }

        //RRS 13082020
        public static DropDownList GenerarCatalogo(string StoredProcedure, string[] parametros, string[] valores)
        {
            //Creando objeto a devolver
            DropDownList catalogo = new DropDownList();

            //Creando objeto de conexión
            SqlConnection conexion = new SqlConnection();
            SqlCommand command = new SqlCommand(StoredProcedure, conexion);
            SqlDataReader dr = null;

            ListItem item = new ListItem();
            item.Value = "0";
            item.Text = "--Seleccione aquí--";
            catalogo.Items.Add(item);

            try
            {
                conexion.ConnectionString = ConexionBD.Obtener();
                conexion.Open();
                //Añadiendo parámetros y valores al sp
                for (int i = 0; i < parametros.Length; i++)
                {
                    command.Parameters.AddWithValue(parametros[i], valores[i]);
                }


                command.CommandType = CommandType.StoredProcedure;

                dr = command.ExecuteReader();

                //Creando y añadiendo cada item con los valores del result
                while (dr.Read())
                {
                    ListItem li = new ListItem();
                    li.Value = dr[0].ToString();
                    li.Text = dr[1].ToString();
                    catalogo.Items.Add(li);
                }
            }
            catch (Exception e)
            {
            }
            finally
            {
                command.Connection.Close();
                //int.TryParse(command.ExecuteScalar.ToString(), out )
                conexion.Close();
                conexion.Dispose();
                conexion = null;
                dr = null;
            }

            return catalogo;
        }


        public static void ProcedimientoAlmacenadoInsercion(string StoredProcedure, string[] parametros, string[] valores)
        {
            SqlDataReader dr = null;
            SqlConnection conexion = new SqlConnection();
            SqlCommand command = new SqlCommand(StoredProcedure, conexion);
            try
            {
                if (parametros.Length == valores.Length)
                {
                    conexion.ConnectionString = ConexionBD.Obtener();
                    conexion.Open();

                    //Añadiendo el nombre del spr               

                    command.CommandType = CommandType.StoredProcedure;

                    //Añadiendo parámetros y valores al sp
                    for (int i = 0; i < parametros.Length; i++)
                    {
                        if (valores[i] == null || valores[i] == "")
                        {
                            command.Parameters.AddWithValue(parametros[i], DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue(parametros[i], valores[i]);
                        }

                    }

                    dr = command.ExecuteReader();
                }
                else
                {
                    Console.WriteLine("La cantidad de parámetros no coincide con la cantidad de valores");
                }
            }
            catch (Exception error)
            {
            }
            finally
            {
                command.Connection.Close();
                //int.TryParse(command.ExecuteScalar.ToString(), out )
                conexion.Close();
                conexion.Dispose();
                conexion = null;
                dr = null;
            }
        }


    }
}