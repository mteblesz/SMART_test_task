using System;
using System.Collections.Generic;

namespace TMAWarehouse.Api.Models;

public partial class Request
{
    public int RequestId { get; set; }

    public string EmployeeName { get; set; } = null!;

    public int ItemId { get; set; }

    public int MeasurementUnitId { get; set; }

    public int Quantity { get; set; }

    public decimal PriceWithoutVat { get; set; }

    public string? Comment { get; set; }

    public int RequestStatusId { get; set; }

    public virtual Item Item { get; set; } = null!;

    public virtual MeasurementUnit MeasurementUnit { get; set; } = null!;

    public virtual RequestStatus RequestStatus { get; set; } = null!;
}
