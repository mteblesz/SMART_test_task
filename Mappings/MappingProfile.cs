using AutoMapper;
using TMAWarehouse.DTOs;
using TMAWarehouse.Models;

namespace TMAWarehouse.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AddItemDto, Item>();
        CreateMap<EditItemDto, Item>();
        CreateMap<EditItemDto, Item>().ReverseMap()
             .ForMember(dest => dest.PhotoBinary, opt => opt.MapFrom(
                 src => src.Photo != null ? src.Photo!.PhotoBinary : null
                 )); ;

        CreateMap<ItemGroupDto, ItemGroup>();
        CreateMap<MeasurementUnitDto, MeasurementUnit>();
        CreateMap<ItemStatusDto, ItemStatus>();
        CreateMap<ItemGroupDto, ItemGroup>().ReverseMap();
        CreateMap<MeasurementUnitDto, MeasurementUnit>().ReverseMap();
        CreateMap<ItemStatusDto, ItemStatus>().ReverseMap();

        CreateMap<Item, ItemDto>()
             .ForMember(dest => dest.ItemGroup, opt => opt.MapFrom(src => src.ItemGroup.GroupName))
             .ForMember(dest => dest.MeasurementUnit, opt => opt.MapFrom(src => src.MeasurementUnit.UnitName))
             .ForMember(dest => dest.ItemStatus, opt => opt.MapFrom(src => src.ItemStatus.StatusName))
             .ForMember(dest => dest.PhotoBinary, opt => opt.MapFrom(
                 src => src.Photo != null ? src.Photo!.PhotoBinary : null
                 ));

        CreateMap<Item, RequestItemDto>()
             .ForMember(dest => dest.MeasurementUnitName, opt => opt.MapFrom(src => src.MeasurementUnit.UnitName))
             .ForMember(dest => dest.MaxQuantity, opt => opt.MapFrom(src => src.Quantity))
             .ForMember(dest => dest.OrderQuantity, opt => opt.MapFrom(src => src.Quantity));
    }
}
