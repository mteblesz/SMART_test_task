using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TMAWarehouse.DTOs;
using TMAWarehouse.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace TMAWarehouse.Services;

public interface IItemsService
{
    Task<List<ItemDto>> GetItems();
    Task DeleteItem(int id);
}

public class ItemsService : IItemsService
{
    private readonly TmaDbContext _context;
    private readonly IMapper _mapper;

    public ItemsService(TmaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ItemDto>> GetItems()
    {
        var items = _context.Items
            .Include(i => i.ItemGroup)
            .Include(i => i.MeasurementUnit)
            .Include(i => i.ItemStatus)
            .Include(i => i.Photo);
        var result = _mapper.Map<List<ItemDto>>(items);

        return result;
    }

    public async void AddItem([FromForm] AddItemDto dto)
    {
        var item = _mapper.Map<Item>(dto);
        _context.Items.Add(item);
        await _context.SaveChangesAsync();
    }

    public async void UpdateItem(int id, AddItemDto updated)
    {
        var item = await _context.Items.FindAsync(id);
        if (item == null)
        {
            return;
        }

        _context.Entry(item).CurrentValues.SetValues(updated); // no mapping needed
        await _context.SaveChangesAsync();
    }

    public async Task DeleteItem(int id)
    {
        var item = await _context.Items.FindAsync(id);
        if (item == null)
        {
            return;
        }
        try
        {
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) 
        {
                
        }
    }
}
