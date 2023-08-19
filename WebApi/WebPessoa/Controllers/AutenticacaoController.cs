using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Data.SqlTypes;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebPessoa.Application.Autenticacao;
using WebPessoa.Repository;

namespace WebPessoa.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutenticacaoController : ControllerBase
    {
        private readonly PessoaContext _context;
        public AutenticacaoController(PessoaContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Login([FromBody] AutenticacaoRequest request)
        {
            var autenticacoService = new AutenticacaoService(_context);
            var autenticacaoResponse = autenticacoService.Autenticar(request);

            if (autenticacaoResponse == null) { 
                return Unauthorized();
            }

            else
            {
                return Ok(autenticacaoResponse);
            }
        }
        
    }
}
