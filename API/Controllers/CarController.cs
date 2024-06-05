using Application.DTOS;
using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;

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
        public IActionResult GetAllCars(int page = 1, int pageSize = 6)
        {
            try
            {
                var response = carService.GetAllCars(page, pageSize);
                if (response.Data == null)
                {
                    return NotFound(new CustomResponseDTO<List<CarDTO>>
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
                    Message = "Error while retrieving the cars",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return BadRequest(errorResponse);
            }
        }

        [HttpPost("AddCar")]
        public IActionResult AddCar( CarDTO carDto) // Use FromForm to handle file uploads
        {
            string OwnerID = User.FindFirstValue("uid");

            try
            {
                carDto.OwnerID = OwnerID;
                var car = carService.AddCar(carDto);

                return Ok(new CustomResponseDTO<CarDTO>
                {
                    Data = car,
                    Message = "Car added successfully",
                    Succeeded = true,
                    Errors = null
                });
            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<List<string>>
                {
                    Data = null,
                    Message = "Error while adding the car",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return BadRequest(errorResponse);
            }
        }
    }
}
