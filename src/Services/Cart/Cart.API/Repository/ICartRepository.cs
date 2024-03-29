﻿using Cart.API.Entities;
using System.Threading.Tasks;

namespace Cart.API.Repository
{
    public interface ICartRepository
    {
        Task<ShoppingBasket> GetCart(string userName);
        Task<ShoppingBasket> UpdateCart(ShoppingBasket cart);
        Task DeleteCart(string userName);
        Task<bool> RemoveCourse(string userName, string idCourse);
        Task<ShoppingBasket> AddCourseInCart(string userName, ShoppingBasketItem itemToAdd);
    }
}
