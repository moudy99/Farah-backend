﻿using Application.DTOS;
using Application.Helpers;
using Application.Interfaces;
using AutoMapper;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class PhotographyService : IPhotographyService
    {
        private readonly IMapper Mapper;
        private readonly IPhotographyRepository photoRepository;

        public PhotographyService(IMapper mapper, IPhotographyRepository _photoRepository)
        {
            Mapper = mapper;
            photoRepository = _photoRepository;

        }

        public CustomResponseDTO<List<PhotographyDTO>> GetAllPhotographer(int page, int pageSize)
        {
            var photographies = photoRepository.GetAll();

            var photographyDTOs = Mapper.Map<List<PhotographyDTO>>(photographies);

            var paginatedList = PaginationHelper.Paginate(photographyDTOs, page, pageSize);
            var paginationInfo = PaginationHelper.GetPaginationInfo(paginatedList);

            var response = new CustomResponseDTO<List<PhotographyDTO>>
            {
                Data = paginatedList.Items,
                Message = "عـــــــــاش  الله ينور",
                Succeeded = true,
                Errors = null,
                PaginationInfo = paginationInfo
            };
            return response;
        }
        public CustomResponseDTO<AddPhotographyDTO> AddPhotographer(AddPhotographyDTO PhotographerDto)
        {
            try
            {
                var imagePaths = ImageHelper.SaveImages(PhotographerDto.Pictures, "PhotographerImages");
                var photographer = Mapper.Map<Photography>(PhotographerDto);
                photographer.Images = imagePaths.Select(path => new Portfolio { ImageURL = path }).ToList();
                PhotographerDto.PictureUrls = imagePaths;

                photoRepository.Insert(photographer);
                photoRepository.Save();

                var resultDTO = Mapper.Map<AddPhotographyDTO>(photographer);

                var response = new CustomResponseDTO<AddPhotographyDTO>
                {
                    Data = resultDTO,
                    Message = "تم إضافة الفوتوجرافر بنجاح",
                    Succeeded = true,
                    Errors = null,
                    PaginationInfo = null
                };

                return response;
            }
            catch (DbUpdateException dbEx)
            {
                // Log inner exception details
                var innerExceptionMessage = dbEx.InnerException?.Message ?? dbEx.Message;

                var errorResponse = new CustomResponseDTO<AddPhotographyDTO>
                {
                    Data = null,
                    Message = "حدث خطأ أثناء إضافة الفوتوجرافر",
                    Succeeded = false,
                    Errors = new List<string> { innerExceptionMessage }
                };
                return errorResponse;
            }
        }

        public CustomResponseDTO<PhotographyDTO> GetPhotographerById(int id)
        {
            var photography = photoRepository.GetById(id);
            var photographyDTO = Mapper.Map<PhotographyDTO>(photography);

            var response = new CustomResponseDTO<PhotographyDTO>
            {
                Data = photographyDTO,
                Message = "عـــــــــاش  الله ينور",
                Succeeded = true,
                Errors = null,
                PaginationInfo = null
            };

            return response;
        }





        public CustomResponseDTO<AddPhotographyDTO> UpdatePhotographer(int id, AddPhotographyDTO PhotographerDto)
        {
            try
            {
                var photographer = photoRepository.GetById(id);
                if (photographer == null)
                {
                    return new CustomResponseDTO<AddPhotographyDTO>
                    {
                        Data = null,
                        Message = "Photographer not found",
                        Succeeded = false,
                        Errors = new List<string> { "Not found" }
                    };
                }

                // Update basic details
                photographer.Description = PhotographerDto.Description;

                // Save new images and update image paths
                var imagePaths = ImageHelper.SaveImages(PhotographerDto.Pictures, "PhotographerImages");
                photographer.Images = imagePaths.Select(path => new Portfolio { ImageURL = path }).ToList();
                PhotographerDto.PictureUrls = imagePaths;

                photoRepository.Update(photographer);
                photoRepository.Save();

                var resultDTO = Mapper.Map<AddPhotographyDTO>(photographer);

                var response = new CustomResponseDTO<AddPhotographyDTO>
                {
                    Data = resultDTO,
                    Message = "تم تحديث الفوتوجرافر بنجاح",
                    Succeeded = true,
                    Errors = null,
                    PaginationInfo = null
                };

                return response;
            }
            catch (DbUpdateException dbEx)
            {
                var innerExceptionMessage = dbEx.InnerException?.Message ?? dbEx.Message;

                var errorResponse = new CustomResponseDTO<AddPhotographyDTO>
                {
                    Data = null,
                    Message = "حدث خطأ أثناء تحديث الفوتوجرافر",
                    Succeeded = false,
                    Errors = new List<string> { innerExceptionMessage }
                };
                return errorResponse;
            }
        }

        public CustomResponseDTO<AddPhotographyDTO> DeletePhotographerById(int id)
        {
            try
            {
                photoRepository.Delete(id);
                photoRepository.Save();

                var response = new CustomResponseDTO<AddPhotographyDTO>
                {
                    Data = null,
                    Message = "تم حذف  الفوتوجرافر بنجاح",
                    Succeeded = true,
                    Errors = null,
                    PaginationInfo = null
                };

                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<AddPhotographyDTO>
                {
                    Data = null,
                    Message = "حدث خطأ أثناء حذف  الفوتوجرافر",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return errorResponse;
            }
        }


    }
}
