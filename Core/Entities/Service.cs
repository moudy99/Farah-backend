﻿using Core.Entities;
using Core.Enums;

namespace Application.Helpers
{
    public class Service
    {
        public int ID { get; set; }

        public string OwnerID { get; set; }
        public Owner Owner { get; set; }

        public List<FavoriteService> FavoriteServices { get; set; }

        public ServiceStatus ServiceStatus { get; set; } = ServiceStatus.Accepted;

    }
}
