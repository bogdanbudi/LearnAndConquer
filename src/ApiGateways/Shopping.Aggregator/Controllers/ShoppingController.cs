using Microsoft.AspNetCore.Mvc;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ShoppingController : ControllerBase
    {
        private readonly ITutorialService _TutorialService;
        private readonly ICartService _CartService;
        private readonly IOrderService _orderService;

        public ShoppingController(ITutorialService TutorialService, ICartService CartService, IOrderService orderService)
        {
            _TutorialService = TutorialService ?? throw new ArgumentNullException(nameof(TutorialService));
            _CartService = CartService ?? throw new ArgumentNullException(nameof(CartService));
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        [HttpGet("{userName}", Name = "GetShopping")]
        [ProducesResponseType(typeof(ShoppingModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingModel>> GetShopping(string userName)
        {
            // get Cart with username
            // iterate Cart items and consume products with Cart item productId member
            // map product related members into Cartitem dto with extended columns
            // consume ordering microservices in order to retrieve order list
            // return root ShoppngModel dto class which including all responses

            var Cart = await _CartService.GetCart(userName);

            foreach (var item in Cart.ShoppingItems)
            {
                var product = await _TutorialService.GetTutorial(item.IdCourse);

                // set additional product fields onto Cart item
                item.IdCourse = product.Name;
                item.Category = product.Category;
                item.Summary = product.Summary;
                item.Description = product.Description;
                item.ImageFile = product.ImageFile;
            }

            var orders = await _orderService.GetOrdersByUserName(userName);

            var shoppingModel = new ShoppingModel
            {
                UserName = userName,
                CartWithCourses = Cart,
                Orders = orders
            };

            return Ok(shoppingModel);
        }
    }
}