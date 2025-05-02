using System;
using System.Collections.Generic;

namespace ForNurseCom.ModelsMaria;

public partial class Dorm
{
    public string ContractId { get; set; } = null!;

    public string GuestName { get; set; } = null!;

    public string GuestId { get; set; } = null!;

    public DateOnly Checkedin { get; set; }

    public string Room { get; set; } = null!;

    public double RatepDay { get; set; }

    public int StayDuration { get; set; }

    public DateTime Checkout { get; set; }

    public ulong Cashortransfer { get; set; }

    public string Cashier { get; set; } = null!;

    public double Totaltopay { get; set; }
}
