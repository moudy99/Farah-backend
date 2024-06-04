using Application.Helpers;
using Core.Entities;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAdminRepository : IRepository<Owner>
    {
        public List<Owner>GetAllOwners();
        public List<Service>GetAllServices();

        List<Owner> GetOwnersByStatus(OwnerAccountStatus? status, bool? isBlocked);
        public List<ApplicationUser> SearchUsersByName(string name);
    }
}
