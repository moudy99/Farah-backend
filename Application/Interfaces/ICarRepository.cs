using Core.Entities;

namespace Application.Interfaces
{
    public interface ICarRepository : IRepository<Car>
    {
        public List<Car> GetOwnerServices(string ownerID);
        public IQueryable<Car> GetAll();
    }
}
