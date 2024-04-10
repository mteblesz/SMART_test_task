using AutoMapper;
using TMAWarehouse.DTOs;
using TMAWarehouse.Models;

namespace TMAWarehouse.Api;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddItemDto, Item>();
        CreateMap<Item, ItemDto>()
             .ForMember(dest => dest.ItemGroup, opt => opt.MapFrom(src => src.ItemGroup.GroupName))
             .ForMember(dest => dest.MeasurementUnit, opt => opt.MapFrom(src => src.MeasurementUnit.UnitName))
             .ForMember(dest => dest.ItemStatus, opt => opt.MapFrom(src => src.ItemStatus.StatusName));
    }
}
