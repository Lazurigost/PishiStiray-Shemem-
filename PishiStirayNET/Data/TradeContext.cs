using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PishiStirayNET.Models.DbEntities;

namespace PishiStirayNET;

public partial class TradeContext : DbContext
{
    public TradeContext()
    {
    }

    public TradeContext(DbContextOptions<TradeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Delivery> Deliveries { get; set; }

    public virtual DbSet<Orderproduct> Orderproducts { get; set; }

    public virtual DbSet<Orderuser> Orderusers { get; set; }

    public virtual DbSet<ProductDB> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<Unit> ProductDeliveries { get; set; }

    public virtual DbSet<Manufacturer> ProductManufacturers { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<UserDB> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=pishi_;password=XCR6hs2M;database=trade", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.25-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Delivery>(entity =>
        {
            entity.HasKey(e => e.IdpickupPoint).HasName("PRIMARY");

            entity.ToTable("delivery");

            entity.Property(e => e.IdpickupPoint)
                .ValueGeneratedNever()
                .HasColumnName("IDPickupPoint");
            entity.Property(e => e.City)
                .HasMaxLength(45)
                .HasColumnName("city");
            entity.Property(e => e.House).HasColumnName("house");
            entity.Property(e => e.PickupPoint).HasColumnName("pickupPoint");
            entity.Property(e => e.Street)
                .HasMaxLength(80)
                .HasColumnName("street");
        });

        modelBuilder.Entity<Orderproduct>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ProductArticleNumber })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("orderproduct");

            entity.HasIndex(e => e.ProductArticleNumber, "fk_article_idx");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ProductArticleNumber)
                .HasMaxLength(100)
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderproducts)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_orderID");

            entity.HasOne(d => d.ProductArticleNumberNavigation).WithMany(p => p.Orderproducts)
                .HasForeignKey(d => d.ProductArticleNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_article");
        });

        modelBuilder.Entity<Orderuser>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.ToTable("orderuser");

            entity.HasIndex(e => e.OrderPickupPoint, "FKPickupPoint_idx");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.OrderStatus).HasColumnType("text");
            entity.Property(e => e.OrdererFio)
                .HasMaxLength(100)
                .HasColumnName("OrdererFIO");

            entity.HasOne(d => d.OrderPickupPointNavigation).WithMany(p => p.Orderusers)
                .HasForeignKey(d => d.OrderPickupPoint)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKPickupPoint");
        });

        modelBuilder.Entity<ProductDB>(entity =>
        {
            entity.HasKey(e => e.ProductArticleNumber).HasName("PRIMARY");

            entity.ToTable("product");

            entity.HasIndex(e => e.ProductCategory, "fk_category_idx");

            entity.HasIndex(e => e.ProductDelivery, "fk_delivery_idx");

            entity.HasIndex(e => e.ProductManufacturer, "fk_manufacturers_idx");

            entity.Property(e => e.ProductArticleNumber)
                .HasMaxLength(100)
                .UseCollation("utf8_general_ci")
                .HasCharSet("utf8");
            entity.Property(e => e.ProductCost).HasPrecision(10);
            entity.Property(e => e.ProductDescription).HasColumnType("text");
            entity.Property(e => e.ProductName).HasColumnType("text");
            entity.Property(e => e.ProductPhoto).HasColumnType("text");
            entity.Property(e => e.ProductStatus)
                .HasColumnType("text")
                .UseCollation("utf8mb4_bin");

            entity.HasOne(d => d.ProductCategoryNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_category");

            entity.HasOne(d => d.ProductDeliveryNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductDelivery)
                .HasConstraintName("fk_delivery");

            entity.HasOne(d => d.ProductManufacturerNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductManufacturer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_manufacturers");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.IdproductCategory).HasName("PRIMARY");

            entity.ToTable("product_category");

            entity.Property(e => e.IdproductCategory)
                .ValueGeneratedNever()
                .HasColumnName("idproduct_category");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(45)
                .HasColumnName("category_name");
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.IdproductDeliveries).HasName("PRIMARY");

            entity.ToTable("product_deliveries");

            entity.Property(e => e.IdproductDeliveries)
                .ValueGeneratedNever()
                .HasColumnName("idproduct_deliveries");
            entity.Property(e => e.DeliveryName)
                .HasMaxLength(45)
                .HasColumnName("delivery_name");
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.IdproductManufacturers).HasName("PRIMARY");

            entity.ToTable("product_manufacturers");

            entity.Property(e => e.IdproductManufacturers)
                .ValueGeneratedNever()
                .HasColumnName("idproduct_manufacturers");
            entity.Property(e => e.ManufacturerName)
                .HasMaxLength(45)
                .HasColumnName("manufacturer_name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity.ToTable("role");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<UserDB>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.UserRole, "UserRole");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.UserLogin).HasColumnType("text");
            entity.Property(e => e.UserName).HasMaxLength(100);
            entity.Property(e => e.UserPassword).HasColumnType("text");
            entity.Property(e => e.UserPatronymic).HasMaxLength(100);
            entity.Property(e => e.UserSurname).HasMaxLength(100);

            entity.HasOne(d => d.UserRoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
