using System;
using System.Collections.Generic;

namespace MSWT_BussinessObject.Model;
public partial class Request
{
    public string RequestId { get; set; } = null!;

    public string? UserId { get; set; }

    public string? Description { get; set; }

    public string? Status { get; set; }

    public DateOnly? RequestDate { get; set; }

    public DateOnly? ResolveDate { get; set; }

    public string? Location { get; set; }

    public virtual User? User { get; set; }
}
