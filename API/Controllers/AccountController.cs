using Application.DTOS;
using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> _userManager, IConfiguration _config, IAccountService accountService, UserManager<ApplicationUser> userManager)
        {
            userManager = _userManager;
            config = _config;
            _accountService = accountService;
            this._userManager = userManager;
        }

        [HttpPost("ownerRegister")]
        public async Task<ActionResult> Register(OwnerRegisterDTO ownerRegisterModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _accountService.OwnerRegisterAsync(ownerRegisterModel);

            if (response.Succeeded)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(new { message = response.Message, errors = response.Errors });
            }
        }

        [HttpPost("customerRegister")]
        public async Task<ActionResult> Register(CustomerRegisterDTO customerRegisterModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _accountService.CustomerRegisterAsync(customerRegisterModel);

            if (response.Succeeded)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(new { message = response.Message, errors = response.Errors });
            }
        }


        [HttpPost("login")]
        public async Task<ActionResult> Register(LoginUserDTO loginUserModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _accountService.Login(loginUserModel);

            if (response.Succeeded)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(new { message = response.Message, errors = response.Errors });
            }
        }


        [HttpPost("changePassword")]
        [Authorize]
        public async Task<ActionResult> ChangePassword(ChangePasswordDTO changePasswordDto)
        {

            var user = await _userManager.GetUserAsync(User);
            var response = await _accountService.ChangePasswordAsync(changePasswordDto, user.Id);

            if (response.Succeeded)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(new { message = response.Message, errors = response.Errors });
            }
        }

    }
}
