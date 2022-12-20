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
                List<Usuario> lista = handler.GetlistaUsuarios();
                return Ok(lista);
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }
        }
        // 
        private readonly ILogger<ProductoVendido> _logger;

        public UsuarioController(ILogger<ProductoVendido> logger)
        {
            _logger = logger;
        }
        /*
           [HttpPost("{nombreUsuario}/{contraseña}")]
           public Usuario GetUsuarioByContraseña(string nombreUsuario, string contraseña)
           {
               var usuario = UsuarioHandler.GetUsuariodesdePassword(nombreUsuario, contraseña);

               return usuario == null ? new Usuario() : usuario;
           }
        */
        [HttpGet("{UserName}")]
        public ActionResult<Usuario> Get(string UserName)
        {



            try
            {
                Usuario usuario = handler.GetUsuariodesdeUserName(UserName);
                if (usuario != null)
                {
                    return Ok(usuario);
                }
                else
                {
                    return NotFound("El usuario " + UserName + " NO fue encontrado");
                }
            }
            catch (Exception ex)
            {

                return Problem(ex.Message);
            }

        }
        /*
                [HttpPost]
                public void PostUsuario(Usuario usuario)
                {
                    UsuarioHandler.InsertUsuario(usuario);
                }
        */



        // Modificar usuario
        [HttpPut]
        public Usuario PutUsuario(int id, Usuario usuarioModificado)
        {
            return handler.ActualizarUsuario(id, usuarioModificado);
        }
    }
    //
}
//}