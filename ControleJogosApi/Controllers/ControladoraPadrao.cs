using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControleJogosApi.Controllers
{
    [Authorize]
    public abstract class ControladoraPadrao : ControllerBase
    {
        protected readonly IMapper mapper;

        #region Construtores

        /// <summary>
        /// Construtor da classe
        /// </summary>
        public ControladoraPadrao(IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.mapper = mapper;
            IdUsuario = int.Parse(httpContextAccessor.HttpContext.User.FindFirst("IdUsuario")?.Value ?? "0");
        }

        #endregion

        #region Propriedades protegidas

        /// <summary>
        /// Propriedade onde irá armazenar o Id do usuário logado
        /// </summary>
        protected int IdUsuario { get; set; }

        #endregion
    }
}