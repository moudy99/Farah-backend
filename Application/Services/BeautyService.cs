using Application.DTOS;
using Application.Helpers;
using Application.Interfaces;
using AutoMapper;
using Core.Entities;

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
            List<BeautyCenter> beautyCenters = _beautyRepository.GetAllBeautyCenters();
            var paginatedList = PaginationHelper.Paginate(beautyCenters, page, pageSize);
            var paginationInfo = PaginationHelper.GetPaginationInfo(paginatedList);

            var beautyCenterDTOs = _mapper.Map<List<BeautyCenterDTO>>(paginatedList.Items);

            var response = new CustomResponseDTO<List<BeautyCenterDTO>>
            {
                Data = beautyCenterDTOs,
                Message = "عـــــــــاش  الله ينور",
                Succeeded = true,
                Errors = null,
                PaginationInfo = paginationInfo
            };
            return response;
        }
        public CustomResponseDTO<BeautyCenterDTO> AddBeautyCenters(BeautyCenterDTO beautyCenterDTO)
        {
            try
            {

                var beautyCenter = _mapper.Map<BeautyCenter>(beautyCenterDTO);


                _beautyRepository.Insert(beautyCenter);
                _beautyRepository.Save();


                var resultDTO = _mapper.Map<BeautyCenterDTO>(beautyCenter);


                var response = new CustomResponseDTO<BeautyCenterDTO>
                {
                    Data = resultDTO,
                    Message = "تم إضافة البيوتي سنتر بنجاح",
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
                    Message = "حدث خطأ أثناء إضافة البيوتي سنتر",
                    Succeeded = false,
                    Errors = new List<string> { ex.Message }
                };
                return errorResponse;
            }
        }
    }

}



