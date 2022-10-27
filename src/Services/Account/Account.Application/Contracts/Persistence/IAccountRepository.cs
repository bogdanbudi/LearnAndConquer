using Account.Domain.Entities;
using System.Threading.Tasks;

namespace Account.Application.Contracts.Persistence
{
    public interface IAccountRepository: IAsyncRepository<Domain.Entities.User>
    {
        Task<Domain.Entities.User> GetUserByEmail(string email);
    }
}
