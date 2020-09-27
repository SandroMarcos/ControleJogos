using Infra;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;

namespace ControleJogosApi.Filtros
{
    public class ValidadorModeloFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ModelState.IsValid == false)
            {
                var erros = context.ModelState
                    .ToDictionary(x => x.Key, x => x.Value.Errors.Select(e => e.ErrorMessage).ToArray())
                    .Where(v => v.Value.Count() > 0);

                object retorno = new
                {
                    errors = new
                    {
                        message = erros
                    }
                };

                context.Result = new BadRequestObjectResult(retorno);
            }
            else
            {
                var resultContext = await next();

                if (resultContext.Exception != null)
                {
                    resultContext.Result = new JsonResult(new
                    {
                        errors = new
                        {
                            message = resultContext.Exception.TratarExcessao()
                        }
                    });

                    resultContext.ExceptionHandled = true;
                    resultContext.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                }
            }
        }
    }
}
