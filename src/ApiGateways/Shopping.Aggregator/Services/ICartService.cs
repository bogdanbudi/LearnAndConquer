using Shopping.Aggregator.Models;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
{
    public interface ICartService
    {
        Task<CartModel> GetCart(string userName);
    }
}
