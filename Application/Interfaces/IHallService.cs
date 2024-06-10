using Application.DTOS;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IHallService : Iservices<Hall>
    {
        public CustomResponseDTO<List<HallDTO>>GetAllHalls(int page, int pageSize);
        public Task<HallDTO>AddHall(AddHallDTO HallDto);
        public CustomResponseDTO<HallDTO> GetHallById(int id);
        public Task<HallDTO> EditHall(int id, HallDTO hallDto);
    }
}
