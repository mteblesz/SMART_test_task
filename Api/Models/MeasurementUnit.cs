using System;
using System.Collections.Generic;

namespace TMAWarehouse.Api.Models;

public partial class MeasurementUnit
{
    public int UnitId { get; set; }

    public string UnitName { get; set; } = null!;

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
