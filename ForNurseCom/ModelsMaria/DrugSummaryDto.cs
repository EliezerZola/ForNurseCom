using System;
using System.Collections.Generic;

namespace ForNurseCom.ModelsMaria;

public partial class DrugSummaryDto
{
    public string MedName { get; set; } = null!;

    public string MedLocation { get; set; } = null!;

    public int TotalGiven { get; set; }

    public int TotalAvailable { get; set; }
}
