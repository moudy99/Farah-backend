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
                    Errors = new List<string> { ex.Message }
                };
                return BadRequest(errorResponse);
            }
        }

        [HttpPost]
        public ActionResult AddBeautyCenter(BeautyCenterDTO beautyCenterDTO)
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

    }
}
