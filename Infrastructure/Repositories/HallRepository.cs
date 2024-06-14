using Application.Interfaces;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class HallRepository : Repository<Hall>, IHallRepository
    {
        private readonly ApplicationDBContext context;

        public HallRepository(ApplicationDBContext _context) : base(_context)
        {
            this.context = _context;
        }

        public List<Hall> GetAll()
        {
            return context.Halls
                .Where(c => c.IsDeleted == false)
                .Include(c => c.Pictures)
                .Include(c => c.Features)
                .ToList();
        }

        public Hall GetById(int id)
        {
            return context.Halls
                .Include(c => c.Pictures)
                .Include(c => c.Features)   
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
