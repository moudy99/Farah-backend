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
    public class HallService : IHallService
    {
        private readonly IMapper Mapper;
        private readonly IHallRepository HallRepository;
        public HallService(IMapper _Mapper, IHallRepository _HallRepository) 
        {
            HallRepository = _HallRepository;
            Mapper = _Mapper;
        }
        public void Delete(int id)
        {
            try
            {
                Hall car = HallRepository.GetById(id);
                if (car == null)
                {
                    throw new Exception("Can't Find A Hall With This ID");
                }
                car.IsDeleted = true;

                HallRepository.Update(car);
                HallRepository.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Hall> GetAll()
        {
            throw new NotImplementedException();
        }

        public CustomResponseDTO<List<HallDTO>> GetAllHalls(int page, int pageSize)
        {
            List<Hall> Halls = HallRepository.GetAll();

            if (!Halls.Any())
            {
                return new CustomResponseDTO<List<HallDTO>>
                {
                    Data = new List<HallDTO>(),
                    Message = "No Halls found",
                    Succeeded = false,
                    Errors = new List<string> { "No data" },
                    PaginationInfo = null
                };
            }

            var HallsDTO = Mapper.Map<List<HallDTO>>(Halls);

            var paginatedList = PaginationHelper.Paginate(HallsDTO, page, pageSize);
            var paginationInfo = PaginationHelper.GetPaginationInfo(paginatedList);

            return new CustomResponseDTO<List<HallDTO>>
            {
                Data = paginatedList.Items,
                Message = "Success",
                Succeeded = true,
                Errors = null,
                PaginationInfo = paginationInfo
            };

        }
        public async Task<HallDTO> AddHall(AddHallDTO HallDto)
        {
            var hall = Mapper.Map<Hall>(HallDto);

            var imagePaths = await ImageSavingHelper.SaveImagesAsync(HallDto.Pictures, "Halls");
            hall.Pictures = imagePaths.Select(path => new HallPicture { Url = path }).ToList();
            HallDto.PictureUrls = imagePaths;

            HallRepository.Insert(hall);
            HallRepository.Save();

            return Mapper.Map<HallDTO>(hall);
        }

        public async Task<HallDTO> EditHall(int id, HallDTO hallDto)
        {
            var existingHall = HallRepository.GetById(id);
            if (existingHall == null)
            {
                throw new Exception("Hall not found");
            }

            Mapper.Map(hallDto, existingHall);

            if (hallDto.Pictures != null && hallDto.Pictures.Any())
            {
                var imagePaths = await ImageSavingHelper.SaveImagesAsync(hallDto.Pictures, "Halls");
                existingHall.Pictures = imagePaths.Select(path => new HallPicture { Url = path }).ToList();
                hallDto.PictureUrls = imagePaths;
            }

            HallRepository.Update(existingHall);
            HallRepository.Save();
            return Mapper.Map<HallDTO>(existingHall);
        }

        public CustomResponseDTO<HallDTO> GetHallById(int id)
        {
            Hall hall = HallRepository.GetById(id);

            if (hall == null)
            {
                return new CustomResponseDTO<HallDTO>()
                {
                    Data = null,
                    Message = "Cant Find A Hall With This ID",
                    Succeeded = false,
                    Errors = null,
                };
            }

            HallDTO hallDTO = Mapper.Map<HallDTO>(hall);
            return new CustomResponseDTO<HallDTO>
            {
                Data = hallDTO,
                Message = "Success",
                Succeeded = true,
                Errors = null
            };
        }


        public void Insert(Hall obj)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Hall obj)
        {
            throw new NotImplementedException();
        }

        List<Hall> Iservices<Hall>.GetAll()
        {
            throw new NotImplementedException();
        }

        Hall Iservices<Hall>.GetById(int id)
        {
            throw new NotImplementedException();
        }

        Hall Iservices<Hall>.GetById(string id)
        {
            throw new NotImplementedException();
        }

        void Iservices<Hall>.Insert(Hall obj)
        {
            throw new NotImplementedException();
        }

        void Iservices<Hall>.Update(Hall obj)
        {
            throw new NotImplementedException();
        }


        void Iservices<Hall>.Save()
        {
            throw new NotImplementedException();
        }
    }
}
