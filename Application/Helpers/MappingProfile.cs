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
                .ForMember(dest => dest.Services, opt => opt.MapFrom(src => src.Services.Select(s => new ServiceForBeautyCenterDTO { Name = s.Name, Description = s.Description, Price = (decimal)s.Price }).ToList()))
                .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews))
                .ForMember(dest => dest.Appointments, opt => opt.MapFrom(src => src.Appointments))
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

            CreateMap<Customer, CustomerRegisterDTO>().ReverseMap();
            CreateMap<OwnerRegisterDTO, Owner>();
            CreateMap<Owner, OwnerDTO>().ReverseMap();

        }
    }
}
