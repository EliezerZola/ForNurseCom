using System;
using System.Collections.Generic;

namespace ForNurseCom.Models;

public partial class DrugChange
{
    public Guid Id { get; set; }

    public string MedName { get; set; } = null!;

    public int MedQuantity { get; set; }

    public string MedLocation { get; set; } = null!;

    public DateTime TimePrescribed { get; set; }
}
