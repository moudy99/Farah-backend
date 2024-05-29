using Application.DTOS;
using Application.Helpers;
using Application.Interfaces;
using AutoMapper;
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
        private readonly IMapper Mapper;

        public AdminService(IAdminRepository adminRepository, IMapper _mapper)
        {
            AdminRepository = adminRepository;
            Mapper = _mapper;
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

        public CustomResponseDTO<List<OwnerDTO>> GetAllOwners(int page, int pageSize)
        {
            List<Owner> AllOwners= AdminRepository.GetAllOwners();
            List<OwnerDTO> Owners = Mapper.Map<List<OwnerDTO>>(AllOwners);

            var paginatedList = PaginationHelper.Paginate(Owners, page, pageSize);
            var paginationInfo = PaginationHelper.GetPaginationInfo(paginatedList);

            var response = new CustomResponseDTO<List<OwnerDTO>>
            {
                Data = paginatedList.Items,
                Message = "Success",
                Succeeded = true,
                Errors = null,
                PaginationInfo = paginationInfo
            };

            return response;
            //mapping from ApplicationUser to DTO


        }


        public Owner GetById(string id)
        {
            return AdminRepository.GetById(id);
        }

        public void Update(Owner obj)
        {
            AdminRepository.Update(obj);
        }
        public void Insert(Owner obj)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }




        List<Owner> Iservices<Owner>.GetAll()
        {
            throw new NotImplementedException();
        }

        Owner Iservices<Owner>.GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
