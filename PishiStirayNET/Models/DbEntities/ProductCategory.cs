using System;
using System.Collections.Generic;

namespace PishiStirayNET;

public partial class ProductCategory
{
    public int IdproductCategory { get; set; }

    public string? CategoryName { get; set; }

    public virtual ICollection<ProductDB> Products { get; } = new List<ProductDB>();
}
