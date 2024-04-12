namespace TMAWarehouse.DTOs;

public class AddItemDto
{
    public required string ItemName { get; set; }

    public int ItemGroupId { get; set; }

    public int MeasurementUnitId { get; set; }

    public int Quantity { get; set; }

    public decimal PriceWithoutVat { get; set; }

    public int ItemStatusId { get; set; }

    public string? StorageLocation { get; set; }

    public string? ContactPerson { get; set; }

    public byte[]? Photo { get; set; }
}
