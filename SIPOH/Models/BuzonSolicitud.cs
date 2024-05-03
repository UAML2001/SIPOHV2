using DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace SIPOH.Models
{
    public class BuzonSolicitud
    {
        public long IdSolicitudBuzon { get; set; }
        public string NUC { get; set; }
        public int IdSolicitud { get; set; }
        public DateTime Fecha { get; set; }
        public int IdJuzgado { get; set; }
        public string DescripAnexos { get; set; }
        public string PosiblesHechos { get; set; }
        public int IdSolicitante { get; set; }
        public int IdUsuarioExterno { get; set; }
        public long IdAsunto { get; set; }
        public string Tipo { get; set; }
        public string Promovente { get; set; }
        public DateTime FeAceptacion { get; set; }
        public string Observaciones { get; set; }
        public string Estatus { get; set; }
       
        /// <summary>
        /// Datos de obtencion de la base
        /// </summary>
        public string Solicitud { get; set; }
        /// <summary>
        /// Datos de obtencion
        /// </summary>
        public string Solicitante { get; set; }
        /// <summary>
        /// Datos de obtencion
        /// </summary>
        public string NombreJuzgado { get; set; }
        /// <summary>
        /// Datos de obtencion
        /// </summary>
        public string RutapdfOriginal { get; set; }
        /// <summary>
        /// Datos de obtencion
        /// </summary>
        public string Nombrearchivopdf_Original { get; set; }





        public static bool Insertarsolicitud(BuzonSolicitud buzon, byte[] documento,  ref long IdSolicitudBuzon, ref string rutaDoc)
        {

            bool success = true;
            string DirCarpetaJuzgado = "";
            string DirCarpetaExpe = "";
            string NombreArchivo = "";

            SqlConnection Conn = new SqlConnection(ConexionBD.Obtener());
            SqlTransaction lTransaccion = null;
            IdSolicitudBuzon = 0;
           long  IdDocDigital = 0;
            try
            {
                Conn.Open();
                lTransaccion = Conn.BeginTransaction(System.Data.IsolationLevel.Serializable);

                SqlCommand cmd = new SqlCommand("spr_insertar_BuzonSolicitud", Conn, lTransaccion);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@IdSolicitud", buzon.IdSolicitud));
                cmd.Parameters.Add(new SqlParameter("@IdJuzgado", buzon.IdJuzgado));
                cmd.Parameters.Add(new SqlParameter("@DescripAnexos", buzon.DescripAnexos));
                cmd.Parameters.Add(new SqlParameter("@PosiblesHechos", buzon.PosiblesHechos));
                cmd.Parameters.Add(new SqlParameter("@IdSolicitante", buzon.IdSolicitante));
                cmd.Parameters.Add(new SqlParameter("@IdUsuarioExterno", buzon.IdUsuarioExterno));
                cmd.Parameters.Add(new SqlParameter("@Tipo", buzon.Tipo));
                cmd.Parameters.Add(new SqlParameter("@Estatus", buzon.Estatus));
                cmd.Parameters.Add(new SqlParameter("@NUC", buzon.NUC));


                if (buzon.Tipo == "P")
                {
                    cmd.Parameters.Add(new SqlParameter("@Promovente", buzon.Promovente));
                    cmd.Parameters.Add(new SqlParameter("@IdAsunto", buzon.IdAsunto));
                }


                IdSolicitudBuzon = Convert.ToInt64(cmd.ExecuteScalar());
                if (IdSolicitudBuzon > 0)
                    success = true;
                else
                    success = false;
               


                DirCarpetaJuzgado = "Solicitudes/" + buzon.IdJuzgado + "/";
                DirCarpetaExpe = DirCarpetaJuzgado  + IdSolicitudBuzon + "/";
                NombreArchivo = $"S_{IdSolicitudBuzon.ToString()}_O.pdf";


                //CREAR CARPETA JUZGADO
                if (success)
                {
                    success = ArchivosFTPFirma.VerificarDirectorioFTP(DirCarpetaJuzgado);

                    if (!success)
                        success = ArchivosFTPFirma.CrearDirectorioFTP(DirCarpetaJuzgado);
                }

                //CREAR CARPETA EXPEDIENTE
                if (success)
                {
                     success = ArchivosFTPFirma.VerificarDirectorioFTP(DirCarpetaExpe);
                    if (!success)
                        success = ArchivosFTPFirma.CrearDirectorioFTP(DirCarpetaExpe);
                }

                if (success)
                {
                    Stream stream = new MemoryStream(documento);
                    success = ArchivosFTPFirma.CrearArchivoPDF(stream, DirCarpetaExpe + NombreArchivo);
                }


                if (success)
                {

                        cmd = new SqlCommand("spr_insertar_BuzonDocDigitales", Conn, lTransaccion);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@IdSolicitudBuzon", IdSolicitudBuzon);
                        cmd.Parameters.AddWithValue("@RutapdfOriginal", DirCarpetaExpe);
                        cmd.Parameters.AddWithValue("@Nombrearchivopdf_Original", NombreArchivo);


                    IdDocDigital = Convert.ToInt64(cmd.ExecuteScalar());
                    if (IdDocDigital > 0)
                        success = true;
                    else
                        success = false;
                }

                if (success)
                {
                    string RutaArchivo = DirCarpetaExpe + NombreArchivo;
                    bool Generado = false;

                    //Generar Hash
                    byte[] data = ArchivosFTPFirma.ObtenerArchivoFTP(RutaArchivo);
                    SHA256CryptoServiceProvider sha = new SHA256CryptoServiceProvider();

                    byte[] result = sha.ComputeHash(data);
                    string DigestHash = System.Convert.ToBase64String(result);


                    cmd = new SqlCommand("spr_insertar_BuzonDocDigitalesHash", Conn, lTransaccion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IdDocDigital", IdDocDigital);
                    cmd.Parameters.AddWithValue("@DigestHash", DigestHash);


                    long res = Convert.ToInt64(cmd.ExecuteScalar());
                    if (res > 0)
                        success = true;
                    else
                        success = false;
                }


            }
            catch (Exception err)
            {
                success = false;
            }
            finally
            {
                if (success)
                {
                    rutaDoc = DirCarpetaExpe + NombreArchivo;
                    lTransaccion.Commit();
                    Conn.Close();
                }
                else
                {

                    bool existe = ArchivosFTPFirma.VerificarArchivoFTP(DirCarpetaExpe + NombreArchivo);

                    if (existe)
                    {
                        bool ArchivoEliminado = ArchivosFTPFirma.EliminarArchivo(DirCarpetaExpe, NombreArchivo);
                    }
                    lTransaccion.Rollback();
                    Conn.Close();
                    Conn.Dispose();

                }
            }

            return success;

        }

        public static  BuzonSolicitud ObtenerbuzonSolicitud(int IdUsuarioExterno, string Tipo, ref bool resultado)
        {
            BuzonSolicitud solicitud = new BuzonSolicitud();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader rdr;

            resultado = false;

            cmd.Connection = new SqlConnection(ConexionBD.Obtener());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ObtenerBuzonSolicitudPendiente";

            cmd.Parameters.Add("@IdUsuarioExterno", SqlDbType.Int).Value = IdUsuarioExterno;
            cmd.Parameters.Add("@Tipo", SqlDbType.VarChar,1).Value = Tipo;

            try
            {
                cmd.Connection.Open();
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    solicitud.IdSolicitudBuzon = BdConverter.FieldToInt64(rdr["IdSolicitudBuzon"]);
                    solicitud.NUC = BdConverter.FieldToString(rdr["NUC"]);
                    solicitud.IdSolicitud = BdConverter.FieldToInt(rdr["IdSolicitud"]);
                    solicitud.Fecha = BdConverter.FieldToDate(rdr["Fecha"]);
                    solicitud.IdJuzgado = BdConverter.FieldToInt(rdr["IdJuzgado"]);
                    solicitud.DescripAnexos = BdConverter.FieldToString(rdr["DescripAnexos"]);
                    solicitud.PosiblesHechos = BdConverter.FieldToString(rdr["PosiblesHechos"]);
                    solicitud.IdSolicitante = BdConverter.FieldToInt(rdr["IdSolicitante"]);
                    solicitud.IdUsuarioExterno = BdConverter.FieldToInt(rdr["IdUsuarioExterno"]);
                    solicitud.Tipo = BdConverter.FieldToString(rdr["Tipo"]);
                    solicitud.Promovente = BdConverter.FieldToString(rdr["Promovente"]);
                    solicitud.Solicitud = BdConverter.FieldToString(rdr["Solicitud"]);
                    solicitud.Solicitante = BdConverter.FieldToString(rdr["Solicitante"]);
                    solicitud.NombreJuzgado = BdConverter.FieldToString(rdr["NombreJuzgado"]);
                    solicitud.RutapdfOriginal = BdConverter.FieldToString(rdr["RutapdfOriginal"]);
                    solicitud.Nombrearchivopdf_Original = BdConverter.FieldToString(rdr["Nombrearchivopdf_Original"]);
                }

                resultado = true;
                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                if (cmd.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();
                resultado = false;

            }

            return solicitud;
        }

        public static bool EliminarSolicitud(int IdSolicitudBuzon, string DirCarpetaExpe, string NombreArchivo)
        {

            bool success = false;

            



                SqlCommand cmd = new SqlCommand();


                cmd.Connection = new SqlConnection(ConexionBD.Obtener());
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spr_eliminar_BuzonSolicitud";

                cmd.Parameters.Add("@IdSolicitudBuzon", SqlDbType.BigInt).Value = IdSolicitudBuzon;

                try
                {
                    cmd.Connection.Open();

                    int respuesta = Convert.ToInt32(cmd.ExecuteScalar());

                    if (respuesta > 0)
                        success = true;
                    else
                        success = false;


                bool existe = ArchivosFTPFirma.VerificarArchivoFTP(DirCarpetaExpe + NombreArchivo);

                if (existe)
                {
                    success = ArchivosFTPFirma.EliminarArchivo(DirCarpetaExpe, NombreArchivo);
                }


                cmd.Connection.Close();
                }
                catch (Exception e)
                {
                    if (cmd.Connection.State == ConnectionState.Open)
                        cmd.Connection.Close();
                    success = false;

                } 

            return success;
        }


        public static bool modificar_doc_digital(long IdSolicitudBuzon, string ruta_firmado, string nombre_firmado)
        {
            bool success = false;

            SqlCommand cmd = new SqlCommand();


            cmd.Connection = new SqlConnection(ConexionBD.Obtener());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spr_modificar_doc_digital";

            cmd.Parameters.Add("@IdSolicitudBuzon", SqlDbType.BigInt).Value = IdSolicitudBuzon;
            cmd.Parameters.Add("@ruta_firmado", SqlDbType.VarChar).Value = ruta_firmado;
            cmd.Parameters.Add("@nombre_firmado", SqlDbType.VarChar).Value = nombre_firmado;

            try
            {
                cmd.Connection.Open();

                int respuesta = Convert.ToInt32(cmd.ExecuteScalar());

                if (respuesta > 0)
                    success = true;
                else
                    success = false;

                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                if (cmd.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();
                success = false;

            }

            return success;
        }

        public static bool modificar_buzonSolicitud(long IdSolicitudBuzon, string Estatus)
        {
            bool success = false;

            SqlCommand cmd = new SqlCommand();


            cmd.Connection = new SqlConnection(ConexionBD.Obtener());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "spr_modificar_buzonSolicitud";

            cmd.Parameters.Add("@IdSolicitudBuzon", SqlDbType.BigInt).Value = IdSolicitudBuzon;
            cmd.Parameters.Add("@Estatus", SqlDbType.VarChar).Value = Estatus;

            try
            {
                cmd.Connection.Open();

                int respuesta = Convert.ToInt32(cmd.ExecuteScalar());

                if (respuesta > 0)
                    success = true;
                else
                    success = false;

                cmd.Connection.Close();
            }
            catch (Exception e)
            {
                if (cmd.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();
                success = false;

            }

            return success;
        }
    }
}