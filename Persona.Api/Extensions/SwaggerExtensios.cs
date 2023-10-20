using Microsoft.OpenApi.Models;

namespace Persona.Api.Extensions
{
    public static class SwaggerExtensios
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services, string? name = null)
        {
            _ = services.AddRouting(options => options.LowercaseUrls = true);
            _ = services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = $"{name} API for Apps",
                    Version = "v1"
                });
                // To Enable authorization using Swagger (JWT)    
                swagger.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme()
                {
                    Name = "ApiKey",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "ApiKey",
                    BearerFormat = "Token",
                    In = ParameterLocation.Header,
                    Description = "Escriba por favor la clave de acceso del cliente.",
                });

                // To Enable authorization using Swagger (JWT)    
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Escriba 'Bearer' [espacio] y luego agrege un token válido en la entrada de texto siguiente.\r\n\r\nEjemplo: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });

                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "ApiKey"
                            }
                        },
                        new string[] {}
                    }
                });

            });
            return services;
        }

    }
}
