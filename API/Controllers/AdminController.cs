using Application.DTOS;
using Application.Interfaces;
using Application.Services;
using Core.Enums;
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
        public ActionResult GetAllOwners(int page=1, int pageSize = 6) 
        {
            try
            {
                var response = AdminService.GetAllOwners(page,pageSize);
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
                var owner = AdminService.GetById(ownerId);
                if (owner.AccountStatus == OwnerAccountStatus.Accepted)
                {
                    return Conflict(new CustomResponseDTO<OwnerAccountStatus>
                    {
                        Data = owner.AccountStatus,
                        Message = "Owner is already accepted",
                        Succeeded = false,
                        Errors = null
                    });
                }

                owner.AccountStatus = OwnerAccountStatus.Accepted;
                AdminService.Update(owner);
                AdminService.Save();

                var response = new CustomResponseDTO<OwnerAccountStatus>
                {
                    Data = owner.AccountStatus,
                    Message = "Owner accepted",
                    Succeeded = true,
                    Errors = null
                };
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
                var owner = AdminService.GetById(ownerId);
                if (owner.AccountStatus == OwnerAccountStatus.Decline)
                {
                    return Conflict(new CustomResponseDTO<OwnerAccountStatus>
                    {
                        Data = owner.AccountStatus,
                        Message = "Owner is already declined",
                        Succeeded = false,
                        Errors = null
                    });
                }

                owner.AccountStatus = OwnerAccountStatus.Decline;
                AdminService.Update(owner);
                AdminService.Save();

                var response = new CustomResponseDTO<OwnerAccountStatus>
                {
                    Data = owner.AccountStatus,
                    Message = "Owner declined",
                    Succeeded = true,
                    Errors = null
                };
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
            try {
                var response = AdminService.BlockOwner(ownerId);
                return Ok(response);
            }
            catch (Exception ex) 
            {
                var errorResponse = new CustomResponseDTO<List<string>>
                {
                    Data = null,
                    Message = "Error while Blocking the owner",
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
                    Message = "Error while Unblocking the owner",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return BadRequest(errorResponse);
            }
        }
    }

    
}
