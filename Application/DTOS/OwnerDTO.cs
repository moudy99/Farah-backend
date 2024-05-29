using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS
{
    public class OwnerDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public int SSN { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public string? ProfileImage { get; set; }
        public bool IsBlocked { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public string YourFavirotePerson { get; set; }

        public string IDFrontImage { get; set; }
        public string IDBackImage { get; set; }

        public UserType UserType { get; set; }

        public OwnerAccountStatus AccountStatus { get; set; } = OwnerAccountStatus.Pending;
    }
}
