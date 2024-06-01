using Application.DTOS;
using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;
        private readonly IAccountService _accountService;

        public AccountController
            (UserManager<ApplicationUser> _userManager, IConfiguration _config, IAccountService accountService)
        {
            userManager = _userManager;
            config = _config;
            _accountService = accountService;
        }

        [HttpPost("ownerRegister")]
        public async Task<ActionResult> Register(OwnerRegisterDTO ownerRegisterModel)
        {
            if (ModelState.IsValid)
            {
                var response = await _accountService.OwnerRegisterAsync(ownerRegisterModel);

                return Ok(response);
            }

            return BadRequest(ownerRegisterModel);
        }

        [HttpPost("customerRegister")]
        public async Task<ActionResult> Register(CustomerRegisterDTO customerRegisterModel)
        {
            return Ok();
        }
        [HttpPost("login")]
        public async Task<ActionResult> Register(LoginUserDTO loginUserModel)
        {
            return Ok();
        }
    }
}
