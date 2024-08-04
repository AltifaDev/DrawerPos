using System;
using System.Collections.Generic;
using DrawerPos.Shared;
using Microsoft.EntityFrameworkCore;

namespace DrawerPos.Data
{
    public partial class DrawerPosDbContext : DbContext
    {
        public DrawerPosDbContext()
        {
        }

        public DrawerPosDbContext(DbContextOptions<DrawerPosDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BillNumber> BillNumbers { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<PrinterSetting> PrinterSettings { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ReceiptHeader> ReceiptHeaders { get; set; }
        public virtual DbSet<Shift> Shifts { get; set; }
        public virtual DbSet<Store> Stores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Data Source=ALFA\\SQLEXPRESS;Initial Catalog=DrawerData;Integrated Security=True;Encrypt=False",
                    b => b.MigrationsAssembly("DrawerPos.Data"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BillNumber>(entity =>
            {
                entity.Property(e => e.BillNumberId).HasColumnName("BillNumberID");
                entity.Property(e => e.BillNo).HasMaxLength(50);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A2BA724DE66");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
                entity.Property(e => e.CategoryName).HasMaxLength(100);
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.CompanyId).HasName("PK__Company__2D971CACF8183BE3");

                entity.ToTable("Company");

                entity.Property(e => e.Branch).HasMaxLength(255);
                entity.Property(e => e.BranchId).HasMaxLength(255);
                entity.Property(e => e.CompanyName).HasMaxLength(255);
                entity.Property(e => e.Email).HasMaxLength(255);
                entity.Property(e => e.Facebookid)
                    .HasMaxLength(255)
                    .HasColumnName("facebookid");
                entity.Property(e => e.LineId).HasMaxLength(125);
                entity.Property(e => e.Phone).HasMaxLength(15);
                entity.Property(e => e.Status).HasDefaultValue(false);
                entity.Property(e => e.TexId).HasMaxLength(25);
                entity.Property(e => e.TiktokId).HasMaxLength(255);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B8B5877ACA");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.FirstName).HasMaxLength(100);
                entity.Property(e => e.LastName).HasMaxLength(100);
                entity.Property(e => e.Phone).HasMaxLength(20);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF1AF77167D");

                entity.HasIndex(e => e.StoreId, "IX_Employees_StoreID");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
                entity.Property(e => e.FirstName).HasMaxLength(100);
                entity.Property(e => e.LastName).HasMaxLength(100);
                entity.Property(e => e.Position).HasMaxLength(100);
                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.HasOne(d => d.Store).WithMany(p => p.Employees)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK__Employees__Store__47DBAE45");
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(e => e.InventoryId).HasName("PK__Inventor__F5FDE6D3F4CA65FE");

                entity.ToTable("Inventory");

                entity.HasIndex(e => e.ProductId, "IX_Inventory_ProductID");

                entity.HasIndex(e => e.StoreId, "IX_Inventory_StoreID");

                entity.Property(e => e.InventoryId).HasColumnName("InventoryID");
                entity.Property(e => e.LastUpdated).HasColumnType("datetime");
                entity.Property(e => e.ProductId).HasColumnName("ProductID");
                entity.Property(e => e.StoreId).HasColumnName("StoreID");

                entity.HasOne(d => d.Product).WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__Inventory__Produ__5165187F");

                entity.HasOne(d => d.Store).WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK__Inventory__Store__5070F446");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.BillNo, "UQ__Orders__11F284183B3D694E").IsUnique();

                entity.Property(e => e.BillNo).HasMaxLength(20);
                entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.OrderDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.PaymentMethod).HasMaxLength(50);
                entity.Property(e => e.SubTotal).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.TotalDiscount).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => e.OrderItemId);

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Discount)
                    .HasColumnType("decimal(18, 2)");

                // Configure the relationship between OrderItem and Order
                entity.HasOne(d => d.BillNoNavigation)
                    .WithMany(o => o.OrderItems)
                    .HasForeignKey(d => d.BillNo)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                // Configure the relationship between OrderItem and Product
                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ProductId);
            });

            // Configuration for Payment
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.PaymentId); // Primary Key for Payment

                // Configure the relationship between Payment and Order
                entity.HasOne(d => d.BillNoNavigation)
                    .WithMany(o => o.Payments)
                    .HasForeignKey(d => d.BillNo)
                    .OnDelete(DeleteBehavior.ClientSetNull); // Ensure the relationship is maintained if the Order is deleted

                // Configure Amount property
                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18, 2)"); // Set column type with precision and scale

                // Additional property configurations if needed
            });


            modelBuilder.Entity<PrinterSetting>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__PrinterS__3214EC07E4B00300");

                entity.Property(e => e.ColorMode).HasMaxLength(50);
                entity.Property(e => e.PaperReprint).HasMaxLength(50);
                entity.Property(e => e.PaperSize).HasMaxLength(50);
                entity.Property(e => e.PrintQuality).HasMaxLength(50);
                entity.Property(e => e.PrinterName).HasMaxLength(100);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6ED470F2E53");

                entity.HasIndex(e => e.CategoryId, "IX_Products_CategoryID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
                entity.Property(e => e.CostPrice).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.ProductName).HasMaxLength(100);
                entity.Property(e => e.Status).HasMaxLength(20);
                entity.Property(e => e.Unit).HasMaxLength(50);

                entity.HasOne(d => d.Category).WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Products__Catego__3B75D760");
            });

            modelBuilder.Entity<ReceiptHeader>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__ReceiptH__3214EC076B06E20C");

                entity.ToTable("ReceiptHeader");

                entity.Property(e => e.Header1).HasMaxLength(255);
                entity.Property(e => e.Header2).HasMaxLength(255);
                entity.Property(e => e.Header3).HasMaxLength(255);
                entity.Property(e => e.Header4).HasMaxLength(255);
                entity.Property(e => e.Header5).HasMaxLength(255);
                entity.Property(e => e.Header6).HasMaxLength(255);
            });

            modelBuilder.Entity<Shift>(entity =>
            {
                entity.HasKey(e => e.ShiftId).HasName("PK__Shifts__C0A838E136860778");

                entity.HasIndex(e => e.EmployeeId, "IX_Shifts_EmployeeID");

                entity.Property(e => e.ShiftId).HasColumnName("ShiftID");
                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
                entity.Property(e => e.ShiftDate).HasColumnType("datetime");

                entity.HasOne(d => d.Employee).WithMany(p => p.Shifts)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__Shifts__Employee__4AB81AF0");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.HasKey(e => e.StoreId).HasName("PK__Stores__3B82F0E1DBDBED6F");

                entity.Property(e => e.StoreId).HasColumnName("StoreID");
                entity.Property(e => e.Location).HasMaxLength(255);
                entity.Property(e => e.Phone).HasMaxLength(20);
                entity.Property(e => e.StoreName).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
