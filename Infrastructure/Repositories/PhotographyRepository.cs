using Application.Interfaces;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PhotographyRepository : Repository<Photography>, IPhotographyRepository
    {
        private readonly ApplicationDBContext context;

        public PhotographyRepository(ApplicationDBContext context) : base(context)
        {
            this.context = context;
        }

        public List<Photography> GetAll()
        {
            return context.Photographies
                .Include(c => c.Images)
                .ToList();
        }
        public Photography GetById(int id)
        {
            return context.Photographies
                .Include(c => c.Images)
                .FirstOrDefault(c => c.ID == id);
        }

        public List<Photography> GetOwnerServices(string ownerID)
        {
            return context
                    .Photographies
                    .Where(c => c.OwnerID == ownerID)
                    .Include(c => c.Images)
                    .ToList();
        }
    }
}
