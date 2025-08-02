using System;
using System.Collections.Generic;

namespace ForNurseCom.ModelsMaria;

public partial class DateUser
{
    public int? StudentId { get; set; }

    public int? Username { get; set; }

    public int? Password { get; set; }

    public int? Salt { get; set; }
}
