using System.Collections.Generic;

namespace Shopping.Aggregator.Models
{
    public class CartModel
    {
        public string UserName { get; set; }

        public List<ShoppingBasketItemExtendedModel> ShoppingItems { get; set; } = new List<ShoppingBasketItemExtendedModel>();

        public decimal TotalPrice { get; set; }
    }
}
