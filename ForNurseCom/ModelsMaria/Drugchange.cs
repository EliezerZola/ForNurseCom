using System;
using System.Collections.Generic;

namespace ForNurseCom.ModelsMaria;

public partial class Drugchange
{
    public string Id { get; set; } = null!;

    public string MedName { get; set; } = null!;

    public string MedLocation { get; set; } = null!;

    public int MedQuantity { get; set; }

    public DateTime TimePrescribe { get; set; }
}
