using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UrbanHub.Data.Entities;

namespace UrbanHub.Data;

public partial class UrbanhubDbContext : DbContext
{
    public UrbanhubDbContext()
    {
    }

    public UrbanhubDbContext(DbContextOptions<UrbanhubDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Registration> Registrations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Verification> Verifications { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=UrbanhubDB");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Registration>(entity =>
        {
            entity.HasKey(e => e.Rid);

            entity.ToTable("registration");

            entity.Property(e => e.Rid).HasColumnName("rid");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Uid).HasName("PK_auth");

            entity.ToTable("user");

            entity.Property(e => e.Uid).HasColumnName("uid");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.JoinDate)
                .HasColumnType("datetime")
                .HasColumnName("join_date");
            entity.Property(e => e.Logid).HasColumnName("logid");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.Vid).HasColumnName("vid");
        });

        modelBuilder.Entity<Verification>(entity =>
        {
            entity.ToTable("verification");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AppliedOn)
                .HasColumnType("datetime")
                .HasColumnName("applied_on");
            entity.Property(e => e.Images).HasColumnName("images");
            entity.Property(e => e.LogId).HasColumnName("log_id");
            entity.Property(e => e.Method)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("method");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.Uid).HasColumnName("uid");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
