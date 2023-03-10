using PishiStiray.Models.DbEntities;
using System.Collections.Generic;

namespace PishiStiray;

public partial class OrderStatus
{
    public int IdOrderStatus { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Orderuser> Orderusers { get; } = new List<Orderuser>();
}
