using Application.Interfaces;
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

        //[HttpGet("Cars")]
        //public IActionResult GetAllCars()
        //{
        //    var Result = carService.GetAll();
        //}
    }
}
