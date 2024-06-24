
using Application.DTOS;
using Application.Helpers;
using Core.Entities;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAdminService
    {
        public AllServicesDTO GetAllServices();
        public CustomResponseDTO<object> GetServiceTypeByID(int id);
        public CustomResponseDTO<List<OwnerDTO>> GetAllOwners(int page, int pageSize);
        public CustomResponseDTO<List<CustomerDTO>> GetAllCustomers(bool? isBlocked, int page, int pageSize);
        public CustomResponseDTO<List<ApplicationUserDTO>> SearchUsersByName(string name);
        public CustomResponseDTO<List<OwnerDTO>> GetFilteredOwners(OwnerAccountStatus? status, bool? isBlocked, int page, int pageSize);
        public CustomResponseDTO<object> BlockOwner(string  ownerId);
        public CustomResponseDTO<object> UnblockOwner(string ownerId);
        public CustomResponseDTO<object> AcceptOwner(string ownerId);
        public CustomResponseDTO<object> DeclineOwner(string ownerId);
        public CustomResponseDTO<OwnerDTO> GetOwnerById(string ownerId);

        public CustomResponseDTO<object> AcceptService(int id);
        public CustomResponseDTO <object> DeclineService(int id);


        CustomResponseDTO<object> BlockCustomer(string customerId);
        CustomResponseDTO<object> UnblockCustomer(string customerId);

    }
}
