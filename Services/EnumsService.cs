using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TMAWarehouse.DTOs;
using TMAWarehouse.Models;

namespace TMAWarehouse.Services;

public interface IEnumsService
{
    Task<ItemGroupDto> GetItemGroup(int id);
    Task<List<ItemGroupDto>> GetItemGroups();
    Task<ItemStatusDto> GetItemStatus(int id);
    Task<List<ItemStatusDto>> GetItemStatuses();
    Task<MeasurementUnitDto> GetMeasurementUnit(int id);
    Task<List<MeasurementUnitDto>> GetMeasurementUnits();
}

public class EnumsService : IEnumsService
{
    private readonly TmaDbContext _context;
    private readonly IMapper _mapper;

    public EnumsService(TmaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<MeasurementUnitDto>> GetMeasurementUnits()
    {
        var data = await _context.MeasurementUnits.ToListAsync();
        var result = _mapper.Map<List<MeasurementUnitDto>>(data);

        return result;
    }

    public async Task<MeasurementUnitDto> GetMeasurementUnit(int id)
    {
        var data = await _context.MeasurementUnits
            .FirstOrDefaultAsync(i => i.UnitId == id);
        var result = _mapper.Map<MeasurementUnitDto>(data);

        return result;
    }
    public async Task<List<ItemGroupDto>> GetItemGroups()
    {
        var data = await _context.ItemGroups.ToListAsync();
        var result = _mapper.Map<List<ItemGroupDto>>(data);

        return result;
    }

    public async Task<ItemGroupDto> GetItemGroup(int id)
    {
        var data = await _context.ItemGroups
            .FirstOrDefaultAsync(i => i.GroupId == id);
        var result = _mapper.Map<ItemGroupDto>(data);

        return result;
    }
    public async Task<List<ItemStatusDto>> GetItemStatuses()
    {
        var data = await _context.ItemStatuses.ToListAsync();
        var result = _mapper.Map<List<ItemStatusDto>>(data);

        return result;
    }

    public async Task<ItemStatusDto> GetItemStatus(int id)
    {
        var data = await _context.ItemStatuses
            .FirstOrDefaultAsync(i => i.StatusId == id);
        var result = _mapper.Map<ItemStatusDto>(data);

        return result;
    }
}
