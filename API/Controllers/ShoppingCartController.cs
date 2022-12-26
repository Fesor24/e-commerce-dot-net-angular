using API.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly IMapper mapper;

        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository, IMapper mapper)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ShoppingCart>> GetCartById(string id)
        {
            var cart = await shoppingCartRepository.GetShoppingCartAsync(id);

            //if it is null we will use the cartId the client has given us to generate a new cart for him
            return Ok(cart ?? new ShoppingCart(id));
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> UpdateCart(ShoppingCartDto cart)
        {
            var customerCart = mapper.Map<ShoppingCartDto, ShoppingCart>(cart);

            var updatedBasket = await shoppingCartRepository.UpdateShoppingCartAsync(customerCart);

            return Ok(updatedBasket);
        }

        [HttpDelete]
        public async Task DeleteBasketAsync(string id)
        {
            await shoppingCartRepository.DeleteShoppingCartAsync(id);
        }
    }
}
