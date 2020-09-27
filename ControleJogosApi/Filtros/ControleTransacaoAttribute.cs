using AcessoADados;
using Infra;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Modelos;
using System;
using System.Threading.Tasks;

namespace ControleJogosApi.Filtros
{
    [AttributeUsage(AttributeTargets.Method , AllowMultiple = true)]
    public class ControleTransacaoAttribute : ActionFilterAttribute
    {
        private IUnidadeTrabalho unidadeTrabalho;

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {            
            unidadeTrabalho = context.HttpContext.RequestServices.GetService<IUnidadeTrabalho>();
            unidadeTrabalho.BeginTransaction();

            var filterContext = await next.Invoke();

            var (gerouExcessao, excessao) = await FinalizarTransacao(filterContext);

            if (gerouExcessao)
            {
                var resposta = new RespostaErroModel(excessao);

                filterContext.Result = new JsonResult(new { resposta });
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }        

        private async Task<(bool,string)> FinalizarTransacao(ActionExecutedContext filterContext)
        {
            string retorno = string.Empty;

            try
            {
                if (filterContext.Exception == null)
                {
                    await unidadeTrabalho.Commit();
                }
                else
                {
                    await unidadeTrabalho.Rollback();
                    retorno = filterContext.Exception.TratarExcessao();
                }
            }
            catch (Exception e)
            {
                filterContext.Exception = e;
                retorno = e.TratarExcessao();                
            }

            return (retorno.Length > 0, retorno);
        }
    }
}
