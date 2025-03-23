using System;
using System.Collections.Generic;

namespace ForNurseCom.Models;

public partial class Drug
{
    public string Id { get; set; } = null!;

    public string MedName { get; set; } = null!;

    public string MedLocation { get; set; } = null!;

    public int MedQuantity { get; set; }
}
