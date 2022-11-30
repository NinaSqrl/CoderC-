using Microsoft.AspNetCore.Mvc;
using ProyectofinalCoradini.ADO.NET;
using ProyectofinalCoradini.Models;
using System.Data.SqlClient;

namespace ProyectofinalCoradini.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private UsuarioHandler handler = new UsuarioHandler();

        [HttpGet]
        public ActionResult<List<Usuario>> Get()
        {
            try
            {
                List<Usuario> lista = handler.GetUsuario();
                return Ok(lista);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
    }
}