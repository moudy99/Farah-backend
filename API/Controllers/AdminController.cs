﻿using Application.DTOS;
using Application.Helpers;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using AutoMapper.Internal;
using Azure;
using Core.Entities;
using Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Presentation.Controllersa
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService AdminService;
        private readonly IMapper _mapper;

        public AdminController(IAdminService _adminService, IMapper mapper)
        {
            AdminService = _adminService;
            _mapper = mapper;
        }

        [HttpGet("Services")]
        public ActionResult GetAllServices() 
        {
            AllServicesDTO services = AdminService.GetAllServices();


            return Ok(services);
        }

        [HttpGet("ServiceType")]
        public ActionResult GetServiceTypeByID(int id) 
        {
            try
            {
                var service = AdminService.GetServiceTypeByID(id);
                return Ok(service);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("owners")]
        public ActionResult GetAllOwners(int page = 1, int pageSize = 6, OwnerAccountStatus? status = null, bool? isBlocked = null)
        {
            try
            {
                var response = AdminService.GetFilteredOwners(status, isBlocked, page, pageSize);

                if (response.Data == null)
                {
                    return NotFound(new CustomResponseDTO<List<OwnerDTO>>
                    {
                        Data = null,
                        Message = "حدث خطأ",
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
                    Message = "حدث خطأ",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return BadRequest(errorResponse);
            }
        }

        [HttpGet("Customers")]
        public ActionResult GetAllCustomers(int page = 1, int pageSize = 6, bool? isBlocked = null)
        {
            try
            {
                var response = AdminService.GetAllCustomers(isBlocked, page, pageSize);
                if (response.Data == null)
                {
                    return NotFound(new CustomResponseDTO<List<CustomerDTO>>
                    {
                        Data = null,
                        Message = "تعذر العثور علي مستخدمين",
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
                    Message = "حدث خطأ",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return BadRequest(errorResponse);
            }
        }

        [HttpGet("GetOwnerById/{ownerId}")]
        public IActionResult GetOwnerById(string ownerId)
        {
            var response = AdminService.GetOwnerById(ownerId);

            if (!response.Succeeded)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
        [HttpGet("SearchUsersByName")]
        public IActionResult SearchUsersByName(string name)
        {
            var response = AdminService.SearchUsersByName(name);

            if (!response.Succeeded)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
        [HttpPut("AcceptOwner")]
        public ActionResult AcceptOwner(string ownerId)
        {
            try
            {
                var response = AdminService.AcceptOwner(ownerId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<List<string>>
                {
                    Data = null,
                    Message = "حدث خطأ اثناء قبل المالك",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return BadRequest(errorResponse);
            }
        }

        [HttpPut("DeclineOwner")]
        public ActionResult DeclineOwner(string ownerId)
        {
            try
            {
                var response = AdminService.DeclineOwner(ownerId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<List<string>>
                {
                    Data = null,
                    Message = "حدث خطأ اثناء رفض المالك",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return BadRequest(errorResponse);
            }
        }

        [HttpPut("BlockOwner")]
        public IActionResult BlockOwner(string ownerId)
        {
            try
            {
                var response = AdminService.BlockOwner(ownerId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<List<string>>
                {
                    Data = null,
                    Message = "حدث خطأ اثناء حظر المالك",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return BadRequest(errorResponse);
            }
        }

        [HttpPut("UnblockOwner")]
        public IActionResult UnblockOwner(string ownerId)
        {
            try
            {
                var response = AdminService.UnblockOwner(ownerId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<List<string>>
                {
                    Data = null,
                    Message = "حدث خطأ اثناء فك الحظر",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return BadRequest(errorResponse);
            }
        }


        [HttpPut("BlockCustomer")]
        public IActionResult BlockCustomer(string customerId)
        {
            try
            {
                var response = AdminService.BlockCustomer(customerId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<List<string>>
                {
                    Data = null,
                    Message = "حدث خطأ اثناء حظر العميل",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return BadRequest(errorResponse);
            }
        }

        [HttpPut("UnblockCustomer")]
        public IActionResult UnblockCustomer(string customerId)
        {
            try
            {
                var response = AdminService.UnblockCustomer(customerId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<List<string>>
                {
                    Data = null,
                    Message = "حدث خطأ اثناء فك الحظر عن العميل",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return BadRequest(errorResponse);
            }
        }
        [HttpPut("AcceptService")]
        public ActionResult AcceptService(int id)
        {
            try
            {
                var response = AdminService.AcceptService(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<List<string>>
                {
                    Data = null,
                    Message = "Error while accepting the service",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return BadRequest(errorResponse);
            }

        }

        [HttpPut("DeclineService")]
        public ActionResult DeclineService(int id)
        {
            try
            {
                var response = AdminService.DeclineService(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<List<string>>
                {
                    Data = null,
                    Message = "Error while declining the service",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return BadRequest(errorResponse);
            }

        }
    }
}