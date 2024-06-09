using Application.DTOS;
using Application.Interfaces;
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

        public FavoriteServiceLayer(IFavoriteRepository _favoriteRepository)
        {
            favoriteRepository = _favoriteRepository;
        }

        //public List<CustomResponseDTO<FavoriteServiceDTO>> GetAll(int page = 1, int pageSize = 6, string CustomerID)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
