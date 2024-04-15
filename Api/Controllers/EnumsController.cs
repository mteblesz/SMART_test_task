using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TMAWarehouse.DTOs;
using TMAWarehouse.Services;

namespace TMAWarehouse.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class EnumsController : ControllerBase
{
    private readonly IEnumsService _service;

    public EnumsController(IEnumsService service)
    {
        _service = service;
    }

    [HttpGet("groups/{id}")]
    public async Task<ActionResult<ItemGroupDto>> GetItemGroup([FromRoute] int id)
    {
        var result = await _service.GetItemGroup(id);
        return Ok(result);
    }

    [HttpGet("groups")]
    public async Task<ActionResult<List<ItemGroupDto>>> GetItemGroups()
    {
        var result = await _service.GetItemGroups();
        return Ok(result);
    }

    [HttpGet("status/{id}")]
    public async Task<ActionResult<ItemStatusDto>> GetItemStatus([FromRoute] int id)
    {
        var result = await _service.GetItemStatus(id);
        return Ok(result);
    }

    [HttpGet("status")]
    public async Task<ActionResult<List<ItemStatusDto>>> GetItemStatuses()
    {
        var result = await _service.GetItemStatuses();
        return Ok(result);
    }

    [HttpGet("units/{id}")]
    public async Task<ActionResult<MeasurementUnitDto>> GetUnit([FromRoute] int id)
    {
        var result = await _service.GetMeasurementUnit(id);
        return Ok(result);
    }

    [HttpGet("units")]
    public async Task<ActionResult<List<MeasurementUnitDto>>> GetUnits()
    {
        var result = await _service.GetMeasurementUnits();
        return Ok(result);
    }
}
