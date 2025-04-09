﻿using System;
using System.Collections.Generic;

namespace ForNurseCom.Models;

public partial class Drugchange
{
    public string Id { get; set; } = null!;

    public string? MedName { get; set; }

    public string? MedLocation { get; set; }

    public int? MedQuantity { get; set; }

    public DateTime? TimePrescribe { get; set; }
}
