namespace Application.DTOS
{
    public class ShopDressesDTo
    {
        public string ShopName { get; set; }
        public string ShopDescription { get; set; }
        public string OwnerID { get; set; }
        public int GovernorateID { get; set; }
        public int City { get; set; }
        public DateTime OpeningHours { get; set; } // "Mon-Fri 10:00-18:00",
        public ICollection<DressDto> Dresses { get; set; }
    }
}
