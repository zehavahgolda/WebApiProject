using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Entity;
namespace Repository;

public partial class Store_329391924Context : DbContext
{
    public Store_329391924Context()
    {
    }

    public Store_329391924Context(DbContextOptions<Store_329391924Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<OrdeItem> OrdeItems { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=desktop-t8jm6mu;Database=Store_329391924Context;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CatogeryId);

            entity.Property(e => e.CatogeryId)
                .ValueGeneratedNever()
                .HasColumnName("Catogery_Id");
            entity.Property(e => e.CatogeryName)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("Catogery_Name");
        });

        modelBuilder.Entity<OrdeItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId);

            entity.ToTable("Orde_Item");

            entity.HasIndex(e => e.OrderId, "IX_Orde_Item_Order_Id");

            entity.HasIndex(e => e.ProductId, "IX_Orde_Item_Product_Id");

            entity.Property(e => e.OrderItemId)
                .ValueGeneratedNever()
                .HasColumnName("Order_Item_Id");
            entity.Property(e => e.OrderId).HasColumnName("Order_Id");
            entity.Property(e => e.ProductId).HasColumnName("Product_Id");

            entity.HasOne(d => d.Order).WithMany(p => p.OrdeItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_Orde_to_user");

            entity.HasOne(d => d.Product).WithMany(p => p.OrdeItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_to_product_from_orderitem");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.HasIndex(e => e.UserId, "IX_Order_User_Id");

            entity.Property(e => e.OrderId)
                .ValueGeneratedNever()
                .HasColumnName("Order_Id");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Paid");
            entity.Property(e => e.OrderSum).HasColumnName("Order_Sum");
            entity.Property(e => e.OredrDate).HasColumnName("Oredr_Date");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Order_to_user");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(e => e.CategoryId, "IX_Products_Category_Id");

            entity.Property(e => e.ProductId)
                .ValueGeneratedNever()
                .HasColumnName("Product_Id");
            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.Color)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.ImgUrl)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Material)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.ProductName)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasColumnName("Product_name");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Table_catogery_to_product");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(25)
                .IsFixedLength();
            entity.Property(e => e.FirstName)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("First_Name");
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("Last_Name");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsFixedLength();
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
