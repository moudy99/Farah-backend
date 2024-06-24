using Application.Helpers;
using Core.Entities;
using Core.Enums;

namespace Application.Interfaces
{
    public interface IAdminRepository : IRepository<Owner> 
    {
        public List<Service> GetAllServices();
        public IQueryable<Owner> GetAllOwners();
        public Owner GetOwnerById(string id);
        public IQueryable<Owner> GetOwnersByStatus(OwnerAccountStatus? status, bool? isBlocked);
        public List<ApplicationUser> SearchUsersByName(string name);
        public Service GetServiceById(int id);
    }
}
