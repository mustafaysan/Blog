using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Blog.API.Infrastructure.Extensions;
using Serilog;

namespace Blog.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDependencyInjection(Configuration);
            services.AddSwaggerOptionService();
            services.AddCustomApiVersioning();
            services.AddJwtBearerAuthentication(Configuration);
            services.AddAuthorization();
            services
                .AddMvcCore()
                .AddApiExplorer()
                .AddDataAnnotations();

            services.AddVersionedApiExplorer(x => x.GroupNameFormat = "'v'VVV");
            services.AddLogging();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSerilogRequestLogging();

            app
                .UseCors("*")
                //.UseResponseCaching()
                .UseAuthentication()
                .UseMvc()
                .UseSwagger()
                .UseCustomSwaggerUI();
            
        }
    }
}
