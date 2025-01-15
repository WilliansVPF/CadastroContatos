using Microsoft.AspNetCore.Mvc;
using CadastroContatos.Models;

namespace CadastroContatos.Controllers
{
    public class ContatoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
    }
}