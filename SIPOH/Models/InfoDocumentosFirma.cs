using DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SIPOH.Models
{
    public class InfoDocumentosFirma
    {
        public long IdSolicitudBuzon { get; set; }
        public long IdDocDigital { get; set; }
        public string DigestHash { get; set; }
        public string RutapdfOriginal { get; set; }
        public string Rutapdf_Firmado { get; set; }
        public string Nombrearchivopdf_Original { get; set; }
        public string Nombrearchivopdf_Firmado { get; set; }
        public string NUC { get; set; }
        public string NombreJuzgado { get; set; }
        public string Solicitud { get; set; }

        public static List<InfoDocumentosFirma> ObtenerdatosDocumentosDigitales(long IdSolicitudBuzon)
        {
            List<InfoDocumentosFirma> infoDocumentosFirmaList = new List<InfoDocumentosFirma>();
            SqlCommand sqlCommand = new SqlCommand("[dbo].[spr_obtener_datosDocumentosDigitales]", new SqlConnection(ConexionBD.Obtener()));
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@IdSolicitudBuzon", SqlDbType.BigInt).Value = IdSolicitudBuzon;
            try
            {
                sqlCommand.Connection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                    infoDocumentosFirmaList.Add(new InfoDocumentosFirma()
                    {
                        IdSolicitudBuzon = BdConverter.FieldToInt64(sqlDataReader["IdSolicitudBuzon"]),
                        IdDocDigital = BdConverter.FieldToInt64(sqlDataReader["IdDocDigital"]),
                        DigestHash = BdConverter.FieldToString(sqlDataReader["DigestHash"]),
                        RutapdfOriginal = BdConverter.FieldToString(sqlDataReader["RutapdfOriginal"]),
                        Rutapdf_Firmado = BdConverter.FieldToString(sqlDataReader["Rutapdf_Firmado"]),
                        Nombrearchivopdf_Original = BdConverter.FieldToString(sqlDataReader["Nombrearchivopdf_Original"]),
                        Nombrearchivopdf_Firmado = BdConverter.FieldToString(sqlDataReader["Nombrearchivopdf_Firmado"]),
                        NUC = BdConverter.FieldToString(sqlDataReader["NUC"]),
                        NombreJuzgado = BdConverter.FieldToString(sqlDataReader["NombreJuzgado"]),
                        Solicitud = BdConverter.FieldToString(sqlDataReader["Solicitud"])
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
            return infoDocumentosFirmaList;
        }

    }
}