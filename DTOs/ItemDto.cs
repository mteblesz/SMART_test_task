
using System.ComponentModel.DataAnnotations;

namespace TMAWarehouse.DTOs;

public class ItemDto
{
    public int ItemId { get; set; }
    public required string ItemName { get; set; }

    public required string ItemGroup { get; set; }

    public required string MeasurementUnit { get; set; }

    public int Quantity { get; set; }

    public decimal PriceWithoutVat { get; set; }

    public required string ItemStatus { get; set; }

    public string? StorageLocation { get; set; }

    public string? ContactPerson { get; set; }

    public byte[]? PhotoBinary { get; set; }

    public int ItemGroupId { get; set; }
    public int MeasurementUnitId { get; set; }
    public int ItemStatusId { get; set; }
    public int? PhotoId { get; set; }
}
