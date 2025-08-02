using System;
using System.Collections.Generic;

namespace ForNurseCom.ModelsMaria;

public partial class ListHospital
{
    public int HosId { get; set; }

    public string HosName { get; set; } = null!;

    public string? HosAddress { get; set; }

    public string? HosNumber { get; set; }
}
