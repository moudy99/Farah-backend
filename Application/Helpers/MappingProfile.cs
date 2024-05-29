using Application.DTOS;
using AutoMapper;
using Core.Entities;

namespace Application.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<TSource, TDestination>()
            CreateMap<Customer, CustomerRegisterDTO>();

            CreateMap<BeautyCenter, BeautyCenterDTO>()
                .ForMember(dest => dest.Services, opt => opt.MapFrom(src => src.Services))
                .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews))
                .ForMember(dest => dest.Appointments, opt => opt.MapFrom(src => src.Appointments)).ReverseMap();

            //CreateMap<BeautyCenterDTO, BeautyCenter>()
            //    .ForMember(dest => dest.Services, opt => opt.MapFrom(src => src.Services))
            //    .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.Reviews))
            //    .ForMember(dest => dest.Appointments, opt => opt.MapFrom(src => src.Appointments));

            CreateMap<ServiceForBeautyCenter, ServiceForBeautyCenterDTO>().ReverseMap();
            // CreateMap<ServiceForBeautyCenterDTO, ServiceForBeautyCenter>();

            CreateMap<Review, ReviewForBeautyCenterDTO>().ReverseMap();
            // CreateMap<ReviewForBeautyCenterDTO, Review>();

            CreateMap<Appointment, AppointmentForBeautyCenterDTO>()
                            .ForMember(dest => dest.Services, opt => opt.MapFrom(src => src.Services.Select(s => s.Name)));

            CreateMap<AppointmentForBeautyCenterDTO, Appointment>()
                .ForMember(dest => dest.Services, opt => opt.Ignore())
                .AfterMap((dto, appointment) =>
                {

                    appointment.Services = dto.Services.Select(name => new ServiceForBeautyCenter { Name = name }).ToList();
                });

        }
    }
}
