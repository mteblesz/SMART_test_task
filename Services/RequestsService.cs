using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using TMAWarehouse.DTOs;
using TMAWarehouse.Models;

namespace TMAWarehouse.Services;

public interface IRequestsService
{
    Task OrderItem(AddRequestDto dto, string employeeName);
    Task<List<RequestDto>> GetRequests();
    Task ConfirmRequest(int requestId);
    Task RejectRequest(int requestId);
}

public class RequestsService : IRequestsService
{
    private readonly TmaDbContext _context;
    private readonly IMapper _mapper;

    public RequestsService(TmaDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task OrderItem(AddRequestDto dto, string employeeName)
    {
        if (dto.OrderQuantity > dto.MaxQuantity) 
        {
            throw new ArgumentException("Order quantity must not exceed available quantity.");
        }


        var status = _context.RequestStatuses.FirstOrDefault();
        if (status == null)
        {
            var newStatusEntity = await _context.RequestStatuses
                .AddAsync(new RequestStatus() { StatusName = "New" });
            status = newStatusEntity.Entity;
        }
        var request = new Models.Request()
        {
            EmployeeName = employeeName,
            ItemId = dto.ItemId,
            MeasurementUnitId = dto.MeasurementUnitId,
            Quantity = dto.OrderQuantity,
            PriceWithoutVat = dto.PriceWithoutVat,
            Comment = dto.Comment,
            RequestStatus = status,
        };

        _context.Requests.Add(request);
        await _context.SaveChangesAsync();
    }

    public async Task<List<RequestDto>> GetRequests()
    {
        var requests = _context.Requests
            .Include(r => r.MeasurementUnit)
            .Include(r => r.RequestStatus)
            .Include(r => r.Item)
                .ThenInclude(i => i.Photo);
        var result = _mapper.Map<List<RequestDto>>(requests);

        return result;
    }

    public async Task ConfirmRequest(int requestId)
    {
        var request = await _context.Requests
            .Include(r => r.Item)
            .FirstOrDefaultAsync(r => r.RequestId == requestId) 
            ?? throw new ArgumentException("Request not found");
        
        if (request.Item == null)
        {
            throw new ArgumentException("Item not found");
        }
        if (request.Quantity > request.Item.Quantity)
        {
            throw new ArgumentException("Order quantity must not exceed available quantity.");
        }

        request.Item.Quantity -= request.Quantity;
        request.RequestStatusId = 2;
        await _context.SaveChangesAsync();

    }
    public async Task RejectRequest(int requestId)
    {
        var request = await _context.Requests
            .Include(r => r.Item)
            .FirstOrDefaultAsync(r => r.RequestId == requestId)
            ?? throw new ArgumentException("Request not found");

        request.RequestStatusId = 3;
        await _context.SaveChangesAsync();
    }
}
