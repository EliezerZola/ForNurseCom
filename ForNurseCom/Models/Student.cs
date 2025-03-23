using System;
using System.Collections.Generic;

namespace ForNurseCom.Models;

public partial class Student
{
    public string StdId { get; set; } = null!;

    public string StdName { get; set; } = null!;

    public string StdFac { get; set; } = null!;

    public byte[] StdImg { get; set; } = null!;
}
