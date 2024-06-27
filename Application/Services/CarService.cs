using Application.DTOS;
using Application.Helpers;
using Application.Interfaces;
using AutoMapper;
using Core.Entities;

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

        public CustomResponseDTO<List<CarDTO>> GetAllCars(int page, int pageSize, string priceRange, int govId, int city)
        {
            var allCars = carRepository.GetAll();
            if (!string.IsNullOrEmpty(priceRange) && priceRange != "all")
            {
                int minPrice;
                int maxPrice;

                if (priceRange.StartsWith("<"))
                {
                    maxPrice = int.Parse(priceRange.Substring(1));
                    allCars = allCars.Where(h => h.Price < maxPrice);
                }
                else if (priceRange.StartsWith(">"))
                {
                    minPrice = int.Parse(priceRange.Substring(1));
                    allCars = allCars.Where(h => h.Price > minPrice);
                }
                else
                {
                    var priceRanges = priceRange.Split('-').Select(int.Parse).ToArray();
                    if (priceRanges.Length == 2)
                    {
                        minPrice = priceRanges[0];
                        maxPrice = priceRanges[1];
                        allCars = allCars.Where(h => h.Price >= minPrice && h.Price <= maxPrice);
                    }
                }
            }
            if (govId > 0)
            {
                allCars = allCars.Where(p => p.GovernorateID == govId);
            }

            if (city > 0)
            {
                allCars = allCars.Where(p => p.City == city);
            }

            var paginatedList = PaginationHelper.Paginate(allCars, page, pageSize);
            if (!paginatedList.Items.Any())
            {
                return new CustomResponseDTO<List<CarDTO>>
                {
                    Data = new List<CarDTO>(),
                    Message = "لا يوجد سيارات",
                    Succeeded = false,
                    Errors = new List<string> { "لا يوجد سيارات" },
                    PaginationInfo = null
                };
            }


            var paginationInfo = PaginationHelper.GetPaginationInfo(paginatedList);
            var cars = Mapper.Map<List<CarDTO>>(paginatedList.Items);

            return new CustomResponseDTO<List<CarDTO>>
            {
                Data = cars,
                Message = "تم",
                Succeeded = true,
                Errors = null,
                PaginationInfo = paginationInfo
            };

        }

        public async Task<CarDTO> AddCar(AddCarDTO carDto)
        {
            var car = Mapper.Map<Car>(carDto);

            var imagePaths = await ImageSavingHelper.SaveImagesAsync(carDto.Pictures, "Cars");
            car.Pictures = imagePaths.Select(path => new CarPicture { Url = path }).ToList();
            carDto.PictureUrls = imagePaths;

            carRepository.Insert(car);
            carRepository.Save();

            return Mapper.Map<CarDTO>(car);
        }

        public async Task<CarDTO> EditCar(int id, CarDTO carDto)
        {
            var existingCar = carRepository.GetById(id);
            if (existingCar == null)
            {
                throw new Exception("تعذر العثور علي السياره");
            }

            Mapper.Map(carDto, existingCar);

            if (carDto.Pictures != null && carDto.Pictures.Any())
            {
                var imagePaths = await ImageSavingHelper.SaveImagesAsync(carDto.Pictures, "Cars");
                existingCar.Pictures = imagePaths.Select(path => new CarPicture { Url = path }).ToList();
                carDto.PictureUrls = imagePaths;
            }

            carRepository.Update(existingCar);
            carRepository.Save();
            return Mapper.Map<CarDTO>(existingCar);
        }
        public void Delete(int id)
        {
            try
            {
                Car car = carRepository.GetById(id);
                if (car == null)
                {
                    throw new Exception("تعذر العثور علي السياره");
                }
                car.IsDeleted = true;

                carRepository.Update(car);
                carRepository.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CustomResponseDTO<CarDTO> GetCarById(int id)
        {
            Car car = carRepository.GetById(id);

            if (car == null)
            {
                return new CustomResponseDTO<CarDTO>()
                {
                    Data = null,
                    Message = "تعذر العثور علي السياره",
                    Succeeded = false,
                    Errors = null,
                };
            }

            CarDTO carDTO = Mapper.Map<CarDTO>(car);
            return new CustomResponseDTO<CarDTO>
            {
                Data = carDTO,
                Message = "تم",
                Succeeded = true,
                Errors = null
            };
        }

    }
}
