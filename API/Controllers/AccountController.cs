using Application.DTOS;
using Core.Entities;
using Microsoft.AspNetCore.Http;
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

        public AccountController
            (UserManager<ApplicationUser> _userManager, IConfiguration _config)
        {
            userManager = _userManager;
            config = _config;
        }

        //[HttpPost("CustomerRegister")]
        //public async Task<ActionResult<CustomResponseDTO> Register(CustomerRegisterDTO customerRegisterDTO)
        //{

        //}
    }
}
