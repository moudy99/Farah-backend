using Application.Interfaces;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository AdminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            AdminRepository = adminRepository;
        }
        public void Delete(int id)
        {
            // any logic
            AdminRepository.Delete(id);
        }

        public List<ApplicationUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Owner> GetAllOwners()
        {
            List<Owner> list= AdminRepository.GetAllOwners();
            return list;
            //mapping from ApplicationUser to DTO

        }

        public ApplicationUser GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(ApplicationUser obj)
        {
            //mapping  from DTO to ApplicationUser


        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(ApplicationUser obj)
        {
            throw new NotImplementedException();
        }
    }
}
