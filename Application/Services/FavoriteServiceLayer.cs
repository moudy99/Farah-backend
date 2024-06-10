using Application.DTOS;
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
    public class FavoriteServiceLayer : IFavoriteServiceLayer
    {
        private readonly IFavoriteRepository favoriteRepository;
        private readonly IMapper _mapper;

        public FavoriteServiceLayer(IFavoriteRepository _favoriteRepository, IMapper mapper)
        {
            favoriteRepository = _favoriteRepository;
            _mapper = mapper;
        }

        public CustomResponseDTO<AllServicesDTO> GetAll(string CustomerID)
        {
            var favoriteServices = favoriteRepository.GetAllFavoritesForCustomer(CustomerID);

            var allServicesDTO = new AllServicesDTO
            {
                BeautyCenters = new List<BeautyCenterDTO>(),
                Halls = new List<HallDTO>(),
                Cars = new List<CarDTO>(),
                Photographys = new List<PhotographyDTO>(),
                ShopDresses = new List<ShopDressesDTo>()
            };

            foreach (var favorite in favoriteServices)
            {
                switch (favorite.Service)
                {
                    case BeautyCenter beautyCenter:
                        allServicesDTO.BeautyCenters.Add(_mapper.Map<BeautyCenterDTO>(beautyCenter));
                        break;
                    case Hall hall:
                        allServicesDTO.Halls.Add(_mapper.Map<HallDTO>(hall));
                        break;
                    case Car car:
                        allServicesDTO.Cars.Add(_mapper.Map<CarDTO>(car));
                        break;
                    case Photography photography:
                        allServicesDTO.Photographys.Add(_mapper.Map<PhotographyDTO>(photography));
                        break;
                    case ShopDresses shopDresses:
                        allServicesDTO.ShopDresses.Add(_mapper.Map<ShopDressesDTo>(shopDresses));
                        break;
                }
            }

            return new CustomResponseDTO<AllServicesDTO>()
            {
                Data = allServicesDTO,
                Message = "Success",
                Succeeded = true,
                Errors = null,
                PaginationInfo = null
            };
        }
    }
}
