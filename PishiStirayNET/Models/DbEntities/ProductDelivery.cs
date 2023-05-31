using System;
using System.Collections.Generic;

namespace PishiStiray;

public partial class ProductDelivery
{
    public int IdproductDeliveries { get; set; }

    public string? DeliveryName { get; set; }

    public virtual ICollection<ProductDB> Products { get; } = new List<ProductDB>();
}
