using System.Net.Mail;
using System.Threading.Tasks;
using WebAPI.Models.Auth;

namespace WebAPI.Services.Email
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string fromAddress, string toAddress, string subject, string message);
    }
}
