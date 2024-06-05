using Application.DTOS;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICarService : Iservices<Car>
    {
        public CustomResponseDTO<List<CarDTO>> GetAllCars(int page, int pageSize);
        public CarDTO AddCar(CarDTO carDto);
    }
}
