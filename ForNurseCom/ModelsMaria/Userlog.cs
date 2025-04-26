using System;
using System.Collections.Generic;

namespace ForNurseCom.ModelsMaria;

public partial class Userlog
{
    public string Id { get; set; } = null!;

    public DateOnly Logintime { get; set; }

    public string Username { get; set; } = null!;
}
