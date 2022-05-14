using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
{
    public class CartService : ICartService
    {
        private readonly HttpClient _client;

        public CartService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<CartModel> GetCart(string userName)
        {
            var response = await _client.GetAsync($"/api/v1/Cart/{userName}");
            return await response.ReadContentAs<CartModel>();
        }
    }
}