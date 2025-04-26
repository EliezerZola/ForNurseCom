using System;
using System.Collections.Generic;

namespace ForNurseCom.ModelsMaria;

public partial class Visit
{
    public string VisitId { get; set; } = null!;

    public DateOnly CreatedAt { get; set; }

    public string PtId { get; set; } = null!;

    public string PtName { get; set; } = null!;

    public string PtAge { get; set; } = null!;

    public string PtWeight { get; set; } = null!;

    public string PtHeight { get; set; } = null!;

    public string PtBp { get; set; } = null!;

    public string PtHeart { get; set; } = null!;

    public string PtTemp { get; set; } = null!;

    public string PtDept { get; set; } = null!;

    public string PtResidence { get; set; } = null!;

    public string PtLocation { get; set; } = null!;

    public string PtNumber { get; set; } = null!;

    public string VisitType { get; set; } = null!;

    public string PtImg { get; set; } = null!;

    public string Symptoms { get; set; } = null!;

    public string BodySystem { get; set; } = null!;

    public string? EmerName { get; set; }

    public string? EmerPhone { get; set; }

    public string? EmerAddress { get; set; }

    public string Medicines { get; set; } = null!;

    public int MedQ { get; set; }

    public string? MedicinesA { get; set; }

    public int? MedQa { get; set; }

    public string? MedicinesAb { get; set; }

    public int? MedQab { get; set; }

    public string NurseName { get; set; } = null!;

    public string? Medicines4 { get; set; }

    public string? Medicines5 { get; set; }

    public int? MedQ4 { get; set; }

    public int? MedQ5 { get; set; }
}
