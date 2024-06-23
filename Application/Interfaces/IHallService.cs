using Application.DTOS;
using Core.Entities;

namespace Application.Interfaces
{
    public interface IHallService : Iservices<Hall>
    {
        public CustomResponseDTO<List<HallDTO>> GetAllHalls(int page, int pageSize, string priceRange,int govId,int cityId);
        public Task<HallDTO> AddHall(AddHallDTO HallDto);
        public CustomResponseDTO<HallDTO> GetHallById(int id);
        public Task<HallDTO> EditHall(int id, HallDTO hallDto);
    }
}
