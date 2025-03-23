using System;
using System.Collections.Generic;

namespace ForNurseCom.ModelsMaria;

public partial class User
{
    public string Userid { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public string UserSalt { get; set; } = null!;
}
