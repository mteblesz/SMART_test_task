using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TMAWarehouse.DTOs;
using TMAWarehouse.Models;
using TMAWarehouse.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TMAWarehouse.Api.Controllers;
[ApiController]
[Area("api")]
[Route("[area]/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IItemsService _service;

    public ItemsController(IItemsService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<ItemDto>>> GetItems()
    {
        var result = await _service.GetItems();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EditItemDto>> GetItem([FromRoute]int id)
    {
        var result = await _service.GetItem(id);
        return Ok(result);
    }


    [HttpPost]
    public async Task<ActionResult> AddItem([FromForm] AddItemDto dto)
    {
        await _service.AddItem(dto);
        return Created();
    }

    [HttpPatch]
    public async Task<ActionResult> UpdateItem([FromForm] EditItemDto updated)
    {
        await _service.UpdateItem(updated);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteItem([FromRoute] int id)
    {
        await _service.DeleteItem(id);
        return NoContent();
    }
}
