using Application.Interfaces;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BeautyRepository : Repository<BeautyCenter>, IBeautyRepository
    {
        private readonly ApplicationDBContext context;

        public BeautyRepository(ApplicationDBContext _context) : base(_context)
        {
            context = _context;
        }

        public List<BeautyCenter> GetAllBeautyCenters()
        {
            return context.BeautyCenters
                          .Include(b => b.ImagesBeautyCenter)
                          .Include(b => b.ServicesForBeautyCenter)
                          .Include(b => b.Reviews)
                          .ToList();
        }



        public List<BeautyCenter> GetBeautyCenterByName(string name)
        {
            return context.BeautyCenters
                          .Where(b => b.Name.Contains(name))
                          .Include(b => b.ServicesForBeautyCenter)
                          .Include(b => b.ImagesBeautyCenter)
                          .Include(b => b.Reviews)
                          .ToList();
        }

        public BeautyCenter GetBeautyCenterById(int id)
        {
            return context.BeautyCenters
                .Include(b => b.ImagesBeautyCenter)
                .Include(b => b.ServicesForBeautyCenter)
                .Include(b => b.Reviews)
                .SingleOrDefault(b => b.ID == id);
        }

        public List<BeautyCenter> GetOwnerServices(string ownerID)
        {
            return context
                    .BeautyCenters
                    .Where(c => c.OwnerID == ownerID)
                    .Include(c => c.Reviews)
                    .ToList();
        }
    }
}
