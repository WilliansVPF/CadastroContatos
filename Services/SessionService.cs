using System.Text.Json;
using CadastroContatos.Interfaces;
using CadastroContatos.Models;
//using Microsoft.AspNetCore.Http;

namespace CadastroContatos.Services
{
    public class SessionService : ISessao
    {
        private readonly IHttpContextAccessor _httpContext;

        public SessionService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        
        public UsuarioModel? BuscarSessao()
        {
            string sessaoUsurio = _httpContext.HttpContext.Session.GetString("SessaoUsuario");

            if(string.IsNullOrEmpty(sessaoUsurio)) return null;

            return JsonSerializer.Deserialize<UsuarioModel>(sessaoUsurio);

        }

        public void CriarSessao(UsuarioModel usuario)
        {
            string valor = JsonSerializer.Serialize(usuario);
            _httpContext.HttpContext.Session.SetString("SessaoUsuario", valor);
        }

        public void RemoverSessao()
        {
            _httpContext.HttpContext.Session.Remove("SessaoUsuario");
        }
    }
}