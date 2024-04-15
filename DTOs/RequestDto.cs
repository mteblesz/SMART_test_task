using TMAWarehouse.Models;

namespace TMAWarehouse.DTOs;

public class RequestDto
{
    public int RequestId { get; set; }

    public string EmployeeName { get; set; } = null!;

    public ItemDto Item { get; set; }

    public int MeasurementUnitId { get; set; }
    public required string MeasurementUnitName { get; set; }
    public required string RequestStatusName { get; set; }
    
    public int Quantity { get; set; }

    public decimal PriceWithoutVat { get; set; }

    public string? Comment { get; set; }

    public int RequestStatusId { get; set; }

}
