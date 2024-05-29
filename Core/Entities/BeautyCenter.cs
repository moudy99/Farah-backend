﻿using Application.Helpers;

namespace Core.Entities
{
    public class BeautyCenter : Service
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ServiceForBeautyCenter> Services { get; set; }
        public List<Review> Reviews { get; set; }

        public List<Appointment> Appointments { get; set; }

    }
}
