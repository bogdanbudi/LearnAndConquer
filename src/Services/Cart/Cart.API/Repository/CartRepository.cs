using Cart.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cart.API.Repository
{
    public class CartRepository: ICartRepository
    {
        private readonly IDistributedCache _redisCache;

        public CartRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        public async Task<ShoppingBasket> GetCart(string userName)
        {
            var cart = await _redisCache.GetStringAsync(userName);

            if (String.IsNullOrEmpty(cart))
                return null;

            return JsonConvert.DeserializeObject<ShoppingBasket>(cart);
        }

        public async Task<ShoppingBasket> UpdateCart(ShoppingBasket cart)
        {
            await _redisCache.SetStringAsync(cart.UserName, JsonConvert.SerializeObject(cart));

            return await GetCart(cart.UserName);
        }

        public async Task DeleteCart(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }

        public async Task<ShoppingBasket> AddCourseInCart(string userName, ShoppingBasketItem itemToAdd)
        {
            var checkCart = await _redisCache.GetStringAsync(userName);

            if (String.IsNullOrEmpty(checkCart))
                await _redisCache.SetStringAsync(userName, JsonConvert.SerializeObject(new ShoppingBasket(userName)));

            var cart = await GetCart(userName);
            if(String.IsNullOrEmpty(cart.UserName) || cart == null)
            {
                cart.UserName = userName;
            }


            cart.ShoppingItems.Add(itemToAdd);
            return await UpdateCart(cart);
        }

        public async Task<bool> RemoveCourse(string userName, string idCourse)
        {
            var cart = await GetCart(userName);

            var item = cart.ShoppingItems.SingleOrDefault(x => x.IdCourse == idCourse);
            if (item != null)
            {
                cart.ShoppingItems.Remove(item);
                await _redisCache.SetStringAsync(cart.UserName, JsonConvert.SerializeObject(cart));
                return true;
            }

            return false;

            //FilterDefinition<ShoppingBasket> filter = Builders<ShoppingBasket>.Filter.Exists(p => p.ShoppingItems.SingleOrDefault(x => x.IdCourse == idCourse));

        }
    }
}
