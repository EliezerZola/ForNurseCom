using System;
using System.Collections.Generic;

namespace ForNurseCom.Models;

public partial class Visit
{
    public string VisitId { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public string? PtId { get; set; }

    public string? PtName { get; set; }

    public string? PtAge { get; set; }

    public string? PtWeight { get; set; }

    public string? PtHeight { get; set; }

    public string? PtBp { get; set; }

    public string? PtHeart { get; set; }

    public string? PtTemp { get; set; }

    public string? PtDept { get; set; }

    public string? PtResidence { get; set; }

    public string? PtLocation { get; set; }

    public string? PtNumber { get; set; }

    public string? VisitType { get; set; }

    public string? PtImg { get; set; }

    public string? Symptoms { get; set; }

    public string? BodySystem { get; set; }

    public string? EmerName { get; set; }

    public string? EmerPhone { get; set; }

    public string? EmerAddress { get; set; }

    public string? Medicines { get; set; }

    public int? MedQ { get; set; }

    public string? MedicinesA { get; set; }

    public int? MedQa { get; set; }

    public string? MedicinesAb { get; set; }

    public int? MedQab { get; set; }

    public string NurseName { get; set; } = null!;

    public string? Medicines4 { get; set; }

    public int? MedQ4 { get; set; }

    public string? Medicines5 { get; set; }

    public int? MedQ5 { get; set; }
}
