using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DuAnTruongTim.Models;

public partial class CheckQlgiaoVuContext : DbContext
{
    public CheckQlgiaoVuContext()
    {
    }

    public CheckQlgiaoVuContext(DbContextOptions<CheckQlgiaoVuContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<RequestFile> RequestFiles { get; set; }

    public virtual DbSet<Requet> Requets { get; set; }

    public virtual DbSet<Requetsdetailed> Requetsdetaileds { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleClaim> RoleClaims { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Account__3214EC07FA541DE1");

            entity.ToTable("Account");

            entity.Property(e => e.Academicrank).HasMaxLength(50);
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Citizenidentification).HasMaxLength(150);
            entity.Property(e => e.Class).HasMaxLength(50);
            entity.Property(e => e.Dateofbirth).HasColumnType("date");
            entity.Property(e => e.Degree).HasMaxLength(50);
            entity.Property(e => e.Emailaddress).HasMaxLength(50);
            entity.Property(e => e.Fullname).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(150);
            entity.Property(e => e.Phonenumber).HasMaxLength(50);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Schoolyear).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.IdDepartmentNavigation).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.IdDepartment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_account_Department_");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_account_role_");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departme__3214EC070690F445");

            entity.ToTable("Department");

            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Describe).HasMaxLength(50);
            entity.Property(e => e.TenDepartment).HasMaxLength(50);
        });

        modelBuilder.Entity<RequestFile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RequestF__3214EC0731BD4125");

            entity.ToTable("RequestFile");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.IdRequestNavigation).WithMany(p => p.RequestFiles)
                .HasForeignKey(d => d.IdRequest)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RequestFile_Mail_");
        });

        modelBuilder.Entity<Requet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Requets__3214EC07E4699005");

            entity.Property(e => e.Enddate).HasColumnType("date");
            entity.Property(e => e.Sentdate).HasColumnType("date");
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.IdComplainNavigation).WithMany(p => p.RequetIdComplainNavigations)
                .HasForeignKey(d => d.IdComplain)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mail_Account_");

            entity.HasOne(d => d.IdDepartmentNavigation).WithMany(p => p.Requets)
                .HasForeignKey(d => d.IdDepartment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Mail_Department_");

            entity.HasOne(d => d.IdHandleNavigation).WithMany(p => p.RequetIdHandleNavigations)
                .HasForeignKey(d => d.IdHandle)
                .HasConstraintName("FK_Mail_Account_1");
        });

        modelBuilder.Entity<Requetsdetailed>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Requetsd__3214EC079861C003");

            entity.ToTable("Requetsdetailed");

            entity.Property(e => e.Payday).HasColumnType("date");
            entity.Property(e => e.Reason).HasColumnType("text");
            entity.Property(e => e.Reply).HasColumnType("text");
            entity.Property(e => e.Sentdate).HasColumnType("date");

            entity.HasOne(d => d.IdRequestNavigation).WithMany(p => p.Requetsdetaileds)
                .HasForeignKey(d => d.IdRequest)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Detailedemail_Mail_");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC0762687E5F");

            entity.ToTable("Role");

            entity.Property(e => e.Describe).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<RoleClaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoleClai__3214EC07AE7EC214");

            entity.ToTable("RoleClaim");

            entity.Property(e => e.Describe).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasMany(d => d.IdAccounts).WithMany(p => p.IdRoleClaims)
                .UsingEntity<Dictionary<string, object>>(
                    "ManageRoleClaim",
                    r => r.HasOne<Account>().WithMany()
                        .HasForeignKey("IdAccount")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ManageRoleClaim_Account_"),
                    l => l.HasOne<RoleClaim>().WithMany()
                        .HasForeignKey("IdRoleClaim")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ManageRoleClaim_RoleClaim_"),
                    j =>
                    {
                        j.HasKey("IdRoleClaim", "IdAccount").HasName("PK__ManageRo__DA82820053CE2C5F");
                        j.ToTable("ManageRoleClaim");
                    });
        });
        base.OnModelCreating(modelBuilder);
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
