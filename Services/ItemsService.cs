using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TMAWarehouse.DTOs;
using TMAWarehouse.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace TMAWarehouse.Services;

public interface IItemsService
{
    Task<EditItemDto> GetItem(int id);
    Task<List<ItemDto>> GetItems();
    Task AddItem([FromForm] AddItemDto dto);
    Task DeleteItem(int id);
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

    public async Task<EditItemDto> GetItem(int id)
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

    public async Task DeleteItem(int id)
    {
        var item = await _context.Items
            .Include(i => i.Photo)
            .FirstOrDefaultAsync(i => i.ItemId == id)
            ?? throw new ArgumentException("Item with this id does not exist.");
        try
        {
            var photo = item.Photo;
            _context.Items.Remove(item);
            if (photo != null)
                _context.Photos.Remove(photo);

            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new ArgumentException("Dabase error occured.");
        }
    }

    public async Task UpdateItem(EditItemDto dto)
    {
        var item = await _context.Items.FindAsync(dto.ItemId);
        if (item == null)
            return;

        var updated = _mapper.Map<Item>(dto);

        if (dto.PhotoBinary != null)
        {
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
