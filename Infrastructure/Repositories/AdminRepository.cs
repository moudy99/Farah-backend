using Application.DTOS;
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
        public List<Owner> GetAllOwners()
        {

            return context.Owners
                    .ToList();

        }
        public List<Owner> GetOwnersByStatus(OwnerAccountStatus? status, bool? isBlocked)
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

            return query.ToList();
        }

        public List<Service> GetAllServices()
        {

            var hallServices = context.Halls.ToList<Service>();


            var beautyCenterServices = context.BeautyCenters.ToList<Service>();
            var ShopServices = context.ShopDresses.ToList<Service>();


            var allServices = hallServices
                .Concat(ShopServices)
                .Concat(beautyCenterServices)
                .ToList();

            return allServices;
        }

        public List<ApplicationUser> SearchUsersByName(string name)
        {
            return context.Users
                           .Where(u => u.FirstName.Contains(name) || u.LastName.Contains(name))
                           .ToList();
        }
    }
}
