using Microsoft.AspNetCore.Mvc;
using ProyectofinalCoradini.ADO.NET;
using ProyectofinalCoradini.Models;
using System.Data.SqlClient;

namespace ProyectofinalCoradini.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private ProductoHandler handler = new ProductoHandler();

        [HttpGet]
        public ActionResult<List<Producto>> Get()
        {
            try
            {
                List<Producto> lista = handler.GetProductos();
                return Ok(lista);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
    }
}