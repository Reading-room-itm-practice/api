using Core.Exceptions;
using Core.ServiceResponses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }

            app.UseHttpsRedirection();
            app.UseMiddleware<ResponseStatusCodeMiddleware>();

            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var exception = context.Features.Get<IExceptionHandlerPathFeature>().Error;
                if (exception is NotFoundException)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    await context.Response.WriteAsJsonAsync(ServiceResponse.Error(exception.Message, ApiException.ResponseCode));
                }
                else if (exception is ApiException)
                {
                    context.Response.StatusCode = (int)ApiException.ResponseCode;
                    await context.Response.WriteAsJsonAsync(ServiceResponse.Error(exception.Message, ApiException.ResponseCode));
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await context.Response.WriteAsJsonAsync(ServiceResponse.Error(exception.Message, HttpStatusCode.InternalServerError));
                }
            }));

            app.UseStatusCodePages();
            app.UseCors();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
