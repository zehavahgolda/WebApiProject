using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Entity; 

namespace Repository;

public partial class Store_329391924Context : DbContext
{
    public Store_329391924Context() { }

    public Store_329391924Context(DbContextOptions<Store_329391924Context> options)
        : base(options) { }

    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<OrdeItem> OrdeItems { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Rating> Ratings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=desktop-t8jm6mu;Database=Store_329391924Context;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId); 

            entity.Property(e => e.CategoryId)
                .HasColumnName("Category_Id"); 

            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .HasColumnName("Category_Name");
        });

    
        modelBuilder.Entity<OrdeItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId);
            entity.ToTable("Orde_Item");

            entity.Property(e => e.OrderItemId)
                .HasColumnName("Order_Item_Id"); 

            entity.Property(e => e.OrderId).HasColumnName("Order_Id");
            entity.Property(e => e.ProductId).HasColumnName("Product_Id");

            entity.HasOne(d => d.Order).WithMany(p => p.OrdeItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_OrderItem_Order");

            entity.HasOne(d => d.Product).WithMany(p => p.OrdeItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_OrderItem_Products");
        });

     
        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.Property(e => e.OrderId)
                .HasColumnName("Order_Id"); 
            entity.Property(e => e.OrderSum).HasColumnName("Order_Sum");
            entity.Property(e => e.OredrDate).HasColumnName("Oredr_Date");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Order_Users");
        });

        // --- Product ---
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId);

            entity.Property(e => e.ProductId)
                .HasColumnName("Product_Id"); 
            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.ProductName).HasColumnName("Product_name").HasMaxLength(200);
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Products_Categories");
        });

       
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID"); 

            entity.Property(e => e.FirstName).HasColumnName("First_Name").HasMaxLength(50);
            entity.Property(e => e.LastName).HasColumnName("Last_Name").HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(100);
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.ToTable("RATING");
            entity.HasKey(e => e.RatingId);
            entity.Property(e => e.RatingId).HasColumnName("RATING_ID"); 
            entity.Property(e => e.RecordDate).HasColumnName("Record_Date").HasDefaultValueSql("(getdate())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}