using Core.Entities;

namespace Application.Interfaces
{
    public interface IPhotographyRepository : IRepository<Photography>
    {
        public List<Photography> GetOwnerServices(string ownerID);
    }
}
