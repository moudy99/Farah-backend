using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS
{
    public class CustomerDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string SSN { get; set; }
        public int GovID { get; set; }
        public int CityID { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? ProfileImage { get; set; }
        public bool IsBlocked { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public string YourFavirotePerson { get; set; }
        public string IDFrontImage { get; set; }
        public string IDBackImage { get; set; }
    }
}
