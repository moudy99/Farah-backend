﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Customer : ApplicationUser
    {
        public List<FavoriteService> FavoriteServices { get; set; }
    }
}
