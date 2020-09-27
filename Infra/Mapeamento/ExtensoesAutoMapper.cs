using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infra
{
    public static class ExtensoesAutoMapper
    {
        public static IServiceCollection RegistrarAutoMapper(this IServiceCollection services, params Assembly[] assemblies)
        {
            var expressaoConfiguracao = new AutoMapper.Configuration.MapperConfigurationExpression();
            expressaoConfiguracao.AddMaps(assemblies);
            services.AddSingleton<IMapperConfigurationExpression>(expressaoConfiguracao);

            var configuracao = new MapperConfiguration(expressaoConfiguracao);
            services.AddSingleton<MapperConfiguration>(configuracao);

            var mapper = configuracao.CreateMapper();
            services.AddSingleton<IMapper>(mapper);

            //MapeamentoExtensions.Inicializar(expressaoConfiguracao);

            return services;
        }
    }
}
