
using Application.DTOS;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAdminService:Iservices<ApplicationUser>
    {
       public CustomResponseDTO<List<OwnerDTO>> GetAllOwners(int page, int pageSize);
    }
}
