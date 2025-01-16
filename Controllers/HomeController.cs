using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CadastroContatos.Models;
using CadastroContatos.ViewModels;
using CadastroContatos.Services;
using CadastroContatos.Interfaces;

namespace CadastroContatos.Controllers;

public class HomeController : Controller
{
    private readonly IUsuario _usuarioDb;
    private readonly ISessao _sessao;

    public HomeController(IUsuario usuarioDb, ISessao sessao)
    {
        _usuarioDb = usuarioDb;
        _sessao = sessao;
    }

    public IActionResult Index()
    {
        if(_sessao.BuscarSessao() != null) return RedirectToAction("Index", "Contato");
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Login()
    {
        if(_sessao.BuscarSessao() != null) return RedirectToAction("Index", "Contato");
        return View();
    }

    [HttpPost]
    public IActionResult Login(ContaModel conta)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Email ou senha inválidos");
            return View();
        }

        UsuarioModel usuario = new();
        usuario = _usuarioDb.Login(conta.Email, conta.Senha);

        if (usuario == null)
        {
            ModelState.AddModelError(string.Empty, "Email ou senha inválidos");
            return View();
        }
        _sessao.CriarSessao(usuario);
        return RedirectToAction("Index", "Contato");
    }

    public IActionResult Registrar()
    {
        if(_sessao.BuscarSessao() != null) return RedirectToAction("Index", "Contato");
        return View();
    }

    [HttpPost]
    public IActionResult Registrar(RegistroViewModel registro)
    {
        if (registro.Senha != registro.ConfirmacaoSenha)
        {
            ModelState.AddModelError(string.Empty,"As senhas não conferem");
            return View();
        }

        if (ModelState.IsValid)
        {
            UsuarioModel usuario = new UsuarioModel
            {
                Nome = registro.Nome
            };

            ContaModel conta = new ContaModel
            {
                Email = registro.Email,
                Senha = registro.Senha, 
            };

            conta.Salt = HashService.Salt();
            conta.Senha = HashService.Hash(conta.Senha, conta.Salt);

            int i = _usuarioDb.CadastraUsuario(usuario, conta);

            if (i == 0)
            {
                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Erro ao cadastrar usuário");
            }
        }
        return RedirectToAction("Login", "Home");;
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
