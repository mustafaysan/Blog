using Blog.API.Infrastructure.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blog.API.Infrastructure.Extensions
{
    public static class ConfigureExtensions
    {
        public static IApplicationBuilder UseCustomSwaggerUI(this IApplicationBuilder application)
        {
            application.UseSwaggerUI(options =>
            {
                // Set the Swagger UI browser document title.
                options.DocumentTitle = typeof(Startup).Assembly.GetCustomAttribute<AssemblyProductAttribute>().Product;
                // Set the Swagger UI to render at '/'.
                options.RoutePrefix = string.Empty;
                // Show the request duration in Swagger UI.
                options.DisplayRequestDuration();



                var provider = application.ApplicationServices.GetService<IApiVersionDescriptionProvider>();
                foreach (var apiVersionDescription in provider.ApiVersionDescriptions.OrderByDescending(x => x.ApiVersion))
                {
                    if (apiVersionDescription.IsDeprecated)
                        continue;

                    options.SwaggerEndpoint($"./swagger/{apiVersionDescription.GroupName}/swagger.json", $"Version {apiVersionDescription.ApiVersion}");
                }
            });

            return application;
        }
            
    }
}
