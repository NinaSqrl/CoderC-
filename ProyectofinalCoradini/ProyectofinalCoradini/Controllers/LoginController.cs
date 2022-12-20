using Microsoft.AspNetCore.Mvc;
using ProyectofinalCoradini.ADO.NET;
using ProyectofinalCoradini.Models;
// using ProyectofinalCoradini.Repositories;
using System.Data.SqlClient;

namespace ProyectofinalCoradini.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class LoginController : ControllerBase
    {
        private LoginHandler handler = new LoginHandler();

        [HttpPost]
        public ActionResult<Usuario> Login(Usuario usuario)
        {
            try
            {
                bool usuarioExiste = handler.LoginUsuario(usuario);
                return usuarioExiste ? Ok() : NotFound("Usuario o contraseña incorrectos");
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
    }
}