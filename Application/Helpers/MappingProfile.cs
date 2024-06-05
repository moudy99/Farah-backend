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
                .ForMember(dest => dest.Services,
                opt => opt.MapFrom(src => src.ServicesForBeautyCenter.
                Select(s => new ServiceForBeautyCenterDTO
                { Name = s.Name, Description = s.Description, Price = (decimal)s.Price, Appointment = (DateTime)s.Appointment }).ToList()))
                .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews))
                .ReverseMap();



            CreateMap<ServiceForBeautyCenter, ServiceForBeautyCenterDTO>().ReverseMap();

            CreateMap<ShopDresses, ShopDressesDTo>().ReverseMap();
            CreateMap<Dress, DressDto>().ReverseMap();

            CreateMap<Review, ReviewForBeautyCenterDTO>().ReverseMap();



            CreateMap<BeautyCenter, AddBeautyCenterDTO>()
               .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.ImagesBeautyCenter.Select(p => p.ImageUrl).ToList()))
               .ForMember(dest => dest.Images, opt => opt.Ignore()).ReverseMap();

            //  CreateMap<AddBeautyCenterDTO, BeautyCenter>()
            //.ForMember(dest => dest.ServicesForBeautyCenter, opt => opt.MapFrom(src => src.Services));

            CreateMap<OwnerRegisterDTO, Owner>();
            CreateMap<CustomerRegisterDTO, Customer>();
            CreateMap<Owner, OwnerDTO>().ReverseMap();
            CreateMap<ApplicationUser, ApplicationUserDTO>().ReverseMap();


            CreateMap<Governorate, GovernorateDTO>();
            CreateMap<GovernorateDTO, Governorate>();

            CreateMap<City, CityDTO>();
            CreateMap<CityDTO, City>();


            // Map Car to CarDTO, handling the Pictures and PictureUrls properties separately
            CreateMap<Car, CarDTO>()
                .ForMember(dest => dest.PictureUrls, opt => opt.MapFrom(src => src.Pictures.Select(p => p.Url).ToList()))
                .ForMember(dest => dest.Pictures, opt => opt.Ignore());

            // Map CarDTO to Car, converting PictureUrls to CarPicture entities
            CreateMap<CarDTO, Car>()
                .ForMember(dest => dest.Pictures, opt => opt.MapFrom(src => src.PictureUrls.Select(url => new CarPicture { Url = url }).ToList()));
        }
    }
}
