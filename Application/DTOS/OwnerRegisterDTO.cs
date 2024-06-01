using Core.Enums;

namespace Application.DTOS
{
    public class OwnerRegisterDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int SSN { get; set; }

        public string Password { get; set; }
        public UserType UserType { get; set; }

        public int GovID { get; set; }
        public int CityID { get; set; }

        public string IdFrontImage { get; set; }
        public string IdBackImage { get; set; }

        public string YourFavirotePerson { get; set; }
    }
}
