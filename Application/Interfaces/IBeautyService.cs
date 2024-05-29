using Application.DTOS;

namespace Application.Interfaces
{
    public interface IBeautyService
    {
        // object AddBeautyCenters(BeautyCenterDTO beautyCenterDTO);
        CustomResponseDTO<List<BeautyCenterDTO>> GetAllBeautyCenters(int page, int pageSize);
        CustomResponseDTO<BeautyCenterDTO> AddBeautyCenters(BeautyCenterDTO beautyCenterDTO);
    }
}
