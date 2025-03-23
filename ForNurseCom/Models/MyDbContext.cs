using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ForNurseCom.Models;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Dorm> Dorms { get; set; }

    public virtual DbSet<Drug> Drugs { get; set; }

    public virtual DbSet<DrugChange> DrugChanges { get; set; }

    public virtual DbSet<KeyRequest> KeyRequests { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserLog> UserLogs { get; set; }

    public virtual DbSet<Visit> Visits { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ELIEZER-ZOLA\\ZOLA2024; Database=Kmedic; User Id=zola;Password=Elwezer2024;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Thai_CI_AS");

        modelBuilder.Entity<Dorm>(entity =>
        {
            entity.HasKey(e => e.ContractId);

            entity.ToTable("Dorm");

            entity.Property(e => e.ContractId).HasMaxLength(20);
            entity.Property(e => e.Cashier).HasMaxLength(50);
            entity.Property(e => e.Cashortransfer).HasColumnName("cashortransfer");
            entity.Property(e => e.Checkedin).HasColumnType("datetime");
            entity.Property(e => e.Checkout)
                .HasColumnType("datetime")
                .HasColumnName("checkout");
            entity.Property(e => e.GuestId)
                .HasMaxLength(15)
                .HasColumnName("GuestID");
            entity.Property(e => e.GuestName).HasMaxLength(70);
            entity.Property(e => e.RatepDay).HasColumnType("money");
            entity.Property(e => e.Room).HasMaxLength(10);
            entity.Property(e => e.Total)
                .HasComputedColumnSql("([RatepDay]*[StayDuration])", true)
                .HasColumnType("money")
                .HasColumnName("total");
        });

        modelBuilder.Entity<Drug>(entity =>
        {
            entity.ToTable("Drug");

            entity.Property(e => e.Id)
                .HasMaxLength(20)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("id");
            entity.Property(e => e.MedLocation)
                .HasMaxLength(20)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("Med_Location");
            entity.Property(e => e.MedName)
                .HasMaxLength(70)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("Med_Name");
            entity.Property(e => e.MedQuantity).HasColumnName("Med_Quantity");
        });

        modelBuilder.Entity<DrugChange>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.MedLocation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("Med_Location");
            entity.Property(e => e.MedName)
                .HasMaxLength(70)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("Med_Name");
            entity.Property(e => e.MedQuantity).HasColumnName("Med_Quantity");
        });

        modelBuilder.Entity<KeyRequest>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("KeyRequest");

            entity.Property(e => e.Key)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("key");
            entity.Property(e => e.Username)
                .HasMaxLength(70)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.EmDept)
                .HasMaxLength(70)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("Em_Dept");
            entity.Property(e => e.EmImg)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("Em_Img");
            entity.Property(e => e.EmpId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("Emp_ID");
            entity.Property(e => e.EmpName)
                .HasMaxLength(70)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("Emp_Name");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StdId);

            entity.ToTable("Student");

            entity.Property(e => e.StdId)
                .HasMaxLength(15)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("Std_Id");
            entity.Property(e => e.StdFac)
                .HasMaxLength(70)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("Std_Fac");
            entity.Property(e => e.StdImg).HasColumnName("Std_Img");
            entity.Property(e => e.StdName)
                .HasMaxLength(70)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("Std_Name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("userId");
            entity.Property(e => e.UserPassword)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("userPassword");
            entity.Property(e => e.UserSalt)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("userSalt");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS");
        });

        modelBuilder.Entity<UserLog>(entity =>
        {
            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Logintime)
                .HasColumnType("datetime")
                .HasColumnName("logintime");
            entity.Property(e => e.UserName)
                .HasMaxLength(70)
                .HasColumnName("userName");
        });

        modelBuilder.Entity<Visit>(entity =>
        {
            entity.HasKey(e => e.VisitId).HasName("PK_Visit1");

            entity.ToTable("Visit");

            entity.Property(e => e.VisitId)
                .ValueGeneratedNever()
                .HasColumnName("VisitID");
            entity.Property(e => e.BodySystem)
                .HasMaxLength(80)
                .HasColumnName("Body_System");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.EmerAddress).HasColumnName("Emer_Address");
            entity.Property(e => e.EmerName)
                .HasMaxLength(80)
                .HasColumnName("Emer_Name");
            entity.Property(e => e.EmerPhone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("Emer_Phone");
            entity.Property(e => e.MedQa).HasColumnName("MedQA");
            entity.Property(e => e.MedQab).HasColumnName("MedQAB");
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
                .IsUnicode(false)
                .HasColumnName("Pt_Age");
            entity.Property(e => e.PtBp)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("Pt_BP");
            entity.Property(e => e.PtDept)
                .HasMaxLength(80)
                .HasColumnName("Pt_Dept");
            entity.Property(e => e.PtHeart)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("Pt_Heart");
            entity.Property(e => e.PtHeight)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("Pt_Height");
            entity.Property(e => e.PtId)
                .HasMaxLength(16)
                .IsUnicode(false)
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
                .IsUnicode(false)
                .HasColumnName("Pt_Number");
            entity.Property(e => e.PtResidence)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Pt_Residence");
            entity.Property(e => e.PtTemp)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("Pt_Temp");
            entity.Property(e => e.PtWeight)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("Pt_Weight");
            entity.Property(e => e.VisitType)
                .HasMaxLength(80)
                .HasColumnName("Visit_Type");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
