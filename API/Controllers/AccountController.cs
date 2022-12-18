using System.Runtime.CompilerServices;
using System.Security.Claims;
using API.DTOs;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly ITokenService token;
        private readonly IMapper mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService token, IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.token = token;
            this.mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> SignInAsync([FromBody] LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);

            if (user is null) return Unauthorized(new ApiResponse(401));

            var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            return new UserDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = token.CreateToken(user)
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> RegisterUserAsync([FromBody] RegisterDto registerDto)
        {
            if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse
                {
                    Error = new[] { "Email address exists" }
                });
            }

            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };

            var result = await userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return new UserDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = token.CreateToken(user)
            };
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUserAsync()
        {
            //Using our extension method to get the user
            var user = await userManager.FindUserByClaimsPrinciple(User);

            return new UserDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = token.CreateToken(user)
            };
        }

        /// <summary>
        /// Creating this method to do asynchronous validation on the client side
        /// </summary>
        /// <param name="email"></param>
        /// <returns><see cref="bool"/></returns>
        [HttpGet("email-exists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await userManager.FindByEmailAsync(email) is not null;
        }

        [HttpGet("user-address")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            var user = await userManager.FindUserByClaimsPrincipleWithAddressAsync(User);

            if(user is null)
            {
                return NotFound(new ApiResponse(404));
            }

            return mapper.Map<Address, AddressDto>(user.Address);
        }

        /// <summary>
        /// Endpoint route to update user address
        /// </summary>
        /// <param name="addressDto"></param>
        /// <returns></returns>
        [HttpPut("address")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> UpdateUserAddressAsync(AddressDto addressDto)
        {
            var user = await userManager.FindUserByClaimsPrincipleWithAddressAsync(User);

            user.Address = mapper.Map<AddressDto, Address>(addressDto);
            
            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded) return Ok(mapper.Map<Address, AddressDto>(user.Address));

            return BadRequest("Problem updating the user");

        }
    }
}
