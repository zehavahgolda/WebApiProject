using System;
using System.Collections.Generic;

namespace Entity;

public partial class Order
{
    public int OrderId { get; set; }

    public DateOnly? OredrDate { get; set; }

    public double? OrderSum { get; set; }

    public int? UserId { get; set; }

    public string OrderStatus { get; set; } = null!;

    public virtual ICollection<OrdeItem> OrdeItems { get; set; } = new List<OrdeItem>();

    public virtual User? User { get; set; }
}
