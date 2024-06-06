﻿using Application.DTOS;
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
    public class PhotographyController : ControllerBase
    {
        private readonly IPhotographyService _photoService;

        private readonly UserManager<ApplicationUser> userManager;

        public PhotographyController(IPhotographyService photoService, UserManager<ApplicationUser> _userManager)
        {
            _photoService = photoService;
            _userManager = userManager;
        }


        [HttpGet]
        public ActionResult GetAllPhotographer(int page = 1, int pageSize = 10)
        {
            try
            {
                var response = _photoService.GetAllPhotographer(page, pageSize);
                if (response.Data.Count > 0)
                {
                    return Ok(response);
                }
                return StatusCode(500, "No Photographer  Added");
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

        [HttpGet("GetPhotographerById")]
        public ActionResult GetPhotographerById(int id)
        {
            try
            {
                var response = _photoService.GetPhotographerById(id);
                if (response.Data != null)
                {
                    return Ok(response);
                }
                return StatusCode(500, "No Photographer match with this Id");

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
        public ActionResult AddPhotographer([FromForm] AddPhotographyDTO photographyDTO)
        {
            string OwnerID = User.FindFirstValue("uid");
            try
            {
                photographyDTO.OwnerID = OwnerID;
                var response = _photoService.AddPhotographer(photographyDTO);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<List<string>>
                {
                    Data = null,
                    Message = "حدث خطأ أثناء إضافة  الفوتوغرافر",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return BadRequest(errorResponse);
            }
        }



        [HttpPut("{id}")]
        [Authorize]
        public ActionResult UpdatePhotographer(int id, [FromForm] AddPhotographyDTO photographyDTO)
        {
            string OwnerID = User.FindFirstValue("uid");
            try
            {
                photographyDTO.OwnerID = OwnerID;
                var response = _photoService.UpdatePhotographer(id, photographyDTO);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<List<string>>
                {
                    Data = null,
                    Message = "حدث خطأ أثناء تحديث الفوتوجرافر",
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
                var response = _photoService.DeletePhotographerById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<List<string>>
                {
                    Data = null,
                    Message = "حدث خطأ أثناء حذف  الفوتوجرافر",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return BadRequest(errorResponse);
            }
        }

    }
}