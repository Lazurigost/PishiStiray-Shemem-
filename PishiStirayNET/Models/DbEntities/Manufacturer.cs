using System;
using System.Collections.Generic;

namespace PishiStirayNET.Models.DbEntities;

public partial class Manufacturer
{
    public int IdproductManufacturers { get; set; }

    public string? ManufacturerName { get; set; }

    public virtual ICollection<ProductDB> Products { get; } = new List<ProductDB>();
}
