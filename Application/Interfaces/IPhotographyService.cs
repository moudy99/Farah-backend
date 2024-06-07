using Application.DTOS;

namespace Application.Interfaces
{
    public interface IPhotographyService
    {
        CustomResponseDTO<List<PhotographyDTO>> GetAllPhotographer(int page, int pageSize);


        CustomResponseDTO<PhotographyDTO> GetPhotographerById(int id);
        CustomResponseDTO<AddPhotographyDTO> AddPhotographer(AddPhotographyDTO PhotographerDTO);
        CustomResponseDTO<AddPhotographyDTO> UpdatePhotographer(int id, AddPhotographyDTO PhotographerDto);

        CustomResponseDTO<AddPhotographyDTO> DeletePhotographerById(int id);
    }
}
