namespace Application.DTOS
{
    public class ServiceForBeautyCenterDTO
    {
        public string Name { get; set; } // Makeup artist
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime Appointment { get; set; }

        public int BeautyCenterId { get; set; }
    }
}
