using AutoMapper;
using Cart.API.Entities;
using Cart.API.GrpcServices;
using Cart.API.Repository;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Linq;

namespace Cart.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
   // [Authorize("ClientIdPolicy")]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _repository;
        private readonly DisocuntGrpcService _discountGrpcService;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;


        public CartController(ICartRepository repository, DisocuntGrpcService discountGrpcService, IMapper mapper, IPublishEndpoint publishEndpoint) 
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _discountGrpcService = discountGrpcService ?? throw new ArgumentNullException(nameof(discountGrpcService));
            _publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
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

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] CartCheckout cartCheckout)
        {
            // get existing basket with total price 
            // Create basketCheckoutEvent -- Set TotalPrice on basketCheckout eventMessage
            // send checkout event to rabbitmq
            // remove the basket

            // get existing basket with total price
            var cart = await _repository.GetCart(cartCheckout.UserName);
            if (cart == null)
            {
                return BadRequest();
            }

            // send checkout event to rabbitmq
            var eventMessage = _mapper.Map<CartCheckoutEvent>(cartCheckout);
            eventMessage.TotalPrice = cart.TotalPrice;
            await _publishEndpoint.Publish(eventMessage);

            // remove the basket
            await _repository.DeleteCart(cart.UserName);

            return Accepted();
        }

        [HttpDelete("{userName}", Name = "DeleteCart")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteCart(string userName)
        {
            await _repository.DeleteCart(userName);
            return Ok();
        }

        [HttpGet("GetCartCount/{userName}", Name = "GetCartCount")]
        [ProducesResponseType(typeof(ShoppingBasket), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> GetCartCount(string userName)
        {
            var cart = await _repository.GetCart(userName);
            return Ok(cart.ShoppingItems.Count);
        }

        [HttpDelete("RemoveCourseFromCart/{userName}/{idCourse}", Name = "RemoveCourseFromCart")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RemoveCourseFromCart(string userName, string idCourse)
        {
            var response = await _repository.RemoveCourse(userName, idCourse);
            return Ok(response);
        }


        [Route("AddCourseToCart/{userName}", Name = "AddCourseToCart")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ShoppingBasket), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingBasket>> AddCourseToCart(string userName, [FromBody] ShoppingBasketItem itemToAdd)
        {
            return Ok(await _repository.AddCourseInCart(userName, itemToAdd));
        }

        //[HttpGet("{getClaim}", Name = "GetClaim")]
        //public IActionResult Get()
        //{
        //    return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        //}


    }
}
