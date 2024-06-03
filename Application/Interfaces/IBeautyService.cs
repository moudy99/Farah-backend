using Application.DTOS;

namespace Application.Interfaces
{
    public interface IBeautyService
    {

        CustomResponseDTO<List<BeautyCenterDTO>> GetAllBeautyCenters(int page, int pageSize);

        CustomResponseDTO<List<BeautyCenterDTO>> GetBeautyCenterByName(string name);
        CustomResponseDTO<BeautyCenterDTO> GetBeautyCenterById(int id);
        CustomResponseDTO<AddBeautyCenterDTO> AddBeautyCenters(AddBeautyCenterDTO beautyCenterDTO);
        CustomResponseDTO<BeautyCenterDTO> UpdateBeautyCenter(BeautyCenterDTO beautyCenterDTO);

        CustomResponseDTO<BeautyCenterDTO> DeleteBeautyCenterById(int id);
    }
}
