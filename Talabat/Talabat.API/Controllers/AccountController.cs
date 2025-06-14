using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.DTOs;
using Talabat.API.Errors;
using Talabat.Core.Entities.Identity;
using Talabat.Core.ServicesContract;

namespace Talabat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenRepository _tokenRepository;

        public AccountController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager , ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenRepository = tokenRepository;
        }

        #region Register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = new AppUser()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email.Split("0")[0],
                PhoneNumber = registerDto.PhoneNumber,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded is false)
            {
                return BadRequest(new ApiResponse(400));
            }

            var ReturnedUser = new UserDto()
            {
                Name = user.DisplayName,
                Email = user.Email,
                Token = await _tokenRepository.CreateTokenAsync(user )
            };

            return Ok(ReturnedUser);


        }
        #endregion

        #region Login

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                return Unauthorized(new ApiResponse(401));
            }

            var CheckPassword = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (CheckPassword == null)
            {
                return Unauthorized(new ApiResponse(401));
            }

            var loggedUser = new UserDto()
            {
                Name = user.DisplayName,
                Email = user.Email,
                Token = await _tokenRepository.CreateTokenAsync(user )
            };

            return Ok(loggedUser);


        }


        #endregion
    }
}
