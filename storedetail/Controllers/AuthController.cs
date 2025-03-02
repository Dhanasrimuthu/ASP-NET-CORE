using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using storedetail.Model.Domain;
using storedetail.Model.Dtos;
using storedetail.Repositories;

//User@123
namespace storedetail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(UserManager<ApplicationUser> userManager,ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Registers")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var user = new ApplicationUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };

            var result = await _userManager.CreateAsync(user, registerRequestDto.Password);

            if (result.Succeeded)
            {
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    result = await _userManager.AddToRolesAsync(user, registerRequestDto.Roles);

                    if (result.Succeeded)
                    {
                        return Ok("User registered successfully! Please login.");
                    }
                }

                return Ok("User registered successfully! No roles added.");
            }

            return BadRequest("Something went wrong while registering the user.");
        }

        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.Username);

            if (user != null)
            {
                var checkPassword = await _userManager.CheckPasswordAsync(user,loginRequestDto.Password);

                if (checkPassword)
                {
                    var roles=await _userManager.GetRolesAsync(user);
                    if(roles != null)
                    {
                        var jwtToken =_tokenRepository.CreateJWTToken(user,roles.ToList());

                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };
                        return Ok(response);
                    }
                }
            }
            return BadRequest("Username or Password incorrect");
        }
        
    }
}
