using Account.Application.Contracts.Models;
using System.Threading.Tasks;

namespace Account.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
