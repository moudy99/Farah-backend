using Application.Interfaces;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Core.Enums;

namespace Infrastructure.Repositories
{
    public class HallRepository : Repository<Hall>, IHallRepository
    {
        private readonly ApplicationDBContext context;

        public HallRepository(ApplicationDBContext _context) : base(_context)
        {
            this.context = _context;
        }

        public IQueryable<Hall> GetAll()
        {
            return context.Halls
                .Where(c => c.IsDeleted == false)
                .Include(c => c.Pictures)
                .Include(c => c.Features)
                .Where(c => c.ServiceStatus == ServiceStatus.Accepted);
        }

        public Hall GetById(int id)
        {
            return context.Halls
                .Include(c => c.Pictures)
                .Include(c => c.Features)
                .Include(c => c.FavoriteServices)
                .FirstOrDefault(c => c.ID == id);
        }

        public List<Hall> GetOwnerServices(string ownerID)
        {
            return context
                    .Halls
                    .Where(c => c.OwnerID == ownerID)
                    .Include(c => c.Pictures)
                    .Where(c => c.IsDeleted == false).ToList();
        }
    }
}
