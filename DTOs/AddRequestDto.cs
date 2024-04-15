using System.ComponentModel.DataAnnotations;

namespace TMAWarehouse.DTOs;

public class AddRequestDto
{
    public int ItemId { get; set; }
    public required string ItemName { get; set; }


    public required string MeasurementUnitName { get; set; }
    public int MeasurementUnitId { get; set; }

    public int MaxQuantity { get; set; }

    public decimal PriceWithoutVat { get; set; }


    [Required(ErrorMessage = "Quantity is required.")]
    [Range(0, int.MaxValue)]
    [OrderQuantity(nameof(MaxQuantity))]
    public int OrderQuantity { get; set; }

    [StringLength(500, ErrorMessage = "Comment can't exceed {1} characters.")]
    public string? Comment { get; set; }
}

public class OrderQuantityAttribute : ValidationAttribute
{
    private readonly string _maxQuantityProperty;

    public OrderQuantityAttribute(string maxQuantityProperty)
    {
        _maxQuantityProperty = maxQuantityProperty;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var maxQuantityProperty = validationContext.ObjectType.GetProperty(_maxQuantityProperty);

        if (maxQuantityProperty == null)
        {
            throw new ArgumentException("Property with this name not found");
        }

        var maxQuantityValue = (int)maxQuantityProperty.GetValue(validationContext.ObjectInstance, null);
        var orderQuantityValue = (int)value;

        if (orderQuantityValue > maxQuantityValue)
        {
            return new ValidationResult($"The order quantity must be less than or equal to {maxQuantityValue}.");
        }

        return ValidationResult.Success!;
    }
}