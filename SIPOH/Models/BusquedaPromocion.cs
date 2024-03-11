using DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SIPOH.Models
{
    public class BusquedaPromocion
    {
        public string TipoAsunto { get; set; }
        public string Numero { get; set; }
        public string NUC { get; set; }
        public string Delitos { get; set; }
        public string Inculpados { get; set; }
        public string Victimas { get; set; }
        public long IdAsunto { get; set; }
        public string NumeroAmparo { get; set; }
        public string AutoridadResponsable { get; set; }
        public string Estatus { get; set; }
        public string Etapa { get; set; }

        public static List<BusquedaPromocion> ObtenerPromocion(string DataNumero, string DataTipoAsunto, string IdJuzgado)
        {
            List<BusquedaPromocion> lista = new List<BusquedaPromocion>();
            using (SqlConnection connection = new ConexionBD().Connection)
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("P_GetPromocion", connection))
                    {
                        command.Parameters.AddWithValue("@IdJuzgado", IdJuzgado);
                        command.Parameters.AddWithValue("@DataNumero", DataNumero);
                        command.Parameters.AddWithValue("@DataTipoAsunto", DataTipoAsunto);
                        command.CommandType = CommandType.StoredProcedure;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                BusquedaPromocion busquedaPromocion = new BusquedaPromocion();

                                busquedaPromocion.TipoAsunto = BdConverter.FieldToString( reader["TipoAsunto"]);
                                busquedaPromocion.Numero = BdConverter.FieldToString(reader["Numero"]);
                                busquedaPromocion.NUC = BdConverter.FieldToString(reader["NUC"]);
                                busquedaPromocion.Delitos = BdConverter.FieldToString(reader["Delitos"]);
                                busquedaPromocion.Inculpados = BdConverter.FieldToString(reader["Inculpados"]);
                                busquedaPromocion.Victimas = BdConverter.FieldToString(reader["Victimas"]);
                                busquedaPromocion.IdAsunto = BdConverter.FieldToInt64(reader["IdAsunto"]);
                                busquedaPromocion.NumeroAmparo = BdConverter.FieldToString(reader["NumeroAmparo"]);
                                busquedaPromocion.AutoridadResponsable = BdConverter.FieldToString(reader["AutoridadResponsable"]);
                                busquedaPromocion.Estatus = BdConverter.FieldToString(reader["Estatus"]);
                                busquedaPromocion.Etapa = BdConverter.FieldToString(reader["Etapa"]);
                                lista.Add(busquedaPromocion);
                            }
                        }
                    }

                }
                catch (Exception ex)
                {

                }
                return lista;
            }
        }

    }
}