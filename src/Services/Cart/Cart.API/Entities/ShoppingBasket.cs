using System.Collections.Generic;

namespace Cart.API.Entities
{
    public class ShoppingBasket
    {
        public string UserName { get; set; }

        public List<ShoppingBasketItem> ShoppingItems { get; set; } = new List<ShoppingBasketItem>();

        public ShoppingBasket()
        {

        }

        public ShoppingBasket(string userName)
        {
            this.UserName = userName;
        }

        public decimal TotalPrice
        {
            get
            {
                decimal totalprice = 0;
                foreach(var item in ShoppingItems)
                {
                    totalprice += item.Price;
                }
                return totalprice;
            }
        }

    }
}
