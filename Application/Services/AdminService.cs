using Application.DTOS;
using Application.Helpers;
using Application.Interfaces;
using AutoMapper;
using Core.Entities;
using Core.Enums;
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

        public CustomResponseDTO<List<OwnerDTO>> GetAllOwners(int page, int pageSize)
        {
            List<Owner> AllOwners = AdminRepository.GetAllOwners();
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
        }

        public Owner GetById(string id)
        {
            return AdminRepository.GetById(id);
        }

        public void Update(Owner obj)
        {
            AdminRepository.Update(obj);
        }

        public void Save()
        {
            AdminRepository.Save();
        }

        public CustomResponseDTO<object> BlockOwner(string ownerId)
        {
            Owner owner = AdminRepository.GetById(ownerId);

            if (owner == null)
            {
                return new CustomResponseDTO<object>
                {
                    Data = null,
                    Message = "Owner not found",
                    Succeeded = false,
                    Errors = new List<string> { "Owner not found" }
                };
            }

            if (owner.IsBlocked)
            {
                return new CustomResponseDTO<object>
                {
                    Data = owner.UserName,
                    Message = "Owner is already blocked",
                    Succeeded = false,
                    Errors = null
                };
            }

            owner.IsBlocked = true;
            AdminRepository.Update(owner);
            AdminRepository.Save();

            return new CustomResponseDTO<object>
            {
                Data = owner.UserName,
                Message = "Owner is blocked",
                Succeeded = true,
                Errors = null
            };
        }

        public CustomResponseDTO<object> UnblockOwner(string ownerId)
        {
            Owner owner = AdminRepository.GetById(ownerId);

            if (owner == null)
            {
                return new CustomResponseDTO<object>
                {
                    Data = null,
                    Message = "Owner not found",
                    Succeeded = false,
                    Errors = new List<string> { "Owner not found" }
                };
            }

            if (!owner.IsBlocked)
            {
                return new CustomResponseDTO<object>
                {
                    Data = owner.UserName,
                    Message = "Owner is not blocked",
                    Succeeded = false,
                    Errors = null
                };
            }

            owner.IsBlocked = false;
            AdminRepository.Update(owner);
            AdminRepository.Save();

            return new CustomResponseDTO<object>
            {
                Data = owner.UserName,
                Message = "Owner is unblocked",
                Succeeded = true,
                Errors = null
            };
        }

        public CustomResponseDTO<object> AcceptOwner(string ownerId)
        {
            Owner owner = AdminRepository.GetById(ownerId);

            if (owner == null)
            {
                return new CustomResponseDTO<object>
                {
                    Data = null,
                    Message = "Owner not found",
                    Succeeded = false,
                    Errors = new List<string> { "Owner not found" }
                };
            }

            if (owner.AccountStatus == OwnerAccountStatus.Accepted)
            {
                return new CustomResponseDTO<object>
                {
                    Data = owner.AccountStatus,
                    Message = "Owner is already accepted",
                    Succeeded = false,
                    Errors = null
                };
            }

            owner.AccountStatus = OwnerAccountStatus.Accepted;
            AdminRepository.Update(owner);
            AdminRepository.Save();

            return new CustomResponseDTO<object>
            {
                Data = owner.AccountStatus,
                Message = "Owner accepted",
                Succeeded = true,
                Errors = null
            };
        }

        public CustomResponseDTO<object> DeclineOwner(string ownerId)
        {
            Owner owner = AdminRepository.GetById(ownerId);

            if (owner == null)
            {
                return new CustomResponseDTO<object>
                {
                    Data = null,
                    Message = "Owner not found",
                    Succeeded = false,
                    Errors = new List<string> { "Owner not found" }
                };
            }

            if (owner.AccountStatus == OwnerAccountStatus.Decline)
            {
                return new CustomResponseDTO<object>
                {
                    Data = owner.AccountStatus,
                    Message = "Owner is already declined",
                    Succeeded = false,
                    Errors = null
                };
            }

            owner.AccountStatus = OwnerAccountStatus.Decline;
            AdminRepository.Update(owner);
            AdminRepository.Save();

            return new CustomResponseDTO<object>
            {
                Data = owner.AccountStatus,
                Message = "Owner declined",
                Succeeded = true,
                Errors = null
            };
        }

        public List<ApplicationUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(Owner obj)
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
