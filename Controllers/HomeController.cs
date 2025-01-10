using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CadastroContatos.Models;
using CadastroContatos.DataBase;
using CadastroContatos.ViewModels;
using CadastroContatos.Services;

namespace CadastroContatos.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Registrar()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Registrar(RegistroViewModel registro)
    {
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

            int i = UsuarioDb.CadastraUsuario(usuario, conta);

            if (i == 0)
            {
                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Erro ao cadastrar usuário");
            }
        }
        return View();
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
