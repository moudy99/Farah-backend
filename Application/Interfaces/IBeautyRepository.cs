﻿using Core.Entities;

namespace Application.Interfaces
{
    public interface IBeautyRepository : IRepository<BeautyCenter>
    {
        public List<BeautyCenter>? GetBeautyCenterByName(string name);
        public List<BeautyCenter> GetAllBeautyCenters();

        public void InsertService(ServiceForBeautyCenter service);
        public List<BeautyCenter> GetOwnerServices(string ownerID);
    }
}
