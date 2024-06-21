using Application.Helpers;
using Application.Interfaces;
using Core.Entities;
using Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AdminRepository : Repository<Owner>, IAdminRepository
    {
        private readonly ApplicationDBContext context;

        public AdminRepository(ApplicationDBContext context) : base(context)
        {
            this.context = context;
        }
        public IQueryable<Owner> GetAllOwners()
        {

            return context.Owners
                .Include(o => o.Services);


        }
        public Owner GetOwnerById(string id)
        {
            return context.Owners
                .Include(o => o.Services)
                .FirstOrDefault(o => o.Id == id);

        }
        public List<Service> GetAllServices()
        {
            var beautyCenterServices = context.Services
                .OfType<BeautyCenter>()
                .Include(s => s.ServicesForBeautyCenter)
                .ToList();

            //var shopDressesServices = context.Services
            //    .OfType<ShopDresses>()
            //    .Include(s => s.Dresses)
            //    .ToList();

            var hallServices = context.Services
                .OfType<Hall>()
                .ToList();

            // Combine all service types
            var allServices = new List<Service>();
            allServices.AddRange(beautyCenterServices);
            //allServices.AddRange(shopDressesServices);
            allServices.AddRange(hallServices);

            return allServices;
        }
        public Service GetServiceById(int id)
        {
            return context.Services
                .FirstOrDefault(s => s.ID == id);
        }
        public IQueryable<Owner> GetOwnersByStatus(OwnerAccountStatus? status, bool? isBlocked)
        {
            var query = context.Owners.AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(o => o.AccountStatus == status.Value);
            }

            if (isBlocked.HasValue)
            {
                query = query.Where(o => o.IsBlocked == isBlocked.Value);
            }

            return query;
        }


        public List<ApplicationUser> SearchUsersByName(string name)
        {
            return context.Users
                           .Where(u => u.FirstName.Contains(name) || u.LastName.Contains(name))
                           .ToList();
        }
    }
}
