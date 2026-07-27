using System;
using System.Collections.Generic;

namespace ForNurseCom.ModelsMaria;

/// <summary>
/// for testing queries
/// </summary>
public partial class TestTable
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string PatientCategory { get; set; } = null!;

    public string PtId { get; set; } = null!;
}
