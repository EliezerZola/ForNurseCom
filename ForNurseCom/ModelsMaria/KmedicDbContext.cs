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

    public virtual DbSet<Dorm> Dorms { get; set; }

    public virtual DbSet<Drug> Drugs { get; set; }

    public virtual DbSet<Drugchange> Drugchanges { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Userlog> Userlogs { get; set; }

    public virtual DbSet<Visit> Visits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=10.22.198.67;database=kmedic;user=root;password=Elwezer2024", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.5.28-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Dorm>(entity =>
        {
            entity.HasKey(e => e.ContractId).HasName("PRIMARY");

            entity.ToTable("dorm");

            entity.Property(e => e.ContractId)
                .HasMaxLength(20)
                .HasColumnName("contractID");
            entity.Property(e => e.Cashier)
                .HasMaxLength(70)
                .HasColumnName("cashier");
            entity.Property(e => e.Cashortransfer)
                .HasColumnType("bit(1)")
                .HasColumnName("cashortransfer");
            entity.Property(e => e.Checkedin).HasColumnType("datetime");
            entity.Property(e => e.Checkout)
                .HasColumnType("datetime")
                .HasColumnName("checkout");
            entity.Property(e => e.GuestId)
                .HasMaxLength(15)
                .HasColumnName("GuestID");
            entity.Property(e => e.GuestName).HasMaxLength(50);
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
            entity.Property(e => e.TimePrescribe).HasColumnType("datetime");
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
            entity.Property(e => e.Logintime)
                .HasColumnType("datetime")
                .HasColumnName("logintime");
            entity.Property(e => e.Username).HasMaxLength(70);
        });

        modelBuilder.Entity<Visit>(entity =>
        {
            entity.HasKey(e => e.VisitId).HasName("PRIMARY");

            entity.ToTable("visit");

            entity.Property(e => e.VisitId)
                .HasMaxLength(64)
                .HasColumnName("VisitID");
            entity.Property(e => e.BodySystem)
                .HasMaxLength(70)
                .HasColumnName("Body_System");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.EmerAddress)
                .HasColumnType("text")
                .HasColumnName("Emer_Address");
            entity.Property(e => e.EmerName)
                .HasMaxLength(80)
                .HasColumnName("Emer_Name");
            entity.Property(e => e.EmerPhone)
                .HasMaxLength(15)
                .HasColumnName("Emer_Phone");
            entity.Property(e => e.MedQ).HasColumnType("int(11)");
            entity.Property(e => e.MedQ4).HasColumnType("int(11)");
            entity.Property(e => e.MedQ5).HasColumnType("int(11)");
            entity.Property(e => e.MedQa)
                .HasColumnType("int(11)")
                .HasColumnName("MedQA");
            entity.Property(e => e.MedQab)
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
                .HasMaxLength(100)
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
            entity.Property(e => e.PtImg)
                .HasColumnType("text")
                .HasColumnName("Pt_Img");
            entity.Property(e => e.PtLocation)
                .HasMaxLength(50)
                .HasColumnName("Pt_Location");
            entity.Property(e => e.PtName)
                .HasMaxLength(100)
                .HasColumnName("Pt_Name");
            entity.Property(e => e.PtNumber)
                .HasMaxLength(15)
                .HasColumnName("Pt_Number");
            entity.Property(e => e.PtResidence)
                .HasColumnType("text")
                .HasColumnName("Pt_Residence");
            entity.Property(e => e.PtTemp)
                .HasMaxLength(6)
                .HasColumnName("Pt_Temp");
            entity.Property(e => e.PtWeight)
                .HasMaxLength(6)
                .HasColumnName("Pt_Weight");
            entity.Property(e => e.Symptoms).HasColumnType("text");
            entity.Property(e => e.VisitType)
                .HasMaxLength(40)
                .HasColumnName("Visit_Type");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
