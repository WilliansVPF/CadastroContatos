using Microsoft.AspNetCore.Mvc;
using CadastroContatos.Models;
using CadastroContatos.Interfaces;
using CadastroContatos.Services;
using CadastroContatos.Filters;

namespace CadastroContatos.Controllers
{
    [ValidarSessao]
    public class ContatoController : Controller
    {
        private readonly ISessao _sessao;

        public ContatoController(ISessao sessao)
        {
            _sessao = sessao;
        }
        public IActionResult Index()
        {
            UsuarioModel usuario = _sessao.BuscarSessao();
            return View(usuario);
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessao();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult NovoContato()
        {
            UsuarioModel usuario = _sessao.BuscarSessao();
            return View(usuario);
        }
        
    }
}