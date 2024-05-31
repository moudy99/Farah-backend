using Application.DTOS;
using Application.Helpers;
using Application.Interfaces;
using AutoMapper;
using Core.Entities;

namespace Application.Services
{
    public class ShopDressesService : IShopDressesService
    {
        private readonly IShopDressesRepository _shopRepository;
        private readonly IMapper _mapper;

        public ShopDressesService(IShopDressesRepository shopRepository, IMapper mapper)
        {
            _shopRepository = shopRepository;
            _mapper = mapper;
        }
        public CustomResponseDTO<ShopDressesDTo> AddShopDress(ShopDressesDTo ShopDressDto)
        {
            try
            {
                var ShopDress = _mapper.Map<ShopDresses>(ShopDressDto);
                _shopRepository.Insert(ShopDress);
                _shopRepository.Save();

                var dressDto = _mapper.Map<ShopDressesDTo>(ShopDress);
                var response = new CustomResponseDTO<ShopDressesDTo>
                {
                    Data = dressDto,
                    Message = "تم إضافة  محل الفساتين بنجاح",
                    Succeeded = true,
                    Errors = null,
                    PaginationInfo = null
                };

                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<ShopDressesDTo>
                {
                    Data = null,
                    Message = "حدث خطأ أثناء إضافة  محل الفساتين",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return errorResponse;
            }
        }



        public CustomResponseDTO<List<ShopDressesDTo>> GetAllShopDresses(int page, int pageSize)
        {
            var AllShopDresses = _shopRepository.GetAllShopDresses();

            var ShopDressesDTOs = _mapper.Map<List<ShopDressesDTo>>(AllShopDresses);

            var paginatedList = PaginationHelper.Paginate(ShopDressesDTOs, page, pageSize);
            var paginationInfo = PaginationHelper.GetPaginationInfo(paginatedList);

            var response = new CustomResponseDTO<List<ShopDressesDTo>>
            {
                Data = paginatedList.Items,
                Message = "عـــــــــاش  الله ينور",
                Succeeded = true,
                Errors = null,
                PaginationInfo = paginationInfo
            };
            return response;
        }

        public CustomResponseDTO<List<ShopDressesDTo>> GetShopDressesByName(string name)
        {
            var ShopDresses = _shopRepository.GetShopDressesByName(name);
            var shopDressesDto = _mapper.Map<List<ShopDressesDTo>>(ShopDresses);
            var response = new CustomResponseDTO<List<ShopDressesDTo>>
            {
                Data = shopDressesDto,
                Message = "عـــــــــاش  الله ينور",
                Succeeded = true,
                Errors = null,
                PaginationInfo = null
            };

            return response;
        }


        public CustomResponseDTO<ShopDressesDTo> GetShopDressesById(int id)
        {
            var shopDress = _shopRepository.GetById(id);
            var shopDressDTO = _mapper.Map<ShopDressesDTo>(shopDress);

            var response = new CustomResponseDTO<ShopDressesDTo>
            {
                Data = shopDressDTO,
                Message = "عـــــــــاش  الله ينور",
                Succeeded = true,
                Errors = null,
                PaginationInfo = null
            };

            return response;
        }


        public CustomResponseDTO<ShopDressesDTo> UpdateShopDress(ShopDressesDTo shopDressesDTo)
        {
            try
            {

                var shopDress = _mapper.Map<ShopDresses>(shopDressesDTo);


                _shopRepository.Update(shopDress);
                _shopRepository.Save();


                var resultDTO = _mapper.Map<ShopDressesDTo>(shopDress);


                var response = new CustomResponseDTO<ShopDressesDTo>
                {
                    Data = resultDTO,
                    Message = "تم تعديل   محل الفساتين بنجاح",
                    Succeeded = true,
                    Errors = null,
                    PaginationInfo = null
                };

                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<ShopDressesDTo>
                {
                    Data = null,
                    Message = "حدث خطأ أثناء تعديل البيوتي سنتر",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return errorResponse;
            }
        }

        public CustomResponseDTO<ShopDressesDTo> DeleteShopDressById(int id)
        {
            _shopRepository.Delete(id);
            _shopRepository.Save();
            try
            {
                var response = new CustomResponseDTO<ShopDressesDTo>
                {
                    Data = null,
                    Message = "تم حذف  محل الفساتين بنجاح",
                    Succeeded = true,
                    Errors = null,
                    PaginationInfo = null
                };

                return response;
            }
            catch (Exception ex)
            {
                var errorResponse = new CustomResponseDTO<ShopDressesDTo>
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
