using Application.DTOS;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService AdminService;

        public AdminController(IAdminService _adminService)
        {
            AdminService = _adminService;
        }

        [HttpGet("owners")]
        public ActionResult GetAllOwners() 
        {
            try
            {
                var response = AdminService.GetAllOwners();
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<List<string>>
                {
                    Data = null,
                    Message = "Error",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return BadRequest(errorResponse);
            }

        }

    }
}
