using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs;
using WebAPI.Exceptions;
using WebAPI.Identity;
using WebAPI.Installers;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.InstallServicesInAssembly(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features.Get<IExceptionHandlerPathFeature>().Error;
                if(exception is NotFoundException)
                {
                    context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                    await context.Response.WriteAsJsonAsync(new ErrorDto { Code = StatusCodes.Status404NotFound, Error = exception.Message });
                }
            }));

            app.UseStatusCodePages();
            app.UseCors();
            app.UseAuthentication();
            MyIdentityDataInitializer.SeedData(userManager, roleManager);
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
