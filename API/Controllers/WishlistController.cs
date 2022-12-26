using System.Security.Principal;
using API.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistRepository wishlistRepository;
        private readonly IMapper mapper;

        public WishlistController(IWishlistRepository wishlistRepository, IMapper mapper)
        {
            this.wishlistRepository = wishlistRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Wishlist>> GetWishlistById(string id)
        {
            var wishlist = await wishlistRepository.GetWishlistAsync(id);

            return Ok(wishlist ?? new Wishlist(id));
        }

        [HttpPost]
        public async Task<ActionResult<Wishlist>> UpdateWishlist(WishlistDto wishlist)
        {
            var customerWishlist = mapper.Map<WishlistDto, Wishlist>(wishlist);

            var updatedWishlist = await wishlistRepository.UpdateWishlist(customerWishlist);

            return Ok(updatedWishlist);
        }

        [HttpDelete]
        public async Task DeleteWishlistAsync(string id)
        {
            await wishlistRepository.DeleteWishlist(id);
        }
    }
}
