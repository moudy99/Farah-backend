using Application.Interfaces;
using Core.Entities;
using Core.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AdminRepository: Repository<Owner>, IAdminRepository
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
    }
}
