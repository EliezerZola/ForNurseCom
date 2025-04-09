using System;
using System.Collections.Generic;

namespace ForNurseCom.Models;

public partial class Userlog
{
    public string Id { get; set; } = null!;

    public string? Username { get; set; }

    public DateTime? Logintime { get; set; }
}
