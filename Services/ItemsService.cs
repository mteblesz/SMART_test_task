using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TMAWarehouse.DTOs;
using TMAWarehouse.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace TMAWarehouse.Services;

public interface IItemsService
{
    Task AddItem([FromForm] AddItemDto dto);
    Task DeleteItem(int id);
    Task<EditItemDto> GetItemToEdit(int id);
    Task<AddRequestDto> GetItemToOrder(int id);
    Task<List<ItemDto>> GetItems();
    Task UpdateItem(EditItemDto dto);
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

    public async Task<EditItemDto> GetItemToEdit(int id)
    {
        var items = _context.Items
            .Include(i => i.ItemGroup)
            .Include(i => i.MeasurementUnit)
            .Include(i => i.ItemStatus)
            .Include(i => i.Photo)
            .FirstOrDefault(i => i.ItemId == id);
        var result = _mapper.Map<EditItemDto>(items);

        return result;
    }

    public async Task<AddRequestDto> GetItemToOrder(int id)
    {
        var items = _context.Items
            .Include(i => i.MeasurementUnit)
            .FirstOrDefault(i => i.ItemId == id);
        var result = _mapper.Map<AddRequestDto>(items);

        return result;
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

    public async Task UpdateItem(EditItemDto dto)
    {
        var item = await _context.Items.FindAsync(dto.ItemId);
        if (item == null)
            return;

        var updated = _mapper.Map<Item>(dto);

        var oldPhoto = await _context.Photos.FindAsync(item.PhotoId);
        if (oldPhoto != null)
        {
            oldPhoto.PhotoBinary = dto.PhotoBinary;
            await _context.SaveChangesAsync();
            updated.PhotoId = oldPhoto.PhotoId;
        }
        else
        {
            var photo = new Photo { PhotoBinary = dto.PhotoBinary };
            _context.Photos.Add(photo);
            await _context.SaveChangesAsync();
            updated.PhotoId = photo.PhotoId;
        }

        _context.Entry(item).CurrentValues.SetValues(updated); 
        await _context.SaveChangesAsync();
    }

    public async Task AddItem(AddItemDto dto)
    {
        var item = _mapper.Map<Item>(dto);
        if (dto.PhotoBinary != null)
        {
            var photo = new Photo { PhotoBinary = dto.PhotoBinary };
            _context.Photos.Add(photo);
            await _context.SaveChangesAsync();
            item.PhotoId = photo.PhotoId;

        }
        _context.Items.Add(item);
        await _context.SaveChangesAsync();
    }
}
