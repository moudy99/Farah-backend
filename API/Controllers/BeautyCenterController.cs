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
    public class BeautyCenterController : ControllerBase
    {
        private readonly IBeautyService _beautyService;

        private readonly UserManager<ApplicationUser> userManager;

        public BeautyCenterController(IBeautyService beautyService, UserManager<ApplicationUser> _userManager)
        {
            _beautyService = beautyService;
            _userManager = userManager;
        }

        [HttpGet]
        public ActionResult GetAllBeautyCenters(int page = 1, int pageSize = 10)
        {
            try
            {
                var response = _beautyService.GetAllBeautyCenters(page, pageSize);
                if (response.Data.Count > 0)
                {
                    return Ok(response);
                }
                return StatusCode(500, "No Beauty Center Added");
            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<List<string>>
                {
                    Data = null,
                    Message = "ايروووووووووووووور",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message, ex.StackTrace }
                };
                return StatusCode(500, errorResponse);
            }
        }

        [HttpGet("GetBeautyCentersByName")]
        public ActionResult GetBeautyCentersByName(string name)
        {
            try
            {
                var response = _beautyService.GetBeautyCenterByName(name);
                if (response.Data.Count > 0)
                {
                    return Ok(response);
                }
                return StatusCode(500, "No Beauty Center match with this name");

            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<List<string>>
                {
                    Data = null,
                    Message = "ايروووووووووووووور",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message, ex.StackTrace }
                };
                return StatusCode(500, errorResponse);
            }
        }


        [HttpGet("GetBeautyCentersById")]
        public ActionResult GetBeautyCentersById(int id)
        {
            try
            {
                var response = _beautyService.GetBeautyCenterById(id);
                if (response.Data != null)
                {
                    return Ok(response);
                }
                return StatusCode(500, "No Beauty Center match with this Id");

            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<List<string>>
                {
                    Data = null,
                    Message = "ايروووووووووووووور",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message, ex.StackTrace }
                };
                return StatusCode(500, errorResponse);
            }
        }


        [HttpPost]
        [Authorize]
        public ActionResult AddBeautyCenter([FromForm] AddBeautyCenterDTO beautyCenterDTO)
        {
            string OwnerID = User.FindFirstValue("uid");
            try
            {
                beautyCenterDTO.OwnerID = OwnerID;
                var response = _beautyService.AddBeautyCenter(beautyCenterDTO);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<List<string>>
                {
                    Data = null,
                    Message = "حدث خطأ أثناء إضافة البيوتي سنتر",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return BadRequest(errorResponse);
            }
        }



        [HttpPut]
        [Authorize]
        public ActionResult UpdateBeautyCenter(AddBeautyCenterDTO beautyCenterDTO, int id)
        {
            string OwnerID = User.FindFirstValue("uid");
            try
            {
                beautyCenterDTO.OwnerID = OwnerID;
                var response = _beautyService.UpdateBeautyCenter(beautyCenterDTO, id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<List<string>>
                {
                    Data = null,
                    Message = "حدث خطأ أثناء تعديل البيوتي سنتر",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return BadRequest(errorResponse);
            }
        }



        [HttpDelete]
        [Authorize]
        public ActionResult DeleteBeautyCenterById(int id)
        {
            try
            {
                var response = _beautyService.DeleteBeautyCenterById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<List<string>>
                {
                    Data = null,
                    Message = "حدث خطأ أثناء حذف البيوتي سنتر",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return BadRequest(errorResponse);
            }
        }







    }
}
