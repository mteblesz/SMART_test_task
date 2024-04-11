using System;
using System.Collections.Generic;

namespace TMAWarehouse.Models;

public partial class Photo
{
    public int PhotoId { get; set; }

    public byte[]? PhotoBinary { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
