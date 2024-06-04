﻿using Application.Helpers;

namespace Core.Entities
{
    public class BeautyCenter : Service
    {

        public string Name { get; set; }
        public string Description { get; set; }

        public int Gove { get; set; }

        public int City { get; set; }
        public List<ServiceForBeautyCenter> servicesForBeautyCenter { get; set; }
        public List<Review> Reviews { get; set; }


    }
}
