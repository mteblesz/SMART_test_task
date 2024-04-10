using System;
using System.Collections.Generic;

namespace TMAWarehouse.Api.Models;

public partial class ItemGroup
{
    public int GroupId { get; set; }

    public string GroupName { get; set; } = null!;

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
