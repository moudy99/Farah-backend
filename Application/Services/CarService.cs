using Application.Interfaces;
using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CarService : ICarService
    {

        private readonly IMapper Mapper;
        private readonly ICarRepository carRepository;

        public CarService(IMapper mapper, ICarRepository _carRepository)
        {
            Mapper = mapper;
            carRepository = _carRepository;

        }
        public List<Car> GetAll()
        {
            return carRepository.GetAll();
        }


        public void Delete(int id)
        {
            throw new NotImplementedException();
        }



        public Car GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Car GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Car obj)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Car obj)
        {
            throw new NotImplementedException();
        }
    }
}
