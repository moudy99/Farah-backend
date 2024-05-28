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
            CreateMap<Customer,CustomerRegisterDTO>();

        }
    }
}
