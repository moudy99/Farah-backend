using Application.DTOS;
using Application.Helpers;
using Application.Interfaces;
using AutoMapper;
using Core.Entities;

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
                    throw new Exception("تعذر العثور علي القاعه");
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

        public CustomResponseDTO<List<HallDTO>> GetAllHalls(int page, int pageSize, string priceRange, int govId, int cityId)
        {
            var query = HallRepository.GetAll();

            // Apply price range filter
            if (!string.IsNullOrEmpty(priceRange) && priceRange != "all")
            {
                int minPrice;
                int maxPrice;

                if (priceRange.StartsWith("<"))
                {
                    maxPrice = int.Parse(priceRange.Substring(1));
                    query = query.Where(h => h.Price < maxPrice);
                }
                else if (priceRange.StartsWith(">"))
                {
                    minPrice = int.Parse(priceRange.Substring(1));
                    query = query.Where(h => h.Price > minPrice);
                }
                else
                {
                    var priceRanges = priceRange.Split('-').Select(int.Parse).ToArray();
                    if (priceRanges.Length == 2)
                    {
                        minPrice = priceRanges[0];
                        maxPrice = priceRanges[1];
                        query = query.Where(h => h.Price >= minPrice && h.Price <= maxPrice);
                    }
                }
            }
            if (govId > 0)
            {
                query = query.Where(p => p.GovernorateID == govId);
            }

            if (cityId > 0)
            {
                query = query.Where(p => p.City == cityId);
            }
            // Check if any halls match the filters
            var paginatedList = PaginationHelper.Paginate(query, page, pageSize);

            if (!paginatedList.Items.Any())
            {
                return new CustomResponseDTO<List<HallDTO>>
                {
                    Data = null,
                    Message = "لا يوجد قاعات",
                    Succeeded = false,
                    Errors = new List<string> { "لا يوجد قاعات" },
                    PaginationInfo = null
                };
            }

            var HallsDTO = Mapper.Map<List<HallDTO>>(paginatedList.Items);

            var paginationInfo = PaginationHelper.GetPaginationInfo(paginatedList);

            return new CustomResponseDTO<List<HallDTO>>
            {
                Data = HallsDTO,
                Message = "تم",
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
                throw new Exception("تعذر العثور علي القاعه");
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
                    Message = "تعذر العثور علي القاعه",
                    Succeeded = false,
                    Errors = null,
                };
            }

            HallDTO hallDTO = Mapper.Map<HallDTO>(hall);
            return new CustomResponseDTO<HallDTO>
            {
                Data = hallDTO,
                Message = "تم",
                Succeeded = true,
                Errors = null
            };
        }

    }
}
