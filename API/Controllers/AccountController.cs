using Application.DTOS;
using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;
        private readonly IAccountService _accountService;

        public AccountController(UserManager<ApplicationUser> _userManager, IConfiguration _config, IAccountService accountService)
        {
            userManager = _userManager;
            config = _config;
            _accountService = accountService;
        }

        [HttpGet("OwnerServices")]
        public IActionResult GetOwnerServices()
        {
            string ownerID = User.FindFirstValue("uid");

            try
            {
                var ownerServices = _accountService.GetOwnerServices(ownerID);
                return Ok(new CustomResponseDTO<AllServicesDTO>()
                {
                    Data = ownerServices,
                    Message = $"{ownerID} Services",
                    Succeeded = true,
                    Errors = null,
                    PaginationInfo = null,

                });
            }
            catch
            (Exception ex)
            {
                return BadRequest(new CustomResponseDTO<AllServicesDTO>()
                {
                    Data = null,
                    Message = ex.Message,
                    Succeeded = false,
                    Errors = new List<string> { ex.Message },
                    PaginationInfo = null,

                });
            }
        }

        [HttpPost("ownerRegister")]
        public async Task<ActionResult> Register([FromForm] OwnerRegisterDTO ownerRegisterModel)
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
        public async Task<ActionResult> Login(LoginUserDTO loginUserModel)
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

            var email = User.FindFirstValue(ClaimTypes.Email);

            var response = await _accountService.ChangePasswordAsync(changePasswordDto, email);

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
