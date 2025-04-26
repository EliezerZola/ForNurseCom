using System;
using System.Collections.Generic;

namespace ForNurseCom.ModelsMaria;

public partial class Budget
{
    public string BudgetId { get; set; } = null!;

    public double Income { get; set; }

    public double? Tithe { get; set; }

    public double Saving { get; set; }

    public double Grocery { get; set; }

    public double Lunchanddinner { get; set; }

    public double? Offering { get; set; }

    public double? MobileEx { get; set; }

    public double? Fun { get; set; }

    public double? OnlineShopping { get; set; }

    public double? Rentormorgage { get; set; }

    public double? Familysupport { get; set; }

    public double? Gym { get; set; }

    public double? Charity { get; set; }

    public double? Expectedexpense { get; set; }

    public double? Actualexpense { get; set; }

    public double? Budgetaftersaving { get; set; }

    public double? CashinHand { get; set; }

    public string UsernameB { get; set; } = null!;

    public string Passwordb { get; set; } = null!;

    public string Passwordbb { get; set; } = null!;
}
