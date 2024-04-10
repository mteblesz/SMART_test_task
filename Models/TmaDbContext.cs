using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TMAWarehouse.Models;

public partial class TmaDbContext : DbContext
{
    public TmaDbContext()
    {
    }

    public TmaDbContext(DbContextOptions<TmaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<ItemGroup> ItemGroups { get; set; }

    public virtual DbSet<ItemStatus> ItemStatuses { get; set; }

    public virtual DbSet<MeasurementUnit> MeasurementUnits { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<RequestStatus> RequestStatuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=TMA_DB;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Items__727E838BF40FF854");

            entity.Property(e => e.ContactPerson).HasColumnType("text");
            entity.Property(e => e.Photo).HasMaxLength(1);
            entity.Property(e => e.PriceWithoutVat)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("PriceWithoutVAT");
            entity.Property(e => e.StorageLocation)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.ItemGroup).WithMany(p => p.Items)
                .HasForeignKey(d => d.ItemGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Items__ItemGroup__4F7CD00D");

            entity.HasOne(d => d.ItemStatus).WithMany(p => p.Items)
                .HasForeignKey(d => d.ItemStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Items__ItemStatu__5165187F");

            entity.HasOne(d => d.MeasurementUnit).WithMany(p => p.Items)
                .HasForeignKey(d => d.MeasurementUnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Items__Measureme__5070F446");
        });

        modelBuilder.Entity<ItemGroup>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK__ItemGrou__149AF30AAF9897E9");

            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.GroupName)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ItemStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__ItemStat__C8EE2063011C8370");

            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MeasurementUnit>(entity =>
        {
            entity.HasKey(e => e.UnitId).HasName("PK__Measurem__44F5ECB53CE6D219");

            entity.Property(e => e.UnitName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__Requests__33A8517A84988A65");

            entity.Property(e => e.Comment).HasColumnType("text");
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PriceWithoutVat)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("PriceWithoutVAT");

            entity.HasOne(d => d.Item).WithMany(p => p.Requests)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requests__ItemId__5629CD9C");

            entity.HasOne(d => d.MeasurementUnit).WithMany(p => p.Requests)
                .HasForeignKey(d => d.MeasurementUnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requests__Measur__571DF1D5");

            entity.HasOne(d => d.RequestStatus).WithMany(p => p.Requests)
                .HasForeignKey(d => d.RequestStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requests__Reques__5812160E");
        });

        modelBuilder.Entity<RequestStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__RequestS__C8EE2063386429C3");

            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
