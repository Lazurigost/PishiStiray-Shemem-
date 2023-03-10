using System;
using System.Collections.Generic;

namespace PishiStiray.Models.DbEntities;

public partial class Orderuser
{
    public int OrderId { get; set; }

    public string OrderStatus { get; set; } = null!;

    public DateOnly OrderDeliveryDate { get; set; }

    public int OrderPickupPoint { get; set; }

    public string? OrdererFio { get; set; }

    public int OrderAquireCode { get; set; }

    public virtual Delivery OrderPickupPointNavigation { get; set; } = null!;

    public virtual ICollection<Orderproduct> Orderproducts { get; } = new List<Orderproduct>();
}
