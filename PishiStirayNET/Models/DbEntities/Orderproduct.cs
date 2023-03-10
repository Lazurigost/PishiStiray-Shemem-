using System;
using System.Collections.Generic;
using PishiStirayNET.Models.DbEntities;

namespace PishiStirayNET;

public partial class Orderproduct
{
    public int OrderId { get; set; }

    public string ProductArticleNumber { get; set; } = null!;

    public int? ProductAmount { get; set; }

    public virtual Orderuser Order { get; set; } = null!;

    public virtual ProductDB ProductArticleNumberNavigation { get; set; } = null!;
}
