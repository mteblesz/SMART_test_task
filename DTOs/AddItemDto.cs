using System.ComponentModel.DataAnnotations;
using TMAWarehouse.Models;

namespace TMAWarehouse.DTOs;

public class AddItemDto
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(255, MinimumLength = 1, ErrorMessage = "Item name must be between {2} and {1} characters long.")]
    public string? ItemName { get; set; }

    [Required(ErrorMessage = "Group is required.")]
    public int? ItemGroupId { get; set; }

    [Required(ErrorMessage = "Unit is required.")]
    public int? MeasurementUnitId { get; set; }

    [Required(ErrorMessage = "Quantity is required.")]
    [Range(0, int.MaxValue)]
    public int? Quantity { get; set; }

    [Required(ErrorMessage = "Price without VAT is required.")]
    [Range(0, double.MaxValue, ErrorMessage = "Price without VAT must be a positive value.")]
    public decimal? PriceWithoutVat { get; set; }

    [Required(ErrorMessage = "Status is required.")]
    public int? ItemStatusId { get; set; }

    [StringLength(255, ErrorMessage = "Storage location cannot exceed {1} characters.")]
    public string? StorageLocation { get; set; }

    [StringLength(255, ErrorMessage = "Contact person name cannot exceed {1} characters.")]
    public string? ContactPerson { get; set; }

    public byte[]? PhotoBinary { get; set; }

}
