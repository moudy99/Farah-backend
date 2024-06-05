﻿namespace Application.DTOS
{
    public class Add2
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int Gove { get; set; }
        public string OwnerID { get; set; }
        public int City { get; set; }
        public List<string> Images { get; set; }
        public List<ServiceForBeautyCenterDTO> Services { get; set; }
    }
}
