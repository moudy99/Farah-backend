using Application.Interfaces;
using Application.Services;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class FavoriteRepository : Repository<FavoriteService>, IFavoriteRepository
    {
        private readonly ApplicationDBContext context;

        public FavoriteRepository(ApplicationDBContext _context) : base(_context)
        {
            context = _context;
        }

    }
}
