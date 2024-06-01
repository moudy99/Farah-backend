using Application.DTOS;
using Application.Interfaces;
using Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllersa
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
        [Authorize]
        public ActionResult GetAllOwners(int page = 1, int pageSize = 6, OwnerAccountStatus? status = null, bool? isBlocked = null)
        {
            try
            {
                var response = AdminService.GetFilteredOwners(status, isBlocked, page, pageSize);

                if (response.Data == null)
                {
                    return NotFound(new CustomResponseDTO<List<OwnerDTO>>
                    {
                        Data = null,
                        Message = "No owners found with the given criteria",
                        Succeeded = false,
                        Errors = null
                    });
                }

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

        [HttpPut("AcceptOwner")]
        public ActionResult AcceptOwner(string ownerId)
        {
            try
            {
                var response = AdminService.AcceptOwner(ownerId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<List<string>>
                {
                    Data = null,
                    Message = "Error while accepting the owner",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return BadRequest(errorResponse);
            }
        }

        [HttpPut("DeclineOwner")]
        public ActionResult DeclineOwner(string ownerId)
        {
            try
            {
                var response = AdminService.DeclineOwner(ownerId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<List<string>>
                {
                    Data = null,
                    Message = "Error while declining the owner",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return BadRequest(errorResponse);
            }
        }

        [HttpPut("BlockOwner")]
        public IActionResult BlockOwner(string ownerId)
        {
            try
            {
                var response = AdminService.BlockOwner(ownerId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<List<string>>
                {
                    Data = null,
                    Message = "Error while blocking the owner",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return BadRequest(errorResponse);
            }
        }

        [HttpPut("UnblockOwner")]
        public IActionResult UnblockOwner(string ownerId)
        {
            try
            {
                var response = AdminService.UnblockOwner(ownerId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<List<string>>
                {
                    Data = null,
                    Message = "Error while unblocking the owner",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return BadRequest(errorResponse);
            }
        }
    }
}