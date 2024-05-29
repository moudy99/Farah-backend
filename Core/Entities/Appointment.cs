namespace Core.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string UserId { get; set; }
        public int BeautyCenterId { get; set; }
        public BeautyCenter BeautyCenter { get; set; }
        public List<ServiceForBeautyCenter> Services { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
