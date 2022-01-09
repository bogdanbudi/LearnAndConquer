using Cart.API.Entities;
using Cart.API.GrpcServices;
using Cart.API.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Cart.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _repository;
        private readonly DisocuntGrpcService _discountGrpcService;


        public CartController(ICartRepository repository, DisocuntGrpcService discountGrpcService) 
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _discountGrpcService = discountGrpcService ?? throw new ArgumentNullException(nameof(discountGrpcService));
        }

        [HttpGet("{userName}", Name = "GetCart")]
        [ProducesResponseType(typeof(ShoppingBasket), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingBasket>> GetCart(string userName)
        {
            var cart = await _repository.GetCart(userName);
            return Ok(cart ?? new ShoppingBasket(userName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingBasket), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingBasket>> UpdateCart([FromBody] ShoppingBasket cart)
        {
            //TODO : Communicate with Discout.Grpc
            //&& Calculate the latest price for tutorial into shopping basket ( our product )
            //CartApi will be client of the Grpc Discount 

            foreach(var item in cart.ShoppingItems)
            {
                var coupon = await _discountGrpcService.GetDiscount(item.NameCourse);
                item.Price -= coupon.Amount;
            }
            return Ok(await _repository.UpdateCart(cart));
        }

        [HttpDelete("{userName}", Name = "DeleteCart")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteCart(string userName)
        {
            await _repository.DeleteCart(userName);
            return Ok();
        }
    }
}
