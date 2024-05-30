using Application.Interfaces;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        ApplicationDBContext context;
        public Repository(ApplicationDBContext _context)
        {
            context = _context;
        }
        public void Delete(int id)
        {
            T t = GetById(id);
            Update(t);
        }

        public List<T> GetAll()
        {

            return context.Set<T>().ToList();

        }

        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public T GetById(string id)
        {
            return context.Set<T>().Find(id);
        }

        public void Insert(T obj)
        {
            context.Add(obj);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(T obj)
        {
            context.Update(obj);
        }
    }
}
