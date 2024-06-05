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
            CreateMap<ShopDresses, ShopDressesDTo>();
            CreateMap<BeautyCenter, BeautyCenterDTO>()
                .ForMember(dest => dest.Services, opt => opt.MapFrom(src => src.servicesForBeautyCenter.Select(s => new ServiceForBeautyCenterDTO { Name = s.Name, Description = s.Description, Price = (decimal)s.Price }).ToList()))
                .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews))
                .ReverseMap();

            CreateMap<ServiceForBeautyCenter, ServiceForBeautyCenterDTO>().ReverseMap();

            CreateMap<ShopDresses, ShopDressesDTo>().ReverseMap();
            CreateMap<Dress, DressDto>().ReverseMap();

            CreateMap<Review, ReviewForBeautyCenterDTO>().ReverseMap();
            CreateMap<BeautyCenter, AddBeautyCenterDTO>().ReverseMap();

            CreateMap<Appointment, AppointmentForBeautyCenterDTO>()
                .ForMember(dest => dest.Services, opt => opt.MapFrom(src => src.Services.Select(s => s.Name).ToList()))
                .ReverseMap();

            CreateMap<AppointmentForBeautyCenterDTO, Appointment>().ReverseMap();
            //.ForMember(dest => dest.Services, opt => opt.Ignore())
            //.AfterMap((dto, appointment) =>
            //{
            //    appointment.Services = dto.Services.Select(name => new ServiceForBeautyCenter { Name = name }).ToList();
            //});

            CreateMap<OwnerRegisterDTO, Owner>();
            CreateMap<CustomerRegisterDTO, Customer>();
            CreateMap<Owner, OwnerDTO>().ReverseMap();
            CreateMap<ApplicationUser, ApplicationUserDTO>().ReverseMap();


            CreateMap<Governorate, GovernorateDTO>();
            CreateMap<GovernorateDTO, Governorate>();

            CreateMap<City, CityDTO>();
            CreateMap<CityDTO, City>();


            CreateMap<Car, CarDTO>()
           .ForMember(dest => dest.PictureUrls, opt => opt.MapFrom(src => src.Pictures.Select(p => p.Url).ToList()));

            CreateMap<CarDTO, Car>()
                .ForMember(dest => dest.Pictures, opt => opt.MapFrom(src => src.PictureUrls.Select(url => new CarPicture { Url = url }).ToList()));
        }
    }
}
