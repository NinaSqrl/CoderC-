using ProyectofinalCoradini.Models;
using System.Data;
using System.Data.SqlClient;

namespace ProyectofinalCoradini.ADO.NET
{
    public class ProductoHandler
    {
        private SqlConnection Conexion;
        private string StringConexion = "Server=sql.bsite.net\\MSSQL2016;" +
            "Database=ProyectofinalCoradini;" +
            "User Id=ninasqrl_ninasqrl;" +
            "Password=Cleo__24;";

        public ProductoHandler() 
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

        public List<Producto> GetProductos() 
        {
            List<Producto> listaProductos = new List<Producto>();
            if ( Conexion == null)
            {
                throw new Exception("Conexión no realizada");
            }
            try
            {
                using (SqlCommand command = new SqlCommand ("SELECT * FROM Producto", Conexion))
                {
                    Conexion.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Producto producto = new Producto();
                                producto.Id = Convert.ToInt32(reader["Id"].ToString());
                                producto.Descripciones = reader["Descripciones"].ToString();
                                producto.Costo = Convert.ToDecimal(reader["Costo"].ToString());
                                producto.PrecioVenta = Convert.ToDecimal(reader["PrecioVenta"].ToString());
                                producto.Stock = Convert.ToInt32(reader["Stock"].ToString());
                                producto.IdUsuario = Convert.ToInt32(reader["IdUsuario"].ToString());
                                listaProductos.Add(producto);
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
            return listaProductos;
        }
    }
}
