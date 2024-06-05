using Application.DTOS;
using Application.Helpers;
using Application.Interfaces;
using AutoMapper;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class BeautyService : IBeautyService
    {
        private readonly IBeautyRepository _beautyRepository;
        private readonly IMapper _mapper;

        public BeautyService(IBeautyRepository beautyRepository, IMapper mapper)
        {
            _beautyRepository = beautyRepository;
            _mapper = mapper;
        }
        public CustomResponseDTO<List<BeautyCenterDTO>> GetAllBeautyCenters(int page, int pageSize)
        {
            var beautyCenters = _beautyRepository.GetAllBeautyCenters();

            var beautyCenterDTOs = _mapper.Map<List<BeautyCenterDTO>>(beautyCenters);

            var paginatedList = PaginationHelper.Paginate(beautyCenterDTOs, page, pageSize);
            var paginationInfo = PaginationHelper.GetPaginationInfo(paginatedList);

            var response = new CustomResponseDTO<List<BeautyCenterDTO>>
            {
                Data = paginatedList.Items,
                Message = "عـــــــــاش  الله ينور",
                Succeeded = true,
                Errors = null,
                PaginationInfo = paginationInfo
            };
            return response;
        }



        public CustomResponseDTO<List<BeautyCenterDTO>> GetBeautyCenterByName(string name)
        {
            var beautyCenters = _beautyRepository.GetBeautyCenterByName(name);
            var beautyCenterDTOs = _mapper.Map<List<BeautyCenterDTO>>(beautyCenters);

            var response = new CustomResponseDTO<List<BeautyCenterDTO>>
            {
                Data = beautyCenterDTOs,
                Message = "عـــــــــاش  الله ينور",
                Succeeded = true,
                Errors = null,
                PaginationInfo = null
            };

            return response;
        }



        public CustomResponseDTO<BeautyCenterDTO> GetBeautyCenterById(int id)
        {
            var beautyCenter = _beautyRepository.GetById(id);
            var beautyCenterDTO = _mapper.Map<BeautyCenterDTO>(beautyCenter);

            var response = new CustomResponseDTO<BeautyCenterDTO>
            {
                Data = beautyCenterDTO,
                Message = "عـــــــــاش  الله ينور",
                Succeeded = true,
                Errors = null,
                PaginationInfo = null
            };

            return response;
        }

        public CustomResponseDTO<AddBeautyCenterDTO> AddBeautyCenter(AddBeautyCenterDTO beautyCenterDTO)
        {
            try
            {
                var imagePaths = ImageHelper.SaveImages(beautyCenterDTO.Images, "BeautyCenterImages");
                var beautyCenter = _mapper.Map<BeautyCenter>(beautyCenterDTO);
                beautyCenter.ImagesBeautyCenter = imagePaths.Select(path => new ImagesBeautyCenter { ImageUrl = path }).ToList();
                beautyCenterDTO.ImageUrls = imagePaths;

                _beautyRepository.Insert(beautyCenter);
                _beautyRepository.Save();

                var resultDTO = _mapper.Map<AddBeautyCenterDTO>(beautyCenter);

                var response = new CustomResponseDTO<AddBeautyCenterDTO>
                {
                    Data = resultDTO,
                    Message = "تم إضافة البيوتي سنتر بنجاح",
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

                var errorResponse = new CustomResponseDTO<AddBeautyCenterDTO>
                {
                    Data = null,
                    Message = "حدث خطأ أثناء إضافة البيوتي سنتر",
                    Succeeded = false,
                    Errors = new List<string> { innerExceptionMessage }
                };
                return errorResponse;
            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<AddBeautyCenterDTO>
                {
                    Data = null,
                    Message = "حدث خطأ أثناء إضافة البيوتي سنتر",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return errorResponse;
            }
        }



        public CustomResponseDTO<AddBeautyCenterDTO> UpdateBeautyCenter(AddBeautyCenterDTO beautyCenterDTO, int id)
        {
            try
            {
                var beautyCenter = _beautyRepository.GetById(id);
                if (beautyCenter == null)
                {
                    return new CustomResponseDTO<AddBeautyCenterDTO>
                    {
                        Data = null,
                        Message = "البيوتي سنتر غير موجود",
                        Succeeded = false,
                        Errors = new List<string> { "Beauty center not found" }
                    };
                }

                // Update the existing beautyCenter entity with new values from the DTO
                beautyCenter.Name = beautyCenterDTO.Name;
                beautyCenter.Description = beautyCenterDTO.Description;
                beautyCenter.Gove = beautyCenterDTO.Gove;
                beautyCenter.City = beautyCenterDTO.City;
                beautyCenter.ServicesForBeautyCenter = _mapper.Map<List<ServiceForBeautyCenter>>(beautyCenterDTO.Services);

                // Save new images and update image paths
                var imagePaths = ImageHelper.SaveImages(beautyCenterDTO.Images, "BeautyCenterImages");
                beautyCenter.ImagesBeautyCenter = imagePaths.Select(path => new ImagesBeautyCenter { ImageUrl = path }).ToList();
                beautyCenterDTO.ImageUrls = imagePaths;

                _beautyRepository.Update(beautyCenter);
                _beautyRepository.Save();

                // Map the updated entity back to the DTO
                var resultDTO = _mapper.Map<AddBeautyCenterDTO>(beautyCenter);

                var response = new CustomResponseDTO<AddBeautyCenterDTO>
                {
                    Data = resultDTO,
                    Message = "تم تعديل البيوتي سنتر بنجاح",
                    Succeeded = true,
                    Errors = null,
                    PaginationInfo = null
                };

                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<AddBeautyCenterDTO>
                {
                    Data = null,
                    Message = "حدث خطأ أثناء تعديل البيوتي سنتر",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return errorResponse;
            }
        }


        public CustomResponseDTO<BeautyCenterDTO> DeleteBeautyCenterById(int id)
        {
            try
            {
                _beautyRepository.Delete(id);
                _beautyRepository.Save();

                var response = new CustomResponseDTO<BeautyCenterDTO>
                {
                    Data = null,
                    Message = "تم حذف البيوتي سنتر بنجاح",
                    Succeeded = true,
                    Errors = null,
                    PaginationInfo = null
                };

                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<BeautyCenterDTO>
                {
                    Data = null,
                    Message = "حدث خطأ أثناء حذف البيوتي سنتر",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return errorResponse;
            }
        }


    }

}



