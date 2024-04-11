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

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<RequestStatus> RequestStatuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=TMA_DB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Items__727E838B4C1C5106");

            entity.Property(e => e.ContactPerson).HasColumnType("text");
            entity.Property(e => e.ItemName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PriceWithoutVat).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.StorageLocation)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.ItemGroup).WithMany(p => p.Items)
                .HasForeignKey(d => d.ItemGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Items__ItemGroup__5165187F");

            entity.HasOne(d => d.ItemStatus).WithMany(p => p.Items)
                .HasForeignKey(d => d.ItemStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Items__ItemStatu__534D60F1");

            entity.HasOne(d => d.MeasurementUnit).WithMany(p => p.Items)
                .HasForeignKey(d => d.MeasurementUnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Items__Measureme__52593CB8");

            entity.HasOne(d => d.Photo).WithMany(p => p.Items)
                .HasForeignKey(d => d.PhotoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Items__PhotoId__5441852A");
        });

        modelBuilder.Entity<ItemGroup>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK__ItemGrou__149AF30AD7C9DF7F");

            entity.Property(e => e.GroupId).HasColumnName("GroupID");
            entity.Property(e => e.GroupName)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ItemStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__ItemStat__C8EE2063C10AF11E");

            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MeasurementUnit>(entity =>
        {
            entity.HasKey(e => e.UnitId).HasName("PK__Measurem__44F5ECB5FCE19A5E");

            entity.Property(e => e.UnitName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.HasKey(e => e.PhotoId).HasName("PK__Photos__21B7B5E2D4CD0FFA");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__Requests__33A8517A3722BEBD");

            entity.Property(e => e.Comment).HasColumnType("text");
            entity.Property(e => e.EmployeeName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PriceWithoutVat).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Item).WithMany(p => p.Requests)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requests__ItemId__59063A47");

            entity.HasOne(d => d.MeasurementUnit).WithMany(p => p.Requests)
                .HasForeignKey(d => d.MeasurementUnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requests__Measur__59FA5E80");

            entity.HasOne(d => d.RequestStatus).WithMany(p => p.Requests)
                .HasForeignKey(d => d.RequestStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Requests__Reques__5AEE82B9");
        });

        modelBuilder.Entity<RequestStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__RequestS__C8EE20637D7C9077");

            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
