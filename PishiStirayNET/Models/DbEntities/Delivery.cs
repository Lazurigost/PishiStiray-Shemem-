using System;
using System.Collections.Generic;
using PishiStiray.Models.DbEntities;

namespace PishiStiray;

public partial class Delivery
{
    public int IdpickupPoint { get; set; }

    public int PickupPoint { get; set; }

    public string? City { get; set; }

    public string? Street { get; set; }

    public int? House { get; set; }

    public virtual ICollection<Orderuser> Orderusers { get; } = new List<Orderuser>();
}
