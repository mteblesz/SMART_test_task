using AutoMapper;
using TMAWarehouse.DTOs;
using TMAWarehouse.Models;

namespace TMAWarehouse.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddItemDto, Item>();
        CreateMap<Item, ItemDto>()
             .ForMember(dest => dest.ItemGroup, opt => opt.MapFrom(src => src.ItemGroup.GroupName))
             .ForMember(dest => dest.MeasurementUnit, opt => opt.MapFrom(src => src.MeasurementUnit.UnitName))
             .ForMember(dest => dest.ItemStatus, opt => opt.MapFrom(src => src.ItemStatus.StatusName))
             .ForMember(dest => dest.PhotoBinary, opt => opt.MapFrom(
                 src => src.Photo != null ? src.Photo!.PhotoBinary : null
                 ));
    }
}
