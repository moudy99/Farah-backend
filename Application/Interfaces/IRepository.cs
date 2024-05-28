﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public List<T> GetAll();
        public T GetById(int id);
        public void Insert(T obj);
        public void Update(T obj);
        public void Delete(int id);
        public void Save();
    }
}
