namespace Application.DTOS
{
    public class AddBeautyCenterDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int Gove { get; set; }
        public string OwnerID { get; set; }
        public int City { get; set; }
        public List<ServiceForBeautyCenterDTO> Services { get; set; }

        public DateTime Appointment {  get; set; }
    }
}
