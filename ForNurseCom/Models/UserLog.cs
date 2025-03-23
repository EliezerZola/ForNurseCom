using System;
using System.Collections.Generic;

namespace ForNurseCom.Models;

public partial class UserLog
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = null!;

    public DateTime Logintime { get; set; }
}
