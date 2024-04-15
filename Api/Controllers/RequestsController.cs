using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TMAWarehouse.DTOs;
using TMAWarehouse.Services;

namespace TMAWarehouse.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RequestsController : ControllerBase
{
    private readonly IRequestsService _service;

    public RequestsController(IRequestsService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AddRequestDto>> GetItemToOrder([FromRoute] int id)
    {
        var result = await _service.GetItemInfo(id);
        return Ok(result);
    }

    [HttpPost("order")]
    public async Task<ActionResult> OrderItem([FromBody] AddRequestDto dto, [FromQuery] string employeeName)
    {
        await _service.OrderItem(dto, employeeName);
        return Ok();
    }

    [HttpGet("requests")]
    public async Task<ActionResult<List<RequestDto>>> GetRequests()
    {
        var requests = await _service.GetRequests();
        return Ok(requests);
    }

    [HttpPost("requests/{requestId}/confirm")]
    public async Task<ActionResult> ConfirmRequest([FromRoute] int requestId)
    {
        await _service.ConfirmRequest(requestId);
        return Ok();
    }

    [HttpPost("requests/{requestId}/reject")]
    public async Task<ActionResult> RejectRequest([FromRoute] int requestId)
    {
        await _service.RejectRequest(requestId);
        return Ok();
    }
}
