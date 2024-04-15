using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TMAWarehouse.DTOs;
using TMAWarehouse.Models;

namespace TMAWarehouse.Services;

public interface IRequestsService
{
    Task OrderItem(AddRequestDto dto, string employeeName);
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
        var item = await _context.Items.FirstOrDefaultAsync(i => i.ItemId == dto.ItemId);
        if (item == null)
        {
            throw new ArgumentException("Item not found");
        }


        var status = _context.RequestStatuses.FirstOrDefault();
        if (status == null)
        {
            var newStatusEntity = await _context.RequestStatuses
                .AddAsync(new RequestStatus() { StatusName = "New" });
            status = newStatusEntity.Entity;
        }
        var request = new Request()
        {
            EmployeeName = employeeName,
            ItemId = dto.ItemId,
            MeasurementUnitId = dto.MeasurementUnitId,
            Quantity = dto.OrderQuantity,
            PriceWithoutVat = dto.PriceWithoutVat,
            Comment = dto.Comment,
            RequestStatus = status,
        };
        
        item.Quantity -= dto.OrderQuantity;

        _context.Requests.Add(request);
        await _context.SaveChangesAsync();
    }
}
