using System;
using System.Collections.Generic;

namespace MSWT_BussinessObject.Model;

public partial class Role
{
    public string RoleId { get; set; } = null!;

    public string? RoleName { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
