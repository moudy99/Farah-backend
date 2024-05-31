using Application.Interfaces;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ShopDressesRepository : Repository<ShopDresses>, IShopDressesRepository
    {

        ApplicationDBContext context;
        public ShopDressesRepository(ApplicationDBContext _context) : base(_context)
        {
            context = _context;
        }
        public List<ShopDresses> GetAllShopDresses()
        {
            return context.ShopDresses
                          .Include(b => b.Dresses)
                          .ToList();
        }
        public List<ShopDresses>? GetShopDressesByName(string name)
        {
            return context.ShopDresses
                         .Where(b => b.ShopName.Contains(name))
                         .Include(b => b.Dresses)
                         .ToList();
        }

    }
}
