using Core.DTOs;
using Core.Exceptions;
using Core.ServiceResponses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Storage.Identity;
using System.Net;
using WebAPI.Installers;
using WebAPI.Middleware;

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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<User> userManager, RoleManager<IdentityRole<int>> roleManager, IConfiguration conf)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<ResponseMiddleware>();

            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features.Get<IExceptionHandlerPathFeature>().Error;
                if(exception is NotFoundException)
                {
                    var type = context.Response.ContentType;
                    context.Response.StatusCode = (int) HttpStatusCode.UnprocessableEntity;
                    await context.Response.WriteAsJsonAsync(new ErrorResponse { StatusCode = ApiException.ResponseCode, Message = exception.Message });
                }
                else if(exception is ApiException)
                {
                    context.Response.StatusCode = (int) ApiException.ResponseCode;
                    await context.Response.WriteAsJsonAsync(new ErrorResponse { StatusCode = ApiException.ResponseCode, Message = exception.Message });
                }
                else 
                {
                    context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    await context.Response.WriteAsJsonAsync(new ErrorResponse { StatusCode = HttpStatusCode.InternalServerError, Message = exception.Message });
                }
            }));

            app.UseStatusCodePages();
            app.UseCors();
            app.UseAuthentication();
            //IdentityDataInitializer.SeedData(userManager, roleManager, conf);
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
