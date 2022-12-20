namespace ProyectofinalCoradini.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public string Comentarios { get; set; }
        public int IdUsuario { get; set; }
        public List<ProductoVendido>? ProductosVendidos { get; set; }

        public Venta() 
        {
            Id = 0;
            Comentarios = "";
            IdUsuario = 0;
            ProductosVendidos = new List<ProductoVendido>();
        }
        public Venta (int id, string comentarios, int idUsuario, List<ProductoVendido> productosVendidos)
        {
            Id = id;
            Comentarios = comentarios;
            IdUsuario = idUsuario;
            ProductosVendidos = productosVendidos;

        }
    }
}
