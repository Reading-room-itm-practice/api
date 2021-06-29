using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DataAccessLayer;

namespace WebAPI.Installers
{
    public class DbInstaller : Installer
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var config = new StringBuilder(configuration["ConnectionStrings:ApiConntectionString"]);
            string connection = config.Replace("ENVPW", configuration["DB_PASSWORD"])
                .Replace("ENVID", configuration["DB_USER_ID"])
                .ToString();

            services.AddDbContext<ApiDbContext>(options => options.UseSqlServer(connection));
        }
    }
}
