﻿using Application.DTOS;
using Application.Interfaces;
using Azure;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HallController : ControllerBase
    {
        private readonly IHallService HallService;
        public HallController(IHallService _hallService) 
        {
            HallService = _hallService;
        }

        [HttpGet("AllHalls")]
        public IActionResult GetAll(int page = 1, int pageSize = 6) 
        {
            try
            {
                var response = HallService.GetAllHalls(page, pageSize);
                if (response.Data == null)
                {
                    return NotFound(new CustomResponseDTO<List<HallDTO>>
                    {
                        Data = null,
                        Message = "No Halls Found",
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
                    Message = "Error while retrieving the Halls",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return BadRequest(errorResponse);
            }
        }

        [HttpPost("AddHall")]
        public async Task<ActionResult> AddHall(AddHallDTO hallDTO)
        {
            string OwnerID = User.FindFirstValue("uid");

            try
            {
                hallDTO.OwnerID = OwnerID;
                var Hall = await HallService.AddHall(hallDTO);
                return Ok(new CustomResponseDTO<HallDTO>
                {
                    Data = Hall,
                    Message = "Hall added successfully",
                    Succeeded = true,
                    Errors = null
                });
            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<List<string>>
                {
                    Data = null,
                    Message = "Error while adding the Hall",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return BadRequest(errorResponse);
            }
        }

        [HttpPut]
        public async Task<ActionResult> EditHall(HallDTO HallDto, int id)
        {
            string OwnerID = User.FindFirstValue("uid");

            try
            {
                HallDto.OwnerID = OwnerID;
                HallDto.HallID = id;
                var hall = await HallService.EditHall(id, HallDto);

                return Ok(new CustomResponseDTO<HallDTO>
                {
                    Data = hall,
                    Message = "Hall updated successfully",
                    Succeeded = true,
                    Errors = null
                });
            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<List<string>>
                {
                    Data = null,
                    Message = "Error while updating the Hall",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return BadRequest(errorResponse);
            }
        }

        [HttpDelete]
        public IActionResult DeleteHall(int id)
        {
            try
            {
                HallService.Delete(id);

                return Ok(new CustomResponseDTO<object>
                {
                    Data = id,
                    Message = "Hall Deleted successfully",
                    Succeeded = true,
                    Errors = null
                });
            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<List<string>>
                {
                    Data = null,
                    Message = "Error while Deleting the Hall",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return BadRequest(errorResponse);
            }
        }
        [HttpGet("CarByID")]
        public IActionResult GetHallByID(int id)
        {

            var hall = HallService.GetHallById(id);
            if (!hall.Succeeded)
            {
                return NotFound(hall);
            }
            return Ok(hall);

        }
    }
}