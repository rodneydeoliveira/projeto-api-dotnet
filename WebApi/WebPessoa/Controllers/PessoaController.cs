using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebPessoa.Application.Pessoa;
using WebPessoa.Application.Usuario;
using WebPessoa.Repository;

namespace WebPessoa.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly PessoaContext _context;
        public PessoaController(PessoaContext context)
        {
            _context = context;

        }
        /// <summary>
        ///Rota responsável por processar dados de uma pessoa.
        /// </summary>
        /// <returns>Retorna os dados processados da pessoa</returns>
        /// <response code="200">Retorna os dados processados com sucesso</response>
        /// /// <response code="400"> Retorna Erro de Validação</response>
     
        [HttpPost]
        [Authorize]
        public PessoaResponse ProcessarInformacoesPessoa([FromBody] PessoaRequest request)
        {
            var identidade = (ClaimsIdentity)HttpContext.User.Identity;
            var usuarioId = identidade.FindFirst("usuarioId").Value;

            var pessoaService = new PessoaService(_context);
            var pessoaResponse = pessoaService.ProcesarInformacoesPessoa(request, Convert.ToInt32(usuarioId));

            return pessoaResponse;
           
        }
        [HttpGet]
        [Authorize]
        public List<PessoaHistoricoResponse> ObterHistoricoPessoas()
        {
            var pessoaService = new PessoaService(_context);
            var pessoas = pessoaService.ObterHistoricoPessoas();
            return pessoas;
        }
        [HttpGet]
        [Authorize]
        [Route("{id}")]
        public PessoaHistoricoResponse ObterHistoricoPessoa([FromRoute]int id)
        {
            var pessoaService = new PessoaService(_context);
            var pessoa = pessoaService.ObterHistoricoPessoa(id);
            return pessoa;
        }

        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        public IActionResult ExcluirInformacoesPessoa([FromRoute] int id)
        {
            var pessoaService = new PessoaService(_context);
            var sucesso = pessoaService.ExcluirInformacoesPessoa(id);
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
