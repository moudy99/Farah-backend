using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Owner : ApplicationUser
    {

        public string IDFrontImage { get; set; }
        public string IDBackImage { get; set; }

        public UserType UserType { get; set; }

        public OwnerAccountStatus AccountStatus { get; set; } = OwnerAccountStatus.Pending;
    }
}
