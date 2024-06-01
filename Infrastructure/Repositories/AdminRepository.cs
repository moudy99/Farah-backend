using Application.Interfaces;
using Core.Entities;
using Core.Enums;

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

        public List<ApplicationUser> SearchUsersByName(string name)
        {
            return context.Users
                           .Where(u => u.FirstName.Contains(name) || u.LastName.Contains(name))
                           .ToList();
        }
    }
}
