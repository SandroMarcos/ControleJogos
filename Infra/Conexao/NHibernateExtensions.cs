using AcessoADados;
using FluentNHibernate.Cfg;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Tool.hbm2ddl;
using System.IO;
using System.Reflection;

namespace Infra
{
    public static class NHibernateExtensions
    {
        public static IServiceCollection AddNHibernate(this IServiceCollection services, string connectionString)
        {
            var configuration = new Configuration();
            configuration.DataBaseIntegration(c =>
            {
                c.Dialect<SQLiteDialect>();
                c.ConnectionString = connectionString;
                c.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                c.SchemaAction = SchemaAutoAction.Update;
                //c.LogFormattedSql = true;
                //c.LogSqlInConsole = true;
            });         

            FluentConfiguration fconfiguration = Fluently.Configure(configuration)
                .ExposeConfiguration(c => c.Properties.Add(Environment.SqlTypesKeepDateTime, bool.TrueString))                
                .Mappings(x => x.FluentMappings.AddFromAssembly(Assembly.Load("AcessoADados")));

            var sessionFactory = fconfiguration.BuildSessionFactory();

            services.AddSingleton(sessionFactory);
            services.AddScoped(factory => sessionFactory.OpenSession());
            services.AddScoped<IUnidadeTrabalho, UnidadeTrabalho>();
            services.AddScoped<IRepositorio, Repositorio>();

            services.Scan(scan => scan
            .FromAssemblies(Assembly.Load("Repositorios"))
                .AddClasses(c => c.Where(w => w.Name.StartsWith("Repositorio")))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            .FromAssemblies(Assembly.Load("RegrasNegocio"))
                .AddClasses(c => c.Where(w => w.Name.StartsWith("Consulta") || w.Name.StartsWith("Comando")))
                .AsImplementedInterfaces()
                .WithScopedLifetime());            

            return services;
        }

        /// <summary>
        /// Usado somente para desenvolvedores obterem <see cref="ddl"/>.
        /// </summary>
        /// <param name="configuration">Configuração da <see cref="session do NHibernate"/>.</param>
        private static void ExportarEsquema(Configuration configuration)
        {
            SchemaExport exportaSchema = new SchemaExport(configuration);

            using (TextWriter arquivoSaida = new StreamWriter("g:\\schema.sql"))
            {
                exportaSchema.Execute(true, false, false, null, arquivoSaida);
            }
        }
    }
}
