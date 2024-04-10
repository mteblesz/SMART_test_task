using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMAWarehouse.Api.DTOs;
using TMAWarehouse.Api.Models;

namespace TMAWarehouse.Api.Controllers;
[ApiController]
[Area("api")]
[Route("[area]/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly TmaDbContext _context;
    private readonly IMapper _mapper;

    public ItemsController(TmaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<List<ItemDto>> GetItems()
    {
        var items = _context.Items
            .Include(i => i.ItemGroup)
            .Include(i => i.MeasurementUnit)
            .Include(i => i.ItemStatus);
        var result = _mapper.Map<List<ItemDto>>(items);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> AddItem([FromForm] Item item)
    {
        _context.Items.Add(item);
        await _context.SaveChangesAsync();

        return Created();
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> UpdateItem(int id, [FromBody] AddItemDto updated)
    {
        var item = await _context.Items.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }

        _context.Entry(item).CurrentValues.SetValues(updated); // no mapping needed
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveItem(int id)
    {
        var item = await _context.Items.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }

        _context.Items.Remove(item);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
