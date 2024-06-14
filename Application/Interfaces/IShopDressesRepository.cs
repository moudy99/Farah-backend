﻿using Core.Entities;

namespace Application.Interfaces
{
    public interface IShopDressesRepository : IRepository<ShopDresses>
    {
        public List<ShopDresses>? GetShopDressesByName(string name);
        public List<ShopDresses> GetAllShopDresses();
        public List<ShopDresses> GetOwnerServices(string ownerID);
    }
}
