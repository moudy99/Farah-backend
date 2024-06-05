using Application.DTOS;
using AutoMapper;
using Core.Entities;

namespace Application.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerRegisterDTO>();

            CreateMap<BeautyCenter, BeautyCenterDTO>()
                .ForMember(dest => dest.Services,
                opt => opt.MapFrom(src => src.ServicesForBeautyCenter.
                Select(s => new ServiceForBeautyCenterDTO
                { Name = s.Name, Description = s.Description, Price = (decimal)s.Price, Appointment = (DateTime)s.Appointment }).ToList()))
                .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews))
                .ReverseMap();

            CreateMap<ServiceForBeautyCenter, ServiceForBeautyCenterDTO>().ReverseMap();

            //CreateMap<ImagesBeautyCenter, ImagesBeautyCenterDto>()
            // .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
            // .ReverseMap();


            CreateMap<ShopDresses, ShopDressesDTo>().ReverseMap();
            CreateMap<Dress, DressDto>().ReverseMap();

            CreateMap<Review, ReviewForBeautyCenterDTO>().ReverseMap();

            CreateMap<BeautyCenter, Add2>()
         .ForMember(dest => dest.Services, opt => opt.MapFrom(src => src.ServicesForBeautyCenter)).ReverseMap();



            CreateMap<OwnerRegisterDTO, Owner>();
            CreateMap<CustomerRegisterDTO, Customer>();
            CreateMap<Owner, OwnerDTO>().ReverseMap();
            CreateMap<ApplicationUser, ApplicationUserDTO>().ReverseMap();


            CreateMap<Governorate, GovernorateDTO>();
            CreateMap<GovernorateDTO, Governorate>();

            CreateMap<City, CityDTO>();
            CreateMap<CityDTO, City>();

        }
    }
}
