using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace ForNurseCom.ModelsMaria;

public partial class KmedicDbContext : DbContext
{
    public KmedicDbContext()
    {
    }

    public KmedicDbContext(DbContextOptions<KmedicDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Budget> Budgets { get; set; }

    public virtual DbSet<BudgetUser> BudgetUsers { get; set; }

    public virtual DbSet<Dorm> Dorms { get; set; }

    public virtual DbSet<Drug> Drugs { get; set; }

    public virtual DbSet<Drugchange> Drugchanges { get; set; }

    public virtual DbSet<Keyrequest> Keyrequests { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Userlog> Userlogs { get; set; }

    public virtual DbSet<Visit> Visits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=10.22.198.68;database=kmedic;user=root;password=Elwezer2024;allowzerodatetime=True;convertzerodatetime=True", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.5.28-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Budget>(entity =>
        {
            entity.HasKey(e => e.BudgetId).HasName("PRIMARY");

            entity.ToTable("budget");

            entity.Property(e => e.BudgetId)
                .HasMaxLength(50)
                .HasColumnName("budgetID");
            entity.Property(e => e.Actualexpense)
                .HasDefaultValueSql("'0'")
                .HasColumnName("actualexpense");
            entity.Property(e => e.Budgetaftersaving)
                .HasDefaultValueSql("'0'")
                .HasColumnName("budgetaftersaving");
            entity.Property(e => e.CashinHand)
                .HasDefaultValueSql("'0'")
                .HasColumnName("cashinHand");
            entity.Property(e => e.Charity)
                .HasDefaultValueSql("'0'")
                .HasColumnName("charity");
            entity.Property(e => e.Expectedexpense)
                .HasDefaultValueSql("'0'")
                .HasColumnName("expectedexpense");
            entity.Property(e => e.Familysupport)
                .HasDefaultValueSql("'0'")
                .HasColumnName("familysupport");
            entity.Property(e => e.Fun).HasDefaultValueSql("'0'");
            entity.Property(e => e.Grocery).HasColumnName("grocery");
            entity.Property(e => e.Gym)
                .HasDefaultValueSql("'0'")
                .HasColumnName("gym");
            entity.Property(e => e.Lunchanddinner).HasColumnName("lunchanddinner");
            entity.Property(e => e.MobileEx)
                .HasDefaultValueSql("'0'")
                .HasColumnName("mobileEx");
            entity.Property(e => e.Offering)
                .HasDefaultValueSql("'0'")
                .HasColumnName("offering");
            entity.Property(e => e.OnlineShopping).HasDefaultValueSql("'0'");
            entity.Property(e => e.Passwordb)
                .HasMaxLength(50)
                .HasColumnName("passwordb");
            entity.Property(e => e.Passwordbb)
                .HasMaxLength(50)
                .HasColumnName("passwordbb");
            entity.Property(e => e.Rentormorgage)
                .HasDefaultValueSql("'0'")
                .HasColumnName("rentormorgage");
            entity.Property(e => e.Saving).HasColumnName("saving");
            entity.Property(e => e.Tithe)
                .HasDefaultValueSql("'0'")
                .HasColumnName("tithe");
            entity.Property(e => e.UsernameB).HasMaxLength(50);
        });

        modelBuilder.Entity<BudgetUser>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("PRIMARY");

            entity.ToTable("budgetUser");

            entity.Property(e => e.Userid)
                .HasMaxLength(64)
                .HasColumnName("userid");
            entity.Property(e => e.UserName).HasMaxLength(70);
            entity.Property(e => e.UserPassword).HasColumnType("text");
            entity.Property(e => e.UserPasswordb).HasColumnType("text");
        });

        modelBuilder.Entity<Dorm>(entity =>
        {
            entity.HasKey(e => e.ContractId).HasName("PRIMARY");

            entity.ToTable("dorm");

            entity.Property(e => e.ContractId).HasMaxLength(20);
            entity.Property(e => e.Cashier)
                .HasMaxLength(70)
                .HasColumnName("cashier");
            entity.Property(e => e.Cashortransfer)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)")
                .HasColumnName("cashortransfer");
            entity.Property(e => e.Checkout).HasColumnName("checkout");
            entity.Property(e => e.GuestId)
                .HasMaxLength(15)
                .HasColumnName("GuestID");
            entity.Property(e => e.GuestName).HasMaxLength(70);
            entity.Property(e => e.Room).HasMaxLength(10);
            entity.Property(e => e.StayDuration).HasColumnType("int(11)");
            entity.Property(e => e.Totaltopay).HasColumnName("totaltopay");
        });

        modelBuilder.Entity<Drug>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("drug");

            entity.Property(e => e.Id)
                .HasMaxLength(20)
                .HasColumnName("id");
            entity.Property(e => e.MedLocation)
                .HasMaxLength(20)
                .HasColumnName("Med_Location");
            entity.Property(e => e.MedName)
                .HasMaxLength(70)
                .HasColumnName("Med_Name");
            entity.Property(e => e.MedQuantity)
                .HasColumnType("int(11)")
                .HasColumnName("Med_Quantity");
        });

        modelBuilder.Entity<Drugchange>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("drugchange");

            entity.Property(e => e.Id)
                .HasMaxLength(70)
                .HasColumnName("id");
            entity.Property(e => e.MedLocation)
                .HasMaxLength(50)
                .HasColumnName("Med_Location");
            entity.Property(e => e.MedName)
                .HasMaxLength(70)
                .HasColumnName("Med_Name");
            entity.Property(e => e.MedQuantity)
                .HasColumnType("int(11)")
                .HasColumnName("Med_Quantity");
        });

        modelBuilder.Entity<Keyrequest>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("keyrequest");

            entity.Property(e => e.Key).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(20);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("PRIMARY");

            entity.ToTable("user");

            entity.Property(e => e.Userid)
                .HasMaxLength(64)
                .HasColumnName("userid");
            entity.Property(e => e.UserPassword).HasColumnType("text");
            entity.Property(e => e.UserSalt).HasColumnType("text");
            entity.Property(e => e.Username).HasMaxLength(70);
        });

        modelBuilder.Entity<Userlog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("userlogs");

            entity.Property(e => e.Id)
                .HasMaxLength(64)
                .HasColumnName("id");
            entity.Property(e => e.Logintime).HasColumnName("logintime");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<Visit>(entity =>
        {
            entity.HasKey(e => e.VisitId).HasName("PRIMARY");

            entity.ToTable("visit");

            entity.Property(e => e.VisitId)
                .HasMaxLength(64)
                .HasDefaultValueSql("''")
                .HasColumnName("VisitID");
            entity.Property(e => e.BodySystem)
                .HasMaxLength(80)
                .HasColumnName("Body_System");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.EmerAddress).HasColumnName("Emer_Address");
            entity.Property(e => e.EmerName)
                .HasMaxLength(80)
                .HasColumnName("Emer_Name");
            entity.Property(e => e.EmerPhone)
                .HasMaxLength(15)
                .HasColumnName("Emer_Phone");
            entity.Property(e => e.MedQ).HasColumnType("int(11)");
            entity.Property(e => e.MedQ4)
                .HasDefaultValueSql("'0'")
                .HasColumnType("int(11)");
            entity.Property(e => e.MedQ5)
                .HasDefaultValueSql("'0'")
                .HasColumnType("int(11)");
            entity.Property(e => e.MedQa)
                .HasDefaultValueSql("'0'")
                .HasColumnType("int(11)")
                .HasColumnName("MedQA");
            entity.Property(e => e.MedQab)
                .HasDefaultValueSql("'0'")
                .HasColumnType("int(11)")
                .HasColumnName("MedQAB");
            entity.Property(e => e.Medicines).HasMaxLength(80);
            entity.Property(e => e.Medicines4).HasMaxLength(80);
            entity.Property(e => e.Medicines5).HasMaxLength(80);
            entity.Property(e => e.MedicinesA).HasMaxLength(80);
            entity.Property(e => e.MedicinesAb)
                .HasMaxLength(80)
                .HasColumnName("MedicinesAB");
            entity.Property(e => e.NurseName)
                .HasMaxLength(80)
                .HasColumnName("Nurse_Name");
            entity.Property(e => e.PtAge)
                .HasMaxLength(6)
                .HasColumnName("Pt_Age");
            entity.Property(e => e.PtBp)
                .HasMaxLength(7)
                .HasColumnName("Pt_BP");
            entity.Property(e => e.PtDept)
                .HasMaxLength(80)
                .HasColumnName("Pt_Dept");
            entity.Property(e => e.PtHeart)
                .HasMaxLength(6)
                .HasColumnName("Pt_Heart");
            entity.Property(e => e.PtHeight)
                .HasMaxLength(6)
                .HasColumnName("Pt_Height");
            entity.Property(e => e.PtId)
                .HasMaxLength(16)
                .HasColumnName("Pt_ID");
            entity.Property(e => e.PtImg).HasColumnName("Pt_Img");
            entity.Property(e => e.PtLocation)
                .HasMaxLength(80)
                .HasColumnName("Pt_Location");
            entity.Property(e => e.PtName)
                .HasMaxLength(100)
                .HasColumnName("Pt_Name");
            entity.Property(e => e.PtNumber)
                .HasMaxLength(15)
                .HasColumnName("Pt_Number");
            entity.Property(e => e.PtResidence)
                .HasMaxLength(20)
                .HasColumnName("Pt_Residence");
            entity.Property(e => e.PtTemp)
                .HasMaxLength(6)
                .HasColumnName("Pt_Temp");
            entity.Property(e => e.PtWeight)
                .HasMaxLength(6)
                .HasColumnName("Pt_Weight");
            entity.Property(e => e.VisitType)
                .HasMaxLength(80)
                .HasColumnName("Visit_Type");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
