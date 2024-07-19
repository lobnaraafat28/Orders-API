using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Odrer.Core;
using Odrer.Services.Services;
using Order.PL.API.DTOs;
using Orders.Core.Models;

namespace Orders.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ITokenSer tokenService;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenSer tokenService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginD)
        {
            var user = await userManager.FindByNameAsync(loginD.UserName);
            if (user == null) return new UnauthorizedResult();
            var result = await signInManager.CheckPasswordSignInAsync(user, loginD.Password, false);
            if (!result.Succeeded) return new UnauthorizedResult();
            return Ok(new UserDTO()
            {
                UserName = user.UserName,
                Token = await tokenService.CreateTokenAsync(user, userManager)
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerD)
        {
            if (CheckEmailExists(registerD.Email).Result.Value) return BadRequest();
            var user = new User()
            {
                
                Email = registerD.Email,
                PhoneNumber = registerD.PhoneNumber,
                UserName = registerD.Email.Split("@")[0]
            };
            var result = await userManager.CreateAsync(user, registerD.Password);
            if (!result.Succeeded) return BadRequest();
            return Ok(new UserDTO()
            {
                UserName = user.Email.Split("@")[0],
                Token = await tokenService.CreateTokenAsync(user, userManager)
            });
        }
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            return await userManager.FindByEmailAsync(email) is not null;
        }
    }
}
