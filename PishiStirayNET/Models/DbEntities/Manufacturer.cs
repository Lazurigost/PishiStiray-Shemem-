using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace PishiStiray.Models.DbEntities;

public partial class Manufacturer
{
    [Key]
    public int IdproductManufacturers { get; set; }

    public string? ManufacturerName { get; set; }

    public virtual ICollection<ProductDB> Products { get; } = new List<ProductDB>();
}
