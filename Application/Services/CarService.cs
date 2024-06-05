using Application.DTOS;
using Application.Helpers;
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

        public CustomResponseDTO<List<CarDTO>> GetAllCars(int page, int pageSize)
        {
            var allCars = carRepository.GetAll();
            if (!allCars.Any())
            {
                return new CustomResponseDTO<List<CarDTO>>
                {
                    Data = new List<CarDTO>(),
                    Message = "No cars found",
                    Succeeded = false,
                    Errors = new List<string> { "No data" },
                    PaginationInfo = null
                };
            }

            var cars = Mapper.Map<List<CarDTO>>(allCars);

            var paginatedList = PaginationHelper.Paginate(cars, page, pageSize);
            var paginationInfo = PaginationHelper.GetPaginationInfo(paginatedList);

            return new CustomResponseDTO<List<CarDTO>>
            {
                Data = paginatedList.Items,
                Message = "Success",
                Succeeded = true,
                Errors = null,
                PaginationInfo = paginationInfo
            };

        }

        public CarDTO AddCar(CarDTO carDto)
        {
            var car = Mapper.Map<Car>(carDto);

            var imagePaths = ImageHelper.SaveImages(carDto.Pictures, "Cars");
            car.Pictures = imagePaths.Select(path => new CarPicture { Url = path }).ToList();
            carDto.PictureUrls = imagePaths;

            carRepository.Insert(car);
            carRepository.Save();

            return Mapper.Map<CarDTO>(car);
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

        public List<Car> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
