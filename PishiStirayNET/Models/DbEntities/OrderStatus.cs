using PishiStirayNET.Models.DbEntities;
using System.Collections.Generic;

namespace PishiStirayNET;

public partial class OrderStatus
{
    public int IdOrderStatus { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Orderuser> Orderusers { get; } = new List<Orderuser>();
}
