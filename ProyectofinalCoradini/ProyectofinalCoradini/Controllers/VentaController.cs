using Microsoft.AspNetCore.Mvc;
using ProyectofinalCoradini.ADO.NET;
using ProyectofinalCoradini.Models;
using System.Data.SqlClient;

namespace ProyectofinalCoradini.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class VentaController : ControllerBase
    {
        private VentaHandler handler = new VentaHandler();

        [HttpGet]
        public ActionResult<List<Venta>> Get()
        {
            try
            {
                List<Venta> lista = handler.GetVenta();
                return Ok(lista);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
    }
}