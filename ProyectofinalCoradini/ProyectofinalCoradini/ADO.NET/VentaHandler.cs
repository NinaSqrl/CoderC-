using Microsoft.Extensions.Logging;
using ProyectofinalCoradini.Models;
using System.Data;
using System.Data.SqlClient;

namespace ProyectofinalCoradini.ADO.NET
{
    public class VentaHandler
    {
        private SqlConnection conexion;
        private string CadenaConexion =
            //"Server = sql.bsite.net\\MSSQL2016;" +
            //"Database=ajomuch92_coderhouse_csharp_40930;" +
            //"User Id=ajomuch92_coderhouse_csharp_40930;" +
            //"Password=ElQuequit0Sexy2022;";
           "Server=sql.bsite.net\\MSSQL2016;" +
           "Database=ninasqrl_ProyectofinalCoradini;" +
           "User Id=ninasqrl_ProyectofinalCoradini;" +
           "Password=Cleo__24;";

        public VentaHandler() 
        {
            try
            {
                conexion = new SqlConnection(CadenaConexion);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Venta> GetVenta() 
        {
            List<Venta> listaVentas = new List<Venta>();
            if (CadenaConexion == null)
            {
                throw new Exception("Conexión no realizada");
            }
            try
            {
                conexion.Open();
                using (SqlCommand command = new SqlCommand ("SELECT * FROM Venta", conexion))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Venta venta = new Venta();
                                venta.Id = Convert.ToInt32(reader["Id"].ToString());
                                venta.Comentarios = reader["Comentarios"].ToString();
                                venta.IdUsuario = Convert.ToInt32(reader["IdUsuario"].ToString());
                                listaVentas.Add(venta);
                            }
                        }
                    }
                }


                conexion.Close();
            }
            catch (Exception)
            {

                throw;
            }
            return listaVentas;
        }
       
        // Eliminar venta
        // Elimina una venta de la BD y restablece el stock

        public bool eliminarVenta(int id)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                int deleteventa = 0;

                conexion.Open();
                using (SqlCommand cmd = new SqlCommand("delete from productovendido where idventa = @Id", conexion))
                {

                    cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.Int) { Value = id });
                    deleteventa = cmd.ExecuteNonQuery();
                }
                using (SqlCommand cmd = new SqlCommand("DELETE FROM venta WHERE id = @id", conexion))
                {
                    
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.Int) { Value = id });
                    deleteventa = cmd.ExecuteNonQuery();
                }
                conexion.Close();
                return deleteventa > 0;
            }
            catch
            {
                throw;
            }
        }

         public bool crearVenta(Venta venta)
        {
            int insertventa = 0;
            using (SqlCommand cmd = new SqlCommand("insert into venta (Id, Comentarios, IdUsuario) values (@Id, @Comentarios, @IdUsuario)", conexion))
            {
                conexion.Open();
                cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.Int) { Value = venta.Id });
                cmd.Parameters.Add(new SqlParameter("Comentarios", SqlDbType.VarChar) { Value = venta.Comentarios });
                cmd.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.Int) { Value = venta.IdUsuario });
                insertventa = cmd.ExecuteNonQuery();
            }
            


            if (insertventa > 0)
            {
                foreach (ProductoVendido pv in venta.ProductosVendidos)
                {
                    using (SqlCommand cmd = new SqlCommand("insert into productoVendido (Id, Stock, IdProducto, IdVenta) values (@Id, @Stock, @IdProducto, @IdVenta)", conexion))
                    {
                        
                        cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.Int) { Value = pv.Id });
                        cmd.Parameters.Add(new SqlParameter("Stock", SqlDbType.VarChar) { Value = pv.Stock });
                        cmd.Parameters.Add(new SqlParameter("IdProducto", SqlDbType.Int) { Value = pv.IdProducto });
                        cmd.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.Int) { Value = pv.IdVenta });
                        insertventa = cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = new SqlCommand("update producto set stock = stock - @stock where id = @Id", conexion))
                    {
                        
                        cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.Int) { Value = pv.IdProducto });
                        cmd.Parameters.Add(new SqlParameter("Stock", SqlDbType.VarChar) { Value = pv.Stock });
                        insertventa = cmd.ExecuteNonQuery();
                    }
                   
                }


            
            }
            conexion.Close();
            return insertventa > 0;


        }
        
    }
}
