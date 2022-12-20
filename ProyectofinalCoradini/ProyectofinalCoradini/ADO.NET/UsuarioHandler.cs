using ProyectofinalCoradini.Models;
using System.Data;
using System.Data.SqlClient;

namespace ProyectofinalCoradini.ADO.NET
{
    public class UsuarioHandler
    {
        private SqlConnection Conexion;
        private string StringConexion =
            //"Server = sql.bsite.net\\MSSQL2016;" +
            //"Database=ajomuch92_coderhouse_csharp_40930;" +
            //"User Id=ajomuch92_coderhouse_csharp_40930;" +
            //"Password=ElQuequit0Sexy2022;";
           "Server=sql.bsite.net\\MSSQL2016;" +
           "Database=ninasqrl_ProyectofinalCoradini;" +
           "User Id=ninasqrl_ProyectofinalCoradini;" +
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


        // acá CargarUsuario()
        // Carga los datos del usuario desde la base de datos

        private Usuario CargarUsuarioDesdeReader(SqlDataReader reader)
        {
            Usuario usuario = new Usuario();
            usuario.Id = Convert.ToInt32(reader["Id"].ToString());
            usuario.Nombre = reader["Nombre"].ToString();
            usuario.Apellido = reader["Apellido"].ToString();
            usuario.NombreUsuario = reader["NombreUsuario"].ToString();
            usuario.Contrasenia = reader["Contrasenia"].ToString();
            usuario.Mail = reader["Mail"].ToString();
            return usuario;
        }

        // GetlistaUsuarios
        // Genera una lista con los usuarios registrados en la base de datos

        public List<Usuario> GetlistaUsuarios() 
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
                                Usuario usuario = CargarUsuarioDesdeReader(reader);
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

        // getUsuariodesdeId
        // Devuelve los datos de un usuario a partir de su ID en la base de datos

        public Usuario getUsuariodesdeId(int id)
        {
            if (Conexion == null)
            {
                throw new Exception("Conexión no realizada");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Usuario WHERE id = @id", Conexion))
                {
                    Conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            Usuario usuario = CargarUsuarioDesdeReader(reader);
                            return usuario;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Conexion.Close();
            }
        }

        // crear usuario
        // crea un usuario nuevo en la base de datos
        public Usuario CrearUsuario(Usuario usuario)
        {
            if (Conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Usuario(Nombre, Apellido, NombreUsuario, Contrasenia, Mail) VALUES(@nombre, @apellido, @nombreUsuario, @contraseña, @mail); SELECT @@Identity", Conexion))
                {
                    Conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("nombre", SqlDbType.VarChar) { Value = usuario.Nombre });
                    cmd.Parameters.Add(new SqlParameter("apellido", SqlDbType.VarChar) { Value = usuario.Apellido });
                    cmd.Parameters.Add(new SqlParameter("nombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario });
                    cmd.Parameters.Add(new SqlParameter("contraseña", SqlDbType.VarChar) { Value = usuario.Contrasenia });
                    cmd.Parameters.Add(new SqlParameter("mail", SqlDbType.VarChar) { Value = usuario.Mail });
                    usuario.Id = int.Parse(cmd.ExecuteScalar().ToString());
                    return usuario;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                Conexion.Close();
            }
        }



        // modificar usuario
        // devuelve usuario si se modificaron los datos de un usuario a partir de su ID

        public Usuario? ActualizarUsuario(int id, Usuario usuarioModificado)
        {
            if (Conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                Usuario usuarioOriginal = getUsuariodesdeId(id);
                if (usuarioModificado == null)
                {
                    return null;
                }
                List<string> camposAActualizar = new List<string>();

                if (usuarioOriginal.Nombre != usuarioModificado.Nombre && !string.IsNullOrEmpty(usuarioModificado.Nombre))
                {
                    camposAActualizar.Add("nombre = @nombre");
                    usuarioOriginal.Nombre = usuarioModificado.Nombre;
                }

                if (usuarioOriginal.Apellido != usuarioModificado.Apellido)
                {
                    camposAActualizar.Add("apellido = @apellido");
                    usuarioOriginal.Apellido = usuarioModificado.Apellido;
                }

                if (usuarioOriginal.NombreUsuario != usuarioModificado.NombreUsuario)
                {
                    camposAActualizar.Add("NombreUsuario = @nombreUsuario");
                    usuarioOriginal.NombreUsuario = usuarioModificado.NombreUsuario;
                }

                if (usuarioOriginal.Contrasenia != usuarioModificado.Contrasenia)
                {
                    camposAActualizar.Add("contrasenia = @contrasenia");
                    usuarioOriginal.Contrasenia = usuarioModificado.Contrasenia;
                }

                if (usuarioOriginal.Mail != usuarioModificado.Mail)
                {
                    camposAActualizar.Add("mail = @mail");
                    usuarioOriginal.Mail = usuarioModificado.Mail;
                }

                if (camposAActualizar.Count == 0)
                {
                    throw new Exception("No new fields to update");
                }

                using (SqlCommand cmd = new SqlCommand($"UPDATE Usuario SET {String.Join(", ", camposAActualizar)} WHERE id = @id", Conexion))
                {
                    cmd.Parameters.Add(new SqlParameter("nombre", SqlDbType.VarChar) { Value = usuarioModificado.Nombre });
                    cmd.Parameters.Add(new SqlParameter("apellido", SqlDbType.VarChar) { Value = usuarioModificado.Apellido });
                    cmd.Parameters.Add(new SqlParameter("nombreUsuario", SqlDbType.VarChar) { Value = usuarioModificado.NombreUsuario });
                    cmd.Parameters.Add(new SqlParameter("contrasenia", SqlDbType.VarChar) { Value = usuarioModificado.Contrasenia });
                    cmd.Parameters.Add(new SqlParameter("mail", SqlDbType.VarChar) { Value = usuarioModificado.Mail });
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = usuarioModificado.Id });
                    Conexion.Open();
                    cmd.ExecuteNonQuery();
                    return usuarioOriginal;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                Conexion.Close();
            }
        }
        //
        /*
        public Usuario GetUsuariodesdePassword(string Password, string Mail)
        {
            if (Conexion == null)
            {
                throw new Exception("Conexión no realizada");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Usuario WHERE Contrasenia = @Password and Mail= @Mail", Conexion))
                {
                    Conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("ID", SqlDbType.BigInt) { Value = UserName });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            Usuario usuario = CargarUsuarioDesdeReader(reader);
                            return usuario;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Conexion.Close();
            }
        }

        */

        //
        public Usuario GetUsuariodesdeUserName(string UserName)
        {
            if (Conexion == null)
            {
                throw new Exception("Conexión no realizada");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Usuario WHERE NombreUsuario = @UserName", Conexion))
                {
                    Conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("UserName", SqlDbType.VarChar) { Value = UserName });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            Usuario usuario = CargarUsuarioDesdeReader(reader);
                            return usuario;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Conexion.Close();
            }
        }

        //



    }


}
