using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text;
using WebPessoa.Repository;
using WebPessoa.Repository.Models;

namespace WebPessoa.Application.Usuario
{
    public class UsuarioService
    {
        private readonly PessoaContext _context;
        public UsuarioService(PessoaContext context)
        { 
            _context = context;
        
        }
        public bool InserirUsuario(UsuarioRequest request)
        {
            try
            {
                var usuario = new TabUsuario()
                {
                    nome = request.Nome,
                    usuario = request.Usuario,
                    senha = request.Senha
                };

                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
           
        }
        public List<TabUsuario> ObterUsuarios()
        {
            var usuarios = _context.Usuarios.ToList();
            return usuarios;
        }
        public TabUsuario ObterUsuario(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(x => x.id == id);
            return usuario;
        }
        public bool AtualizarUsuario(int id, UsuarioRequest request)
        {
            try
            {
                var usuarioDb = _context.Usuarios.FirstOrDefault(x => x.id ==id);
                if(usuarioDb == null)
                    return false;

                usuarioDb.nome = request.Nome;
                usuarioDb.senha = request.Senha;
                usuarioDb.usuario = request.Usuario;

                _context.Usuarios.Update(usuarioDb);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex){ 
            return false;}
        }
        public bool RemoverUsuario(int id)
        {
            try
            {
                var usuarioDb = _context.Usuarios.FirstOrDefault(x => x.id == id);
                if (usuarioDb == null)
                    return false;
                _context.Usuarios.Remove(usuarioDb);
                _context.SaveChanges();

                return true;
            }
            catch(Exception ex)
            {
                return false;
            } 
      }

    }
}
