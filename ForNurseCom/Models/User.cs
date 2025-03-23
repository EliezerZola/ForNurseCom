using System;
using System.Collections.Generic;

namespace ForNurseCom.Models;

public partial class User
{
    public Guid UserId { get; set; }

    public string Username { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public string UserSalt { get; set; } = null!;
}
