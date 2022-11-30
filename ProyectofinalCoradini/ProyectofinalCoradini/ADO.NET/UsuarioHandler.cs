using ProyectofinalCoradini.Models;
using System.Data;
using System.Data.SqlClient;

namespace ProyectofinalCoradini.ADO.NET
{
    public class UsuarioHandler
    {
        private SqlConnection Conexion;
        private string StringConexion = "Server=sql.bsite.net\\MSSQL2016;" +
            "Database=ProyectofinalCoradini;" +
            "User Id=ninasqrl_ninasqrl;" +
            "Password=Cleo__24;";

        public UsuarioHandler() 
        {
            try
            {
                Conexion = new SqlConnection(StringConexion);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Usuario> GetUsuario() 
        {
            List<Usuario> listaUsuarios = new List<Usuario>();
            if (StringConexion == null)
            {
                throw new Exception("Conexión no realizada");
            }
            try
            {
                using (SqlCommand command = new SqlCommand ("SELECT * FROM Usuario", Conexion))
                {
                    Conexion.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Usuario usuario = new Usuario();
                                usuario.Id = Convert.ToInt32(reader["Id"].ToString());
                                usuario.Nombre = reader["Nombre"].ToString();
                                usuario.Apellido = reader["Apellido"].ToString();
                                usuario.NombreUsuario = reader["NombreUsuario"].ToString();
                                usuario.Contrasenia = reader["Contraseña"].ToString();
                                usuario.Mail = reader["Mail"].ToString();
                                listaUsuarios.Add(usuario);
                            }
                        }
                    }
                }
                Conexion.Close();
            }
            catch (Exception)
            {

                throw;
            }
            return listaUsuarios;
        }
    }
}
