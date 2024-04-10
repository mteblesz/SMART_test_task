using System;
using System.Collections.Generic;

namespace TMAWarehouse.Api.Models;

public partial class ItemStatus
{
    public int StatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
