using Application.DTOS;
using Application.Interfaces;
using Application.Services;
using Azure;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService carService;
        public CarController(ICarService _carService) 
        { 
            carService = _carService;
        }

        [HttpGet("Cars")]
        public IActionResult GetAllCars(int page, int pageSize)
        {



            try
            {
                var response = carService.GetAllCars(page, pageSize);
                if (response.Data == null)
                {
                    return NotFound(new CustomResponseDTO<List<OwnerDTO>>
                    {
                        Data = null,
                        Message = "No Cars Found",
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
                    Message = "Error while Retrieve The Cars",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return BadRequest(errorResponse);
            }
        }
    }
}
