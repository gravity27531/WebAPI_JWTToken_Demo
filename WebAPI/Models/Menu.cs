using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class Menu
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Detail { get; set; }

    public string? Controller { get; set; }

    public string? Action { get; set; }

    public string? Area { get; set; }

    public bool IsShow { get; set; }

    public int Side { get; set; }

    public int Status { get; set; }

    public string InsertBy { get; set; } = null!;

    public DateTime InsertDate { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<RoleMenu> RoleMenus { get; set; } = new List<RoleMenu>();
}
