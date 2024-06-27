using Core.Entities;

namespace Application.Interfaces
{
    public interface IHallRepository : IRepository<Hall>
    {
        public List<Hall> GetOwnerServices(string ownerID);
        public new IQueryable<Hall> GetAll();
    }
}
