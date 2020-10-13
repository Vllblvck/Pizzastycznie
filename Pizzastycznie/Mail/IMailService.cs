using System.Threading.Tasks;

namespace Pizzastycznie.Mail
{
    public interface IMailService
    {
        public Task SendEmailAsync(string email, string subject, string body);
    }
}