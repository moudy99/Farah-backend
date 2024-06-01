﻿namespace Application.DTOS
{
    public class DressDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsForRent { get; set; }
        public bool IsSaled { get; set; }
        public int ShopId { get; set; }
    }
}