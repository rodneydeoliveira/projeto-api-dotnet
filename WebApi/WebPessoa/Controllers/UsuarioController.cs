using Microsoft.AspNetCore.Mvc;
using WebPessoa.Application.Usuario;
using WebPessoa.Repository;

namespace WebPessoa.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly PessoaContext _context;
        public UsuarioController(PessoaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult InserirUsuario([FromBody] UsuarioRequest request)
        {
            var usuarioService = new UsuarioService(_context);
            var sucesso = usuarioService.InserirUsuario(request);

            if (sucesso == true)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]

        public IActionResult ObterUsuarios()
        {
            var usuarioService = new UsuarioService(_context);
            var usuarios = usuarioService.ObterUsuarios();
            return Ok(usuarios);
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult ObterUsuario([FromRoute] int id)
        {
            var usuarioService = new UsuarioService(_context);
            var usuario = usuarioService.ObterUsuario(id);


            if (usuario == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(usuario);
            }
        }
        [HttpPut]
        [Route("{id}")]

        public IActionResult AtualizarUsuario([FromRoute] int id, [FromBody] UsuarioRequest request)
        {
            var usuarioService = new UsuarioService(_context);
            var sucesso = usuarioService.AtualizarUsuario(id, request);
            if (sucesso == true)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult RemoverUsuario([FromRoute] int id)

        {
            var usuarioService = new UsuarioService(_context);
            var sucesso = usuarioService.RemoverUsuario(id);
            if (sucesso == true)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }




    }  
}
