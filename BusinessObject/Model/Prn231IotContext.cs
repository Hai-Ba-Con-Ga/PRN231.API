using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BusinessObject.Model;

public partial class Prn231IotContext : DbContext
{
    public Prn231IotContext()
    {
    }

    public Prn231IotContext(DbContextOptions<Prn231IotContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<CollectedDataType> CollectedDataTypes { get; set; }

    public virtual DbSet<CollectedDatum> CollectedData { get; set; }

    public virtual DbSet<Device> Devices { get; set; }

    public virtual DbSet<DeviceType> DeviceTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(GetConnectionString());
    }
    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
        var strConn = config.GetConnectionString("DefaultConnection");
        return strConn;
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Account__349DA586D8414DDF");

            entity.Property(e => e.CreatedBy).HasDefaultValue(0);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.UpdatedBy).HasDefaultValue(0);
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<CollectedDataType>(entity =>
        {
            entity.HasKey(e => e.CollectedDataTypeId).HasName("PK__Collecte__74FFB6EA1C6300B3");

            entity.Property(e => e.CreatedBy).HasDefaultValue(0);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.UpdatedBy).HasDefaultValue(0);
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<CollectedDatum>(entity =>
        {
            entity.HasKey(e => e.CollectedDataId).HasName("PK__Collecte__E31677F1592A3CA8");

            entity.Property(e => e.CreatedBy).HasDefaultValue(0);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.UpdatedBy).HasDefaultValue(0);
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.CollectedDataType).WithMany(p => p.CollectedData)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Collected__Colle__4E88ABD4");

            entity.HasOne(d => d.Device).WithMany(p => p.CollectedData)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Collected__Devic__4F7CD00D");
        });

        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasKey(e => e.DeviceId).HasName("PK__Device__49E12331158B8661");

            entity.Property(e => e.CreatedBy).HasDefaultValue(0);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.UpdatedBy).HasDefaultValue(0);
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.DeviceType).WithMany(p => p.Devices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Device__DeviceTy__45F365D3");

            entity.HasOne(d => d.Owner).WithMany(p => p.Devices)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Device__OwnerID__44FF419A");
        });

        modelBuilder.Entity<DeviceType>(entity =>
        {
            entity.HasKey(e => e.DeviceTypeId).HasName("PK__DeviceTy__07A6C7160B313C24");

            entity.Property(e => e.CreatedBy).HasDefaultValue(0);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.UpdatedBy).HasDefaultValue(0);
            entity.Property(e => e.UpdatedDate).HasDefaultValueSql("(getdate())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}