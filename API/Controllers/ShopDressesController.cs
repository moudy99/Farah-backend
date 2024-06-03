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
    public class ShopDressesController : ControllerBase
    {
        private readonly IShopDressesService _dressesService;
        private readonly UserManager<ApplicationUser> userManager;

        public ShopDressesController(IShopDressesService dressesService, UserManager<ApplicationUser> _userManager)
        {
            _dressesService = dressesService;
            userManager = _userManager;
        }


        [HttpGet]
        public ActionResult GetAllShopDresses(int page = 1, int pageSize = 10)
        {
            try
            {
                var response = _dressesService.GetAllShopDresses(page, pageSize);
                if (response.Data.Count > 0)
                {
                    return Ok(response);
                }
                return StatusCode(500, "No Beauty Center just added");
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



        [HttpGet("GetShopDressesByName")]
        public ActionResult GetShopDressesByName(string name)
        {
            try
            {
                var response = _dressesService.GetShopDressesByName(name);
                if (response.Data.Count > 0)
                {
                    return Ok(response);
                }
                return StatusCode(500, "No Shop Dresses match with this name");

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



        [HttpGet("GetShopDressesById")]
        public ActionResult GetShopDressesById(int id)
        {
            try
            {
                var response = _dressesService.GetShopDressesById(id);
                if (response.Data != null)
                {
                    return Ok(response);
                }
                return StatusCode(500, "No Shop Dresses match with this Id");

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
        public ActionResult AddShopDresses(ShopDressesDTo shopDresseDto)
        {
            string OwnerID = User.FindFirstValue("uid");
            try
            {
                shopDresseDto.OwnerID = OwnerID;
                var response = _dressesService.AddShopDress(shopDresseDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<List<string>>
                {
                    Data = null,
                    Message = "حدث خطأ أثناء إضافة  محل الفساتين ",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return BadRequest(errorResponse);
            }
        }
        [HttpPut]
        [Authorize]

        public ActionResult UpdateShopDress(ShopDressesDTo shopDressesDTo, int id)
        {
            string OwnerID = User.FindFirstValue("uid");
            try
            {
                shopDressesDTo.OwnerID = OwnerID;
                var response = _dressesService.UpdateShopDress(shopDressesDTo, id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<List<string>>
                {
                    Data = null,
                    Message = "حدث خطأ أثناء تعديل محل الفساتين",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return BadRequest(errorResponse);
            }
        }



        [HttpDelete]
        [Authorize]
        public ActionResult DeleteShopDressById(int id)
        {
            try
            {
                var response = _dressesService.DeleteShopDressById(id);
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
