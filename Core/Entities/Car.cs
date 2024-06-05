﻿using Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Car : Service
    {
        public int ID { get; set; }
        public string Brand {  get; set; }
        public int Year { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public int GovernorateID { get; set; }
        public int City { get; set; }
    }
}
