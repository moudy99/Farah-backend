
using Application.DTOS;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAdminService:Iservices<Owner>
    {
       public CustomResponseDTO<List<OwnerDTO>> GetAllOwners(int page, int pageSize);
        public CustomResponseDTO<object> BlockOwner(string  ownerId);
        public CustomResponseDTO<object> UnblockOwner(string ownerId);
        public CustomResponseDTO<object> AcceptOwner(string ownerId);
        public CustomResponseDTO<object> DeclineOwner(string ownerId);
    }
}
