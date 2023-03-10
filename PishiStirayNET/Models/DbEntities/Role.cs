using System;
using System.Collections.Generic;
using PishiStiray.Models.DbEntities;

namespace PishiStiray;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<UserDB> Users { get; } = new List<UserDB>();
}
