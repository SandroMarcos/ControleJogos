using AcessoADados;
using AutoMapper;
using System;

namespace Modelos.Mapeamento
{
    public class MapeamentoClasses : Profile
    {
        public MapeamentoClasses()
        {
            CreateMap<Usuario, UsuarioReduzidoModel>().ReverseMap();
            CreateMap<Usuario, RespostaAutenticacaoSucessoModel>().ReverseMap();
            CreateMap<UsuarioReduzidoModel, RespostaAutenticacaoSucessoModel>().ReverseMap();
            CreateMap<Usuario, UsuarioModel>()
                .ForMember(viewModel => viewModel.Senha, opcoes => opcoes.MapFrom(poco => poco.Senha))
                .ReverseMap();            

            CreateMap<Jogo, JogoReduzidoModel>().ReverseMap();
            CreateMap<JogoModel, JogoReduzidoModel>().ReverseMap();
            CreateMap<Jogo, JogoModel>()
                .ForMember(viewModel => viewModel.IdUsuario, opcoes => opcoes.MapFrom(poco => poco.Usuario.IdUsuario))
                .ReverseMap();

            CreateMap<Jogo, JogosDisponiveisModel>()
                .ForMember(viewModel => viewModel.IdUsuario, opcoes => opcoes.MapFrom(poco => poco.Usuario.IdUsuario))
                .ForMember(viewModel => viewModel.NomeUsuario, opcoes => opcoes.MapFrom(poco => poco.Usuario.Nome))
                .ReverseMap();

            CreateMap<EmprestimoJogo, EmprestimoReduzidoModel>()
                .ForMember(viewModel => viewModel.IdJogo, opcoes => opcoes.MapFrom(poco => poco.Jogo.IdJogo))
                .ForMember(viewModel => viewModel.IdUsuario, opcoes => opcoes.MapFrom(poco => poco.Usuario.IdUsuario))
                .ReverseMap();

            CreateMap<EmprestimoJogo, HistoricoEmprestimoModel>()
                .ForMember(viewModel => viewModel.IdJogo, opcoes => opcoes.MapFrom(poco => poco.Jogo.IdJogo))
                .ForMember(viewModel => viewModel.IdUsuario, opcoes => opcoes.MapFrom(poco => poco.Usuario.IdUsuario))
                .ForMember(viewModel => viewModel.NomeJogo, opcoes => opcoes.MapFrom(poco => poco.Jogo.Nome))
                .ForMember(viewModel => viewModel.NomeUsuario, opcoes => opcoes.MapFrom(poco => poco.Usuario.Nome))
                .ReverseMap();
            CreateMap<EmprestimoJogo, EmprestimoModel>().ReverseMap();

            //HistoricoEmprestimoModel
        }
    }
}
