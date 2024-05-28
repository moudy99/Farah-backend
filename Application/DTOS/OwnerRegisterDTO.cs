using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS
{
    internal class OwnerRegisterDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int SSN { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
        public string YourFavirotePerson { get; set; }
    }
}
