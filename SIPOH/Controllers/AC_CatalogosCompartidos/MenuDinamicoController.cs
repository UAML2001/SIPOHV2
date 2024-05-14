using DatabaseConnection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIPOH.Controllers.AC_CatalogosCompartidos
{
    public class MenuDinamicoController : Controller
    {

        public static DataTable ObtenerCatEnlacesPorPerfilToMenuDinamico(string Tipo, int IdPerfil)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new ConexionBD().Connection)
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("SELECT dbo.P_PermisosAsociados.IdPerfil, dbo.P_CatPermisos.Nombre, dbo.P_CatPermisos.linkEnlace, dbo.P_CatPermisos.Nombreicono, dbo.P_CatPermisos.Tipo FROM dbo.P_PermisosAsociados INNER JOIN dbo.P_CatPermisos ON dbo.P_PermisosAsociados.IdPermiso = dbo.P_CatPermisos.IdPermiso WHERE(dbo.P_PermisosAsociados.IdPerfil = @IdPerfil) AND (dbo.P_CatPermisos.Tipo = @Tipo)", connection))
                    {
                        command.Parameters.AddWithValue("@Tipo", Tipo);
                        command.Parameters.AddWithValue("@IdPerfil", IdPerfil);
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
    }
}
