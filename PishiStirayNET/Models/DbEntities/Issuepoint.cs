using PishiStiray.Models.DbEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PishiStiray;

public partial class Issuepoint
{
    [Key]
    public int IdPunkta { get; set; }

    public int PunktIndex { get; set; }

    public string PunktCity { get; set; } = null!;

    public string PunktStreet { get; set; } = null!;

    public int? PunktDom { get; set; }

    public virtual ICollection<Orderuser> Orderusers { get; } = new List<Orderuser>();
}
