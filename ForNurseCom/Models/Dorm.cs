using System;
using System.Collections.Generic;

namespace ForNurseCom.Models;

public partial class Dorm
{
    public string ContractId { get; set; } = null!;

    public string? GuestName { get; set; }

    public string? GuestId { get; set; }

    public DateTime? Checkedin { get; set; }

    public string? Room { get; set; }

    public double? RatepDay { get; set; }

    public int? StayDuration { get; set; }

    public DateTime? Checkout { get; set; }

    public ulong? Cashortransfer { get; set; }

    public string? Cashier { get; set; }

    public double? Totaltopay { get; set; }
}
