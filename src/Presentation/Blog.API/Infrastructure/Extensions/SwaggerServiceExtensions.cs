using Blog.API.Infrastructure.OperationFilters;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Blog.API.Infrastructure.Extensions
{
    /// <summary>
    /// Swagger service extensions
    /// </summary>
    public static class SwaggerServiceExtensions
    {
        /// <summary>
        /// Adds Swagger services and configures the Swagger services.
        /// </summary>
        public static IServiceCollection AddSwaggerOptionService(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var assembly = typeof(Startup).Assembly;
                var assemblyProduct = assembly.GetCustomAttribute<AssemblyProductAttribute>().Product;
                var assemblyDescription = assembly.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description;

                options.DescribeAllEnumsAsStrings();
                options.DescribeAllParametersInCamelCase();
                options.DescribeStringEnumsInCamelCase();

                // Add the XML comment file for this assembly, so its contents can be displayed.

                options.OperationFilter<ApiVersionOperationFilter>();
                options.OperationFilter<TokenOperationFilter>();
                

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = System.IO.Path.Combine(System.AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var apiVersionDescription in provider.ApiVersionDescriptions)
                {
                    if (apiVersionDescription.IsDeprecated)
                        continue;

                    var info = new Info()
                    {
                        Title = assemblyProduct,
                        Description = apiVersionDescription.IsDeprecated
                        ? $"{assemblyDescription} This API version has been deprecated."
                        : assemblyDescription,
                        Version = apiVersionDescription.ApiVersion.ToString(),
                    };
                    options.SwaggerDoc(apiVersionDescription.GroupName, info);
                }
            });

            return services;
        }
           

    }
}
