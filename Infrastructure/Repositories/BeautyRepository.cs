using Application.Interfaces;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BeautyRepository : Repository<BeautyCenter>, IBeautyRepository
    {
        ApplicationDBContext context;
        public BeautyRepository(ApplicationDBContext _context) : base(_context)
        {
            context = _context;
        }

        public List<BeautyCenter> GetAllBeautyCenters()
        {
            return context.BeautyCenters
                          .Include(b => b.Services)
                          .Include(b => b.Appointments)
                          .Include(b => b.Reviews)
                          .ToList();
        }


        public List<BeautyCenter>? GetBeautyCenterByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
