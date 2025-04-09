using System;
using System.Collections.Generic;

namespace ForNurseCom.Models;

public partial class User
{
    public string Userid { get; set; } = null!;

    public string? Username { get; set; }

    public string? UserPassword { get; set; }

    public string? UserSalt { get; set; }
}
