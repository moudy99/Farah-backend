﻿using Application.Interfaces;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CarRepository : Repository<Car>, ICarRepository
    {
        private readonly ApplicationDBContext context;

        public CarRepository(ApplicationDBContext context) : base(context)
        {
            this.context = context;
        }

        public List<Car> GetAll()
        {
            return context.Cars
                .Include(c => c.Pictures)
                .ToList();
        }
    }
}