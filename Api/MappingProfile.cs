using AutoMapper;
using TMAWarehouse.Api.DTOs;
using TMAWarehouse.Api.Models;

namespace TMAWarehouse.Api;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Item, ItemDto>()
             .ForMember(dest => dest.ItemGroup, opt => opt.MapFrom(src => src.ItemGroup.GroupName))
             .ForMember(dest => dest.MeasurementUnit, opt => opt.MapFrom(src => src.MeasurementUnit.UnitName))
             .ForMember(dest => dest.ItemStatus, opt => opt.MapFrom(src => src.ItemStatus.StatusName));
    }
}
