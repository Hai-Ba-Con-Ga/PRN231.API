using BusinessObject.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.Generation.Processors.Security;
using System.Reflection;

namespace WebAPI.Configurations.OpenApi
{
    public static class SwaggerConfigurations
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddOpenApiDocument((document, service) =>
            {
                document.PostProcess = doc =>
                {
                    doc.Info.Title = AppConfig.SwaggerConfig.Title;
                    doc.Info.Version = AppConfig.SwaggerConfig.Version;
                    doc.Info.Description = AppConfig.SwaggerConfig.Description;
                    doc.Info.Contact = new()
                    {
                        Name = AppConfig.SwaggerConfig.ContactName,
                        Email = AppConfig.SwaggerConfig.ContactEmail,
                        Url = AppConfig.SwaggerConfig.ContactUrl
                    };
                    doc.Info.License = new()
                    {
                        Name = AppConfig.SwaggerConfig.LicenseName,
                        Url = AppConfig.SwaggerConfig.LicenseUrl
                    };
                };
                document.AddSecurity(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Input your Bearer token to access this API",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Type = OpenApiSecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",
                });
                document.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor());
                document.OperationProcessors.Add(new SwaggerGlobalAuthProcessor());

                document.OperationProcessors.Add(new SwaggerHeaderAttributeProcessor());
            });

            services.AddSwaggerGen(c =>
            {
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        //use swagger
        public static IApplicationBuilder UseSwaggerAPI(this IApplicationBuilder app)
        {
            app.UseOpenApi();
            app.UseSwaggerUi(options =>
            {
                options.DefaultModelExpandDepth = -1;
                options.DocExpansion = "none";
                options.TagsSorter = "alpha";
            });
            return app;
        }

    }
}