using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository AdminRepository;
        public AdminController(IAdminRepository _adminRepository)
        {
            AdminRepository = _adminRepository;
        }

        //[HttpGet]

    }
}
