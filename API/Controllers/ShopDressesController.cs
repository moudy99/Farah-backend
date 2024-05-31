using Application.DTOS;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopDressesController : ControllerBase
    {
        private readonly IShopDressesService _dressesService;

        public ShopDressesController(IShopDressesService dressesService)
        {
            _dressesService = dressesService;
        }
        [HttpPost]
        public ActionResult AddShopDresses(ShopDressesDTo shopDresseDto)
        {
            try
            {
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

        [HttpPut]
        public ActionResult UpdateShopDress(ShopDressesDTo shopDressesDTo)
        {
            try
            {
                var response = _dressesService.UpdateShopDress(shopDressesDTo);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<List<string>>
                {
                    Data = null,
                    Message = "حدث خطأ أثناء تعديل  محل الفساتين",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return BadRequest(errorResponse);
            }
        }


        [HttpDelete]
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
