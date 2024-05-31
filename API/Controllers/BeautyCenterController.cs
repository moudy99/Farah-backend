using Application.DTOS;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeautyCenterController : ControllerBase
    {
        private readonly IBeautyService _beautyService;



        public BeautyCenterController(IBeautyService beautyService)
        {
            _beautyService = beautyService;
        }

        [HttpGet]
        public ActionResult GetAllBeautyCenters(int page = 1, int pageSize = 10)
        {
            try
            {
                var response = _beautyService.GetAllBeautyCenters(page, pageSize);
                return Ok(response);
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


        [HttpPost]
        public ActionResult AddBeautyCenter(AddBeautyCenterDTO beautyCenterDTO)
        {
            try
            {
                var response = _beautyService.AddBeautyCenters(beautyCenterDTO);
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
        public ActionResult UpdateBeautyCenter(BeautyCenterDTO beautyCenterDTO)
        {
            try
            {
                var response = _beautyService.UpdateBeautyCenter(beautyCenterDTO);
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
