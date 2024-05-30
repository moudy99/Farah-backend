namespace Application.DTOS
{
    public class AddBeautyCenterDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ServiceForBeautyCenterDTO> Services { get; set; }


        public List<AppointmentForBeautyCenterDTO> Appointments { get; set; }
    }
}
