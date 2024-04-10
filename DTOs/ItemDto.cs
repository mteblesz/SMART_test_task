using TMAWarehouse.Api.Models;

namespace TMAWarehouse.DTOs;

public class ItemDto
{
    public int ItemId { get; set; }

    public string ItemGroup { get; set; }

    public string MeasurementUnit { get; set; }

    public int Quantity { get; set; }

    public decimal PriceWithoutVat { get; set; }

    public string ItemStatus { get; set; }

    public string? StorageLocation { get; set; }

    public string? ContactPerson { get; set; }

    public byte[]? Photo { get; set; }

}
