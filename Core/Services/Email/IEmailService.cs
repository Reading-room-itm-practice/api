using System.Threading.Tasks;

namespace Core.Services.Email
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string fromAddress, string toAddress, string subject, string message);
    }
}
