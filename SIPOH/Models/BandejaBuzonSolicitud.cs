using DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SIPOH.Models
{
    public class BandejaBuzonSolicitud
    {
        public long IdSolicitudBuzon { get; set; }
        public string Solicitud { get; set; }
        public string Tipo { get; set; }
        public string Observaciones { get; set; }
        public string TipoAsunto { get; set; }
        public string Numero { get; set; }
        public DateTime FeIngreso { get; set; }
        public int IdUsuarioExterno { get; set; }
        public string Estatus { get; set; }
        public DateTime Fecha { get; set; }
        public string RutaDoc { get; set; }


        public string FeIngresoMostrar
        {
            get
            {
                if (FeIngreso == DateTime.MinValue)
                    return "EN ESPERA";
                else
                    return FeIngreso.ToString("dd/MM/yyyy HH:mm");
            }
        }

        public static List<BandejaBuzonSolicitud> ObtenerBandejaBuzonSolicitud(long IdUsuarioExterno)
        {
            List<BandejaBuzonSolicitud> lista = new List<BandejaBuzonSolicitud>();



            SqlCommand sqlCommand = new SqlCommand("[dbo].[spr_ObtenerBandejaSeguimientoBuzon]", new SqlConnection(ConexionBD.Obtener()));
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@IdUsuarioExterno", SqlDbType.BigInt).Value = (object)IdUsuarioExterno;
            try
            {
                sqlCommand.Connection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                    lista.Add(new BandejaBuzonSolicitud()
                    {
                        IdSolicitudBuzon = BdConverter.FieldToInt64(sqlDataReader[nameof(IdSolicitudBuzon)]),
                        Solicitud = BdConverter.FieldToString(sqlDataReader["Solicitud"]),
                        Tipo = BdConverter.FieldToString(sqlDataReader["Tipo"]),
                        Observaciones = BdConverter.FieldToString(sqlDataReader["Observaciones"]),
                        TipoAsunto = BdConverter.FieldToString(sqlDataReader["TipoAsunto"]),
                        Numero = BdConverter.FieldToString(sqlDataReader["Numero"]),
                        FeIngreso = BdConverter.FieldToDate(sqlDataReader["FeIngreso"]),
                        IdUsuarioExterno = BdConverter.FieldToInt(sqlDataReader["IdUsuarioExterno"]),
                        Estatus = BdConverter.FieldToString(sqlDataReader["Estatus"]),
                        Fecha = BdConverter.FieldToDate(sqlDataReader["Fecha"]),
                        RutaDoc = BdConverter.FieldToString(sqlDataReader["RutaDoc"])

                    });
                sqlCommand.Connection.Close();
                sqlDataReader.Close();
            }
            catch (Exception ex)
            {
                if (sqlCommand.Connection.State != ConnectionState.Closed)
                    sqlCommand.Connection.Close();
            }
            finally
            {
                sqlCommand.Connection.Close();
                sqlCommand.Connection.Dispose();
            }
            return lista;
        }

        public static List<BandejaBuzonSolicitud> ObtenerBandejaBuzonSolicitudxTipoYNumero(string TipoAsunto,string Numero)
        {
            List<BandejaBuzonSolicitud> lista = new List<BandejaBuzonSolicitud>();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[spr_ObtenerBandejaSeguimientoBuzonxTipoYNumero]", new SqlConnection(ConexionBD.Obtener()));
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@TipoAsunto", SqlDbType.VarChar).Value = (object)TipoAsunto;
            sqlCommand.Parameters.Add("@Numero", SqlDbType.VarChar).Value = (object)Numero;
            try
            {
                sqlCommand.Connection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                    lista.Add(new BandejaBuzonSolicitud()
                    {
                        IdSolicitudBuzon = BdConverter.FieldToInt64(sqlDataReader[nameof(IdSolicitudBuzon)]),
                        Solicitud = BdConverter.FieldToString(sqlDataReader["Solicitud"]),
                        Tipo = BdConverter.FieldToString(sqlDataReader["Tipo"]),
                        Observaciones = BdConverter.FieldToString(sqlDataReader["Observaciones"]),
                        TipoAsunto = BdConverter.FieldToString(sqlDataReader["TipoAsunto"]),
                        Numero = BdConverter.FieldToString(sqlDataReader["Numero"]),
                        FeIngreso = BdConverter.FieldToDate(sqlDataReader["FeIngreso"]),
                        IdUsuarioExterno = BdConverter.FieldToInt(sqlDataReader["IdUsuarioExterno"]),
                        Estatus = BdConverter.FieldToString(sqlDataReader["Estatus"]),
                        Fecha = BdConverter.FieldToDate(sqlDataReader["Fecha"]),
                        RutaDoc = BdConverter.FieldToString(sqlDataReader["RutaDoc"])

                    });
                sqlCommand.Connection.Close();
                sqlDataReader.Close();
            }
            catch (Exception ex)
            {
                if (sqlCommand.Connection.State != ConnectionState.Closed)
                    sqlCommand.Connection.Close();
            }
            finally
            {
                sqlCommand.Connection.Close();
                sqlCommand.Connection.Dispose();
            }
            return lista;
        }

        public static List<BandejaBuzonSolicitud> ObtenerBandejaBuzonSolicitudxFolio(string IdSolicitudBuzon)
        {
            List<BandejaBuzonSolicitud> lista = new List<BandejaBuzonSolicitud>();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[spr_ObtenerBandejaSeguimientoBuzonxFolio]", new SqlConnection(ConexionBD.Obtener()));
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@IdSolicitudBuzon", SqlDbType.BigInt).Value = (object)IdSolicitudBuzon;
            try
            {
                sqlCommand.Connection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                    lista.Add(new BandejaBuzonSolicitud()
                    {
                        IdSolicitudBuzon = BdConverter.FieldToInt64(sqlDataReader[nameof(IdSolicitudBuzon)]),
                        Solicitud = BdConverter.FieldToString(sqlDataReader["Solicitud"]),
                        Tipo = BdConverter.FieldToString(sqlDataReader["Tipo"]),
                        Observaciones = BdConverter.FieldToString(sqlDataReader["Observaciones"]),
                        TipoAsunto = BdConverter.FieldToString(sqlDataReader["TipoAsunto"]),
                        Numero = BdConverter.FieldToString(sqlDataReader["Numero"]),
                        FeIngreso = BdConverter.FieldToDate(sqlDataReader["FeIngreso"]),
                        IdUsuarioExterno = BdConverter.FieldToInt(sqlDataReader["IdUsuarioExterno"]),
                        Estatus = BdConverter.FieldToString(sqlDataReader["Estatus"]),
                        Fecha = BdConverter.FieldToDate(sqlDataReader["Fecha"]),
                        RutaDoc = BdConverter.FieldToString(sqlDataReader["RutaDoc"])

                    });
                sqlCommand.Connection.Close();
                sqlDataReader.Close();
            }
            catch (Exception ex)
            {
                if (sqlCommand.Connection.State != ConnectionState.Closed)
                    sqlCommand.Connection.Close();
            }
            finally
            {
                sqlCommand.Connection.Close();
                sqlCommand.Connection.Dispose();
            }
            return lista;
        }


        public static List<BandejaBuzonSolicitud> ObtenerBandejaBuzonSolicitudxNUC(string NUC)
        {
            List<BandejaBuzonSolicitud> lista = new List<BandejaBuzonSolicitud>();

            SqlCommand sqlCommand = new SqlCommand("[dbo].[spr_ObtenerBandejaSeguimientoBuzonxNUC]", new SqlConnection(ConexionBD.Obtener()));
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@NUC", SqlDbType.VarChar).Value = (object)NUC;
            try
            {
                sqlCommand.Connection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                    lista.Add(new BandejaBuzonSolicitud()
                    {
                        IdSolicitudBuzon = BdConverter.FieldToInt64(sqlDataReader[nameof(IdSolicitudBuzon)]),
                        Solicitud = BdConverter.FieldToString(sqlDataReader["Solicitud"]),
                        Tipo = BdConverter.FieldToString(sqlDataReader["Tipo"]),
                        Observaciones = BdConverter.FieldToString(sqlDataReader["Observaciones"]),
                        TipoAsunto = BdConverter.FieldToString(sqlDataReader["TipoAsunto"]),
                        Numero = BdConverter.FieldToString(sqlDataReader["Numero"]),
                        FeIngreso = BdConverter.FieldToDate(sqlDataReader["FeIngreso"]),
                        IdUsuarioExterno = BdConverter.FieldToInt(sqlDataReader["IdUsuarioExterno"]),
                        Estatus = BdConverter.FieldToString(sqlDataReader["Estatus"]),
                        Fecha = BdConverter.FieldToDate(sqlDataReader["Fecha"]),
                        RutaDoc = BdConverter.FieldToString(sqlDataReader["RutaDoc"])

                    });
                sqlCommand.Connection.Close();
                sqlDataReader.Close();
            }
            catch (Exception ex)
            {
                if (sqlCommand.Connection.State != ConnectionState.Closed)
                    sqlCommand.Connection.Close();
            }
            finally
            {
                sqlCommand.Connection.Close();
                sqlCommand.Connection.Dispose();
            }
            return lista;
        }

    }
}