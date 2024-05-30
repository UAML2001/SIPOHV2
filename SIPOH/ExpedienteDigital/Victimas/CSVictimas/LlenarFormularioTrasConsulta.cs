﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public class InformacionFormulario
{
    public string APaterno { get; set; }
    public string AMaterno { get; set; }
    public string Nombre { get; set; }
    public string GeneroNumerico { get; set; }
    public string TipoVictima { get; set; }
    public string Victima { get; set; }
    public string RFC { get; set; }
    public string CURP { get; set; }
    public DateTime? FeNacimiento { get; set; }
    public string Edad { get; set; }
    public string IdContinenteNacido { get; set; }
    public string IdPaisNacido { get; set; }
    public string IdEstadoNacido { get; set; }
    public string IdMunicipioNacido { get; set; }
    public string IdNacionalidad { get; set; }
    public string IdiomaEspañol { get; set; }
    public string IdDialecto { get; set; }
    public string IdVulnerabilidad { get; set; }
    public string IdCondicion { get; set; }
    public string IdAlfabet { get; set; }
    public string HablaIndigena { get; set; }
    public string IdPueblo { get; set; }
    public string DomiTrabVicti { get; set; }
    public string DomOcupacion { get; set; }
    public string IdEstadoCivil { get; set; }
    public string IdOcupacion { get; set; }
    public string IdGradoEstudios { get; set; }
    public string IdProfesion { get; set; }
    public string Discapacidad { get; set; }
    public string DomResidencia { get; set; }
    public string IdDefensor { get; set; }
    public string Interprete { get; set; }
    public string Telefono { get; set; }
    public string Correo { get; set; }
    public string Fax { get; set; }
    public string IdDocIdentificador { get; set; }
    public string FeIndividualización { get; set; }
    public string DomNotificacion { get; set; }
    public string Privacidad { get; set; }
    public DataTable dtDiscapacidades { get; set; }
    public string IdContinenteResidencia { get; set; }
    public string IdPaisResidencia { get; set; }
    public string IdEstadoResidencia { get; set; }
    public string IdMunicipioResidencia { get; set; }
}

public class LlenarFormularioTrasConsulta
{
    private string connectionString;

    public LlenarFormularioTrasConsulta()
    {
        connectionString = ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString;
    }

    public InformacionFormulario LlenarFormulario(int idAsunto, int idPartes)
    {
        InformacionFormulario info = new InformacionFormulario();

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("ObtenerInformacionVictimaPorAsuntoYParte", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdAsunto", idAsunto);
                cmd.Parameters.AddWithValue("@IdParte", idPartes);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    info.APaterno = reader["APaterno"].ToString();
                    info.AMaterno = reader["AMaterno"].ToString();
                    info.Nombre = reader["Nombre"].ToString();
                    info.GeneroNumerico = reader["GeneroNumerico"].ToString();
                    info.TipoVictima = reader["TipoVictima"].ToString();
                    info.Victima = reader["Victima"].ToString();
                    info.RFC = reader["RFC"].ToString();
                    info.CURP = reader["CURP"].ToString();
                    if (reader["FeNacimiento"] != DBNull.Value)
                    {
                        info.FeNacimiento = Convert.ToDateTime(reader["FeNacimiento"]);
                    }
                    info.Edad = reader["Edad"].ToString();
                    if (reader["IdContinenteNacido"] != DBNull.Value)
                    {
                        info.IdContinenteNacido = reader["IdContinenteNacido"].ToString();
                    }
                    if (reader["IdPaisNacido"] != DBNull.Value)
                    {
                        info.IdPaisNacido = reader["IdPaisNacido"].ToString();
                    }
                    if (reader["IdEstadoNacido"] != DBNull.Value)
                    {
                        info.IdEstadoNacido = reader["IdEstadoNacido"].ToString();
                    }
                    if (reader["IdMunicipioNacido"] != DBNull.Value)
                    {
                        info.IdMunicipioNacido = reader["IdMunicipioNacido"].ToString();
                    }
                    info.IdNacionalidad = reader["IdNacionalidad"].ToString();
                    info.IdiomaEspañol = reader["IdiomaEspañol"].ToString();
                    info.IdDialecto = reader["IdDialecto"].ToString();
                    info.IdVulnerabilidad = reader["IdVulnerabilidad"].ToString();
                    info.IdCondicion = reader["IdCondicion"].ToString();
                    info.IdAlfabet = reader["IdAlfabet"].ToString();
                    info.HablaIndigena = reader["HablaIndigena"].ToString();
                    info.IdPueblo = reader["IdPueblo"].ToString();
                    info.DomiTrabVicti = reader["DomOcupacion"].ToString();
                    info.IdEstadoCivil = reader["IdEstadoCivil"].ToString();
                    info.IdOcupacion = reader["IdOcupacion"].ToString();
                    info.IdGradoEstudios = reader["IdGradoEstudios"].ToString();
                    if (reader["IdProfesion"] != DBNull.Value)
                    {
                        info.IdProfesion = reader["IdProfesion"].ToString();
                    }
                    if (reader["Discapacidad"] != DBNull.Value)
                    {
                        info.Discapacidad = reader["Discapacidad"].ToString();
                    }

                    if (reader["IdContinenteResidencia"] != DBNull.Value)
                    {
                        info.IdContinenteResidencia = reader["IdContinenteResidencia"].ToString();
                    }
                    if (reader["IdPaisResidencia"] != DBNull.Value)
                    {
                        info.IdPaisResidencia = reader["IdPaisResidencia"].ToString();
                    }
                    if (reader["IdEstadoResidencia"] != DBNull.Value)
                    {
                        info.IdEstadoResidencia = reader["IdEstadoResidencia"].ToString();
                    }
                    if (reader["IdMunicipioResidencia"] != DBNull.Value)
                    {
                        info.IdMunicipioResidencia = reader["IdMunicipioResidencia"].ToString();
                    }

                    info.DomResidencia = reader["DomResidencia"].ToString();
                    info.IdDefensor = reader["IdDefensor"].ToString();
                    info.Interprete = reader["Interprete"].ToString();
                    info.Telefono = reader["Telefono"].ToString();
                    info.Correo = reader["Correo"].ToString();
                    info.Fax = reader["Fax"].ToString();
                    info.IdDocIdentificador = reader["IdDocIdentificador"].ToString();
                    info.FeIndividualización = reader["FeIndividualización"].ToString();
                    info.DomNotificacion = reader["DomNotificacion"].ToString();
                    info.Privacidad = reader["Privacidad"].ToString();

                    // Crear un DataTable y agregar las columnas necesarias
                    info.dtDiscapacidades = new DataTable();
                    info.dtDiscapacidades.Columns.Add("DiscapacidadAgregada", typeof(string));

                    // Mover el lector al siguiente registro y llenar el DataTable directamente
                    while (reader.Read())
                    {
                        DataRow row = info.dtDiscapacidades.NewRow();
                        row["DiscapacidadAgregada"] = reader["DiscapacidadAgregada"].ToString();
                        info.dtDiscapacidades.Rows.Add(row);
                    }

                }
            }
        }

        return info;
    }
}