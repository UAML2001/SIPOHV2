using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace SIPOH.Controllers.AC_CatalogosCompartidos
{
    // Dropdowns Datos Personales
    public class CatalogosVictimas
    {
        public void DropdownGenero(DropDownList dropdown)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT DescripcionTribunal, TipoSexo FROM [SIPOH].[dbo].[P_CatGenero]", conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListItem listItem = new ListItem();
                        listItem.Text = dr["DescripcionTribunal"].ToString();
                        listItem.Value = dr["TipoSexo"].ToString();
                        dropdown.Items.Add(listItem);
                    }
                }
            }

        }

        public void DropdownTipoVictima(DropDownList dropdown)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Descripción, TipoVictima FROM [SIPOH].[dbo].[P_CatTipoVictima]", conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListItem listItem = new ListItem();
                        listItem.Text = dr["Descripción"].ToString();
                        listItem.Value = dr["TipoVictima"].ToString();
                        dropdown.Items.Add(listItem);
                    }
                }
            }

        }

        public void DropdownTipoMoral(DropDownList dropdown)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT DESCRIPCION, MORAL_ID FROM [SIPOH].[dbo].[P_CatTipoMoral]", conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListItem listItem = new ListItem();
                        listItem.Text = dr["DESCRIPCION"].ToString();
                        listItem.Value = dr["MORAL_ID"].ToString();
                        dropdown.Items.Add(listItem);
                    }
                }
            }

        }



        public void DropdownContinentes(DropDownList dropdown)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Continente, IdContinente FROM [SIPOH].[dbo].[P_CatContinentes]", conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListItem listItem = new ListItem();
                        listItem.Text = dr["Continente"].ToString();
                        listItem.Value = dr["IdContinente"].ToString();
                        dropdown.Items.Add(listItem);
                    }
                }
            }

        }

        public void DropdownPaises(DropDownList dropdown, int idContinente)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString))
            {
                conn.Open();
                string query = "SELECT Pais, IdPais FROM [SIPOH].[dbo].[P_CatPais] WHERE IdContinente = @IdContinente";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdContinente", idContinente);
                    SqlDataReader dr = cmd.ExecuteReader();
                    dropdown.Items.Clear(); // Limpiar los elementos anteriores
                    while (dr.Read())
                    {
                        ListItem listItem = new ListItem();
                        listItem.Text = dr["Pais"].ToString();
                        listItem.Value = dr["IdPais"].ToString();
                        dropdown.Items.Add(listItem);
                    }
                }
            }
        }


        public void DropdownEntidades(DropDownList dropdown)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Nombre, IdEstados FROM [SIPOH].[dbo].[P_CatEntidades]", conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListItem listItem = new ListItem();
                        listItem.Text = dr["Nombre"].ToString();
                        listItem.Value = dr["IdEstados"].ToString();
                        dropdown.Items.Add(listItem);
                    }
                }
            }
        }


        public void DropdownMunicipios(DropDownList dropdown, string IdEstado)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Municipio, IdMunicipio FROM [SIPOH].[dbo].[P_CatMunicipios] WHERE IdEntidad = @IdEstado", conn))
                {
                    cmd.Parameters.AddWithValue("@IdEstado", IdEstado);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListItem listItem = new ListItem();
                        listItem.Text = dr["Municipio"].ToString();
                        listItem.Value = dr["IdMunicipio"].ToString();
                        dropdown.Items.Add(listItem);
                    }
                }
            }
        }


        // Dropdowns Datos Generales

        public void DropdownNacionalidad(DropDownList dropdown)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Nacionalidad, IdNacionalidad FROM [SIPOH].[dbo].[P_CatNacionalidades]", conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListItem listItem = new ListItem();
                        listItem.Text = dr["Nacionalidad"].ToString();
                        listItem.Value = dr["IdNacionalidad"].ToString();
                        dropdown.Items.Add(listItem);
                    }
                }
            }

        }

        public void DropdownCondicMigratoria(DropDownList dropdown)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Condicionmigratoria, IdCondicion FROM [SIPOH].[dbo].[P_CatCondicionMigratoria]", conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListItem listItem = new ListItem();
                        listItem.Text = dr["Condicionmigratoria"].ToString();
                        listItem.Value = dr["IdCondicion"].ToString();
                        dropdown.Items.Add(listItem);
                    }
                }
            }

        }

        public void DropdownEstadoCivil(DropDownList dropdown)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT [Descripcion Tribunal], [Tipo] FROM [SIPOH].[dbo].[P_CatEstadoCivil]", conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListItem listItem = new ListItem();
                        listItem.Text = dr["Descripcion Tribunal"].ToString();
                        listItem.Value = dr["Tipo"].ToString();
                        dropdown.Items.Add(listItem);
                    }
                }
            }

        }

        public void DropdownGradoEstudios(DropDownList dropdown)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Grado, IdGradoEstudios FROM [SIPOH].[dbo].[P_CatGradoEstudios]", conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListItem listItem = new ListItem();
                        listItem.Text = dr["Grado"].ToString();
                        listItem.Value = dr["IdGradoEstudios"].ToString();
                        dropdown.Items.Add(listItem);
                    }
                }
            }

        }

        public void DropdownCondicAlfabetismo(DropDownList dropdown)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT CondicionAlfabet, IdAlfabet FROM [SIPOH].[dbo].[P_CatCondicionAlfabetismo]", conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListItem listItem = new ListItem();
                        listItem.Text = dr["CondicionAlfabet"].ToString();
                        listItem.Value = dr["IdAlfabet"].ToString();
                        dropdown.Items.Add(listItem);
                    }
                }
            }

        }

        public void DropdownVulnerabilidad(DropDownList dropdown)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Vulnerabilidad, IdVulnerabilidad FROM [SIPOH].[dbo].[P_CatVulnerabilidad]", conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListItem listItem = new ListItem();
                        listItem.Text = dr["Vulnerabilidad"].ToString();
                        listItem.Value = dr["IdVulnerabilidad"].ToString();
                        dropdown.Items.Add(listItem);
                    }
                }
            }

        }

        public void DropdownPuebloIndigena(DropDownList dropdown)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT PuebloIndigena, IdPueblo FROM [SIPOH].[dbo].[P_CatPuebloIndigena]", conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListItem listItem = new ListItem();
                        listItem.Text = dr["PuebloIndigena"].ToString();
                        listItem.Value = dr["IdPueblo"].ToString();
                        dropdown.Items.Add(listItem);
                    }
                }
            }

        }

        public void DropdownLenguaIndigena(DropDownList dropdown)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Dialecto, IdDialecto FROM [SIPOH].[dbo].[P_CatLenguaIndigena]", conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListItem listItem = new ListItem();
                        listItem.Text = dr["Dialecto"].ToString();
                        listItem.Value = dr["IdDialecto"].ToString();
                        dropdown.Items.Add(listItem);
                    }
                }
            }

        }

        public void DropdownOcupacion(DropDownList dropdown)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Ocupacion, IdOcupacion FROM [SIPOH].[dbo].[P_CatOcupacion]", conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListItem listItem = new ListItem();
                        listItem.Text = dr["Ocupacion"].ToString();
                        listItem.Value = dr["IdOcupacion"].ToString();
                        dropdown.Items.Add(listItem);
                    }
                }
            }

        }

        public void DropdownProfesiones(DropDownList dropdown, string idOcupacion)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Profesion, IdProfesion FROM [SIPOH].[dbo].[P_CatProfesiones] WHERE IdOcupacion = @IdOcupacion", conn))
                {
                    cmd.Parameters.AddWithValue("@IdOcupacion", idOcupacion);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListItem listItem = new ListItem();
                        listItem.Text = dr["Profesion"].ToString();
                        listItem.Value = dr["IdProfesion"].ToString();
                        dropdown.Items.Add(listItem);
                    }
                }
            }
        }

        public void DropdownDiscapacidad(DropDownList dropdown)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Discapacidad, IdDiscapacidad FROM [SIPOH].[dbo].[P_CatDiscapacidad] WHERE sub_index = 9", conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListItem listItem = new ListItem();
                        listItem.Text = dr["Discapacidad"].ToString();
                        listItem.Value = dr["IdDiscapacidad"].ToString();
                        dropdown.Items.Add(listItem);
                    }
                }
            }
        }

        public void DropdownEspeDiscapacidad(DropDownList dropdown, string idDiscapacidad)
        {
            dropdown.Items.Clear();
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Discapacidad FROM [SIPOH].[dbo].[P_CatDiscapacidad] WHERE sub_index = @idDiscapacidad", conn))
                {
                    cmd.Parameters.AddWithValue("@idDiscapacidad", idDiscapacidad);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListItem listItem = new ListItem();
                        listItem.Text = dr["Discapacidad"].ToString();
                        listItem.Value = dr["Discapacidad"].ToString();
                        dropdown.Items.Add(listItem);
                    }
                }
            }
        }


        public void DropdownAsesorJuridico(DropDownList dropdown)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Defensor, Id_Defensor FROM [SIPOH].[dbo].[P_CatTipoDefensor]", conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListItem listItem = new ListItem();
                        listItem.Text = dr["Defensor"].ToString();
                        listItem.Value = dr["Id_Defensor"].ToString();
                        dropdown.Items.Add(listItem);
                    }
                }
            }
        }


        public void DropdownRelacion(DropDownList dropdown)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Relacion, IdRelacion FROM [SIPOH].[dbo].[P_CatRelacion]", conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListItem listItem = new ListItem();
                        listItem.Text = dr["Relacion"].ToString();
                        listItem.Value = dr["IdRelacion"].ToString();
                        dropdown.Items.Add(listItem);
                    }
                }
            }
        }

        public void DropdownIdentifiacion(DropDownList dropdown)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT DocIdentificacion, IdDocIdentificador FROM [SIPOH].[dbo].[P_CatDocIdentificacion]", conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListItem listItem = new ListItem();
                        listItem.Text = dr["DocIdentificacion"].ToString();
                        listItem.Value = dr["IdDocIdentificador"].ToString();
                        dropdown.Items.Add(listItem);
                    }
                }
            }
        }

        public void DropdownPreguntas(DropDownList dropdown)
        {
            using (SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SIPOHDB"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Pregunta, IdPregunta FROM [SIPOH].[dbo].[P_CatPregunta]", conn))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListItem listItem = new ListItem();
                        listItem.Text = dr["Pregunta"].ToString();
                        listItem.Value = dr["IdPregunta"].ToString();
                        dropdown.Items.Add(listItem);
                    }
                }
            }
        }


    }

}