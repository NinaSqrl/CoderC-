using Microsoft.AspNetCore.Mvc;
using ProyectofinalCoradini.ADO.NET;
using ProyectofinalCoradini.Models;
using System.Data.SqlClient;

namespace ProyectofinalCoradini.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class ProductoVendidoController : ControllerBase
    {
        private ProductoVendidoHandler handler = new ProductoVendidoHandler();

        [HttpGet]
        public ActionResult<List<ProductoVendido>> Get()
        {
            try
            {
                List<ProductoVendido> lista = handler.GetProductosVendidos();
                return Ok(lista);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
    }
}