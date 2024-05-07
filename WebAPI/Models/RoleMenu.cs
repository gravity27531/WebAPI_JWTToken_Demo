using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class RoleMenu
{
    public int MenuId { get; set; }

    public int RoleId { get; set; }

    public int? ParentId { get; set; }

    public int? NumSort { get; set; }

    public bool? IsDefault { get; set; }

    public string InsertBy { get; set; } = null!;

    public DateTime InsertDate { get; set; }

    public virtual Menu Menu { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
