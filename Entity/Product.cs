using System;
using System.Collections.Generic;

namespace Entity;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductName { get; set; }

    public double? Price { get; set; }

    public int? CategoryId { get; set; }

    public string? Description { get; set; }

    public string? ImgUrl { get; set; }

    public string? Color { get; set; }

    public string? Material { get; set; }

    public short Quantity { get; set; }

    public bool IsActive { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrdeItem> OrdeItems { get; set; } = new List<OrdeItem>();
}
