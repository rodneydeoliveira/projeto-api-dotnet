using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Cache;
using System.Security.Claims;
using System.Text;
using WebPessoa.Repository;
using WebPessoa.Repository.Models;

namespace WebPessoa.Application.Autenticacao
{
  
    public class AutenticacaoService
    {
        private readonly PessoaContext _context;
        public AutenticacaoService(PessoaContext context)
        {
            _context = context;
        }
        public string Autenticar(AutenticacaoRequest request)
        {
            var usuario = _context.Usuarios.FirstOrDefault(x => x.usuario == request.UserName && x.senha == request.Password);


            if (usuario != null)
            {
                var tokenString = GerarTokenJwt(usuario);
                return tokenString;
            }
            else
            {
                return null;
            }
        }
        private string GerarTokenJwt(TabUsuario usuario)
        {
            var issuer = "var";
            var audience = "var";
            var key = "72297769-8b79-481a-bea8-b2cb732ae5eb";

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("usuarioId", usuario.id.ToString())
            };

            var token = new JwtSecurityToken(issuer: issuer,claims: claims, audience: audience, expires: DateTime.Now.AddMinutes(60), signingCredentials: credentials);
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);

            return stringToken;
        }
    }
}
