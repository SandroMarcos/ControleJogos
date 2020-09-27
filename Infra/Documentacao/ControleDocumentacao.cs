using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Infra
{
    public static class ControleDocumentacao
    {
        public static void AddDocumentacao(this IServiceCollection services, IConfiguration configuration, string xmlAplicacao)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", configuration.GetSection("OpenApiInfo").Get<OpenApiInfo>());

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                            BearerFormat = "JWT",
                            Type = SecuritySchemeType.ApiKey,
                        },
                        new List<string>() { "Bearer" }
                    },
                });

                var xmlPath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, $"{xmlAplicacao}.xml");
                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}


