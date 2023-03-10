using System;
using System.Collections.Generic;

namespace PishiStiray.Models.DbEntities;

public partial class Unit
{
    public int IdproductDeliveries { get; set; }

    public string? DeliveryName { get; set; }

    public virtual ICollection<ProductDB> Products { get; } = new List<ProductDB>();
}
