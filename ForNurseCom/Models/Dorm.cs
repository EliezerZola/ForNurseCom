using System;
using System.Collections.Generic;

namespace ForNurseCom.Models;

public partial class Dorm
{
    public string ContractId { get; set; } = null!;

    public string GuestName { get; set; } = null!;

    public string GuestId { get; set; } = null!;

    public DateTime Checkedin { get; set; }

    public string Room { get; set; } = null!;

    public decimal RatepDay { get; set; }

    public int StayDuration { get; set; }

    public DateTime Checkout { get; set; }

    public bool Cashortransfer { get; set; }

    public string Cashier { get; set; } = null!;

    public decimal? Total { get; set; }
}
