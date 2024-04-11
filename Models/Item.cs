using System;
using System.Collections.Generic;

namespace TMAWarehouse.Models;

public partial class Item
{
    public int ItemId { get; set; }

    public string ItemName { get; set; } = null!;

    public int ItemGroupId { get; set; }

    public int MeasurementUnitId { get; set; }

    public int Quantity { get; set; }

    public decimal PriceWithoutVat { get; set; }

    public int ItemStatusId { get; set; }

    public string? StorageLocation { get; set; }

    public string? ContactPerson { get; set; }

    public int PhotoId { get; set; }

    public virtual ItemGroup ItemGroup { get; set; } = null!;

    public virtual ItemStatus ItemStatus { get; set; } = null!;

    public virtual MeasurementUnit MeasurementUnit { get; set; } = null!;

    public virtual Photo Photo { get; set; } = null!;

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
