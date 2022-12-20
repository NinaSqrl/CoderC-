using Microsoft.AspNetCore.Hosting.Server;
using ProyectofinalCoradini.Models;
using System.Data;
using System.Data.SqlClient;

namespace ProyectofinalCoradini.ADO.NET
{
    public class LoginHandler
    {
        private SqlConnection? conexion;
        private String stringConexion = 
            //"Server = sql.bsite.net\\MSSQL2016;" +
            //"Database=ajomuch92_coderhouse_csharp_40930;" +
            //"User Id=ajomuch92_coderhouse_csharp_40930;" +
            //"Password=ElQuequit0Sexy2022;";
            "Server=sql.bsite.net\\MSSQL2016;" +
            "Database=ninasqrl_ProyectofinalCoradini;" +
            "User Id=ninasqrl_ProyectofinalCoradini;" +
            "Password=Cleo__24;";

        public LoginHandler()
        {
            try
            {
                conexion = new SqlConnection(stringConexion);
            }
            catch (Exception ex)
            {

            }
        }

        public bool LoginUsuario(Usuario usuario)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM usuario WHERE NombreUsuario = @NombreUsuario AND Contrasenia = @Contrasenia", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario });
                    cmd.Parameters.Add(new SqlParameter("Contrasenia", SqlDbType.VarChar) { Value = usuario.Contrasenia });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
        }
    }
}