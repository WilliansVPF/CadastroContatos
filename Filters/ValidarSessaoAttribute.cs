using CadastroContatos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CadastroContatos.Filters
{
    public class ValidarSessaoAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var sessao = context.HttpContext.RequestServices.GetService(typeof(ISessao)) as ISessao;
            if(sessao.BuscarSessao() == null) context.Result = new RedirectToActionResult("Index", "Home", null);
            base.OnActionExecuted(context);
        }
    }
}