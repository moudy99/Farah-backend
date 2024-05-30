using Application.DTOS;

namespace Application.Interfaces
{
    public interface IBeautyService
    {
        // object AddBeautyCenters(BeautyCenterDTO beautyCenterDTO);
        CustomResponseDTO<List<BeautyCenterDTO>> GetAllBeautyCenters(int page, int pageSize);

        CustomResponseDTO<List<BeautyCenterDTO>> GetBeautyCenterByName(string name);
        CustomResponseDTO<AddBeautyCenterDTO> AddBeautyCenters(AddBeautyCenterDTO beautyCenterDTO);
        CustomResponseDTO<BeautyCenterDTO> UpdateBeautyCenter(BeautyCenterDTO beautyCenterDTO);

        CustomResponseDTO<BeautyCenterDTO> DeleteBeautyCenterById(int id);
    }
}
