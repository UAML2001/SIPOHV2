using DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SIPOH.Models
{
    public class DocumentoTransfer
    {
        public long IdSolicitudBuzon { get; set; }
        public long IdDocDigital { get; set; }
        public string DigestHash { get; set; }
        public string RutapdfOriginal { get; set; }
        public string Rutapdf_Firmado { get; set; }
        public string Nombrearchivopdf_Original { get; set; }
        public string Nombrearchivopdf_Firmado { get; set; }

        public int IdTransfer { get; set; }
        public string Descripcion { get; set; }
        public string Huella { get; set; }
        public string CN { get; set; }
        public string HexSerie { get; set; }
        public DateTime Fecha { get; set; }

        public static List<DocumentoTransfer> ObtenerDocumentosTransfer(long IdSolicitudBuzon)
        {
            List<DocumentoTransfer> documentoTransferList = new List<DocumentoTransfer>();
            SqlCommand sqlCommand = new SqlCommand("[dbo].[spr_ObtenerDocumentosTransfer]", new SqlConnection(ConexionBD.Obtener()));
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@IdSolicitudBuzon", SqlDbType.BigInt).Value = (object)IdSolicitudBuzon;
            try
            {
                sqlCommand.Connection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                    documentoTransferList.Add(new DocumentoTransfer()
                    {
                        IdSolicitudBuzon = BdConverter.FieldToInt64(sqlDataReader[nameof(IdSolicitudBuzon)]),
                        IdTransfer = BdConverter.FieldToInt(sqlDataReader["IdTransfer"]),
                        Fecha = BdConverter.FieldToDate(sqlDataReader["Fecha"]),
                        CN = BdConverter.FieldToString(sqlDataReader["CN"]),
                        IdDocDigital = BdConverter.FieldToInt64(sqlDataReader["IdDocDigital"])

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
            return documentoTransferList;
        }

        public static int InsertarDocumentoTransfer( DocumentoTransfer DocTransfer)
        {
            int result = -1;
            object obj1 = new object();
            SqlCommand sqlCommand = new SqlCommand("[dbo].[spr_insertar_documento_transfer]", new SqlConnection(ConexionBD.Obtener()));
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add("@IdDocDigital", SqlDbType.BigInt).Value = (object)DocTransfer.IdDocDigital;
            if (DocTransfer.IdTransfer > 0)
                sqlCommand.Parameters.Add("@IdTransfer", SqlDbType.Int).Value = (object)DocTransfer.IdTransfer;
            if (!string.IsNullOrEmpty(DocTransfer.Descripcion))
                sqlCommand.Parameters.Add("@Descripcion", SqlDbType.VarChar, 1000).Value = (object)DocTransfer.Descripcion;
            if (!string.IsNullOrEmpty(DocTransfer.Huella))
                sqlCommand.Parameters.Add("@Huella", SqlDbType.VarChar, 1000).Value = (object)DocTransfer.Huella;
            if (!string.IsNullOrEmpty(DocTransfer.CN))
                sqlCommand.Parameters.Add("@Cn", SqlDbType.VarChar, 1000).Value = (object)DocTransfer.CN;
            if (!string.IsNullOrEmpty(DocTransfer.HexSerie))
                sqlCommand.Parameters.Add("@HexSerie", SqlDbType.VarChar, 1000).Value = (object)DocTransfer.HexSerie;
            DateTime fecha = DocTransfer.Fecha;
            if (DocTransfer.Fecha != DateTime.MinValue)
                sqlCommand.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = (object)DocTransfer.Fecha;
            try
            {
                sqlCommand.Connection.Open();
                object obj2 = sqlCommand.ExecuteScalar();
                sqlCommand.Connection.Close();
                int.TryParse(obj2.ToString(), out result);
            }
            catch (Exception ex)
            {
                if (sqlCommand.Connection.State != ConnectionState.Closed)
                    sqlCommand.Connection.Close();
            }
            finally
            {
                sqlCommand.Connection.Dispose();
            }
            return result;
        }

    }
}