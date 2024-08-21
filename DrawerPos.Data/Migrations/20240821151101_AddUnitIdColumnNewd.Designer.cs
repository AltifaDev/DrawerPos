﻿// <auto-generated />
using System;
using DrawerPos.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DrawerPos.Data.Migrations
{
    [DbContext(typeof(DrawerPosDbContext))]
    [Migration("20240821151101_AddUnitIdColumnNewd")]
    partial class AddUnitIdColumnNewd
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DrawerPos.Shared.BillNumber", b =>
                {
                    b.Property<int>("BillNumberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("BillNumberID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BillNumberId"));

                    b.Property<string>("BillNo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("BillNumberId");

                    b.ToTable("BillNumbers");
                });

            modelBuilder.Entity("DrawerPos.Shared.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CategoryID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CategoryId")
                        .HasName("PK__Categori__19093A2BA724DE66");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("DrawerPos.Shared.Company", b =>
                {
                    b.Property<int>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CompanyId"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Branch")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("BranchId")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("CompanyName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Facebookid")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("facebookid");

                    b.Property<string>("LineId")
                        .HasMaxLength(125)
                        .HasColumnType("nvarchar(125)");

                    b.Property<string>("LogoImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<bool?>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("TexId")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("TiktokId")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Website")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CompanyId")
                        .HasName("PK__Company__2D971CACF8183BE3");

                    b.ToTable("Company", (string)null);
                });

            modelBuilder.Entity("DrawerPos.Shared.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CustomerID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("CustomerId")
                        .HasName("PK__Customer__A4AE64B8B5877ACA");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("DrawerPos.Shared.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("EmployeeID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Position")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("StoreId")
                        .HasColumnType("int")
                        .HasColumnName("StoreID");

                    b.HasKey("EmployeeId")
                        .HasName("PK__Employee__7AD04FF1AF77167D");

                    b.HasIndex(new[] { "StoreId" }, "IX_Employees_StoreID");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("DrawerPos.Shared.Ingredient", b =>
                {
                    b.Property<int>("IngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IngredientId"));

                    b.Property<string>("IngredientName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UnitId")
                        .HasColumnType("int");

                    b.HasKey("IngredientId");

                    b.HasIndex("UnitId");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("DrawerPos.Shared.IngredientStock", b =>
                {
                    b.Property<int>("IngredientStockId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IngredientStockId"));

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("IngredientId")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuantityInStock")
                        .HasColumnType("int");

                    b.Property<int>("ReorderPoint")
                        .HasColumnType("int");

                    b.HasKey("IngredientStockId");

                    b.HasIndex("IngredientId");

                    b.ToTable("IngredientStocks");
                });

            modelBuilder.Entity("DrawerPos.Shared.Inventory", b =>
                {
                    b.Property<int>("InventoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("InventoryID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InventoryId"));

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("datetime");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int?>("StoreId")
                        .HasColumnType("int")
                        .HasColumnName("StoreID");

                    b.HasKey("InventoryId")
                        .HasName("PK__Inventor__F5FDE6D3F4CA65FE");

                    b.HasIndex(new[] { "ProductId" }, "IX_Inventory_ProductID");

                    b.HasIndex(new[] { "StoreId" }, "IX_Inventory_StoreID");

                    b.ToTable("Inventory", (string)null);
                });

            modelBuilder.Entity("DrawerPos.Shared.MethodPayment", b =>
                {
                    b.Property<int>("QrId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QrId"));

                    b.Property<string>("MethodName")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("MethodNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MethodStatus")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MethodType")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("QrId");

                    b.ToTable("MethodPayments");
                });

            modelBuilder.Entity("DrawerPos.Shared.OrderItem", b =>
                {
                    b.Property<int>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderItemId"));

                    b.Property<string>("BillNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<decimal?>("Discount")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderItemId");

                    b.HasIndex("BillNo");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("DrawerPos.Shared.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentId"));

                    b.Property<decimal?>("Amount")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("BillNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime?>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaymentId");

                    b.HasIndex("BillNo");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("DrawerPos.Shared.PrinterSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ColorMode")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PaperReprint")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PaperSize")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PrintQuality")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PrinterName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id")
                        .HasName("PK__PrinterS__3214EC07E4B00300");

                    b.ToTable("PrinterSettings");
                });

            modelBuilder.Entity("DrawerPos.Shared.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("CategoryID");

                    b.Property<decimal>("CostPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameDisplay")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<string>("ProductBarcode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ProductSize")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Unit")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ProductId")
                        .HasName("PK__Products__B40CC6ED470F2E53");

                    b.HasIndex(new[] { "CategoryId" }, "IX_Products_CategoryID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("DrawerPos.Shared.ProductIngredient", b =>
                {
                    b.Property<int>("ProductIngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductIngredientId"));

                    b.Property<int>("IngredientId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ProductIngredientId");

                    b.HasIndex("IngredientId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductIngredients");
                });

            modelBuilder.Entity("DrawerPos.Shared.ReceiptHeader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Header1")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Header2")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Header3")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Header4")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Header5")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Header6")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("HeaderImg")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id")
                        .HasName("PK__ReceiptH__3214EC076B06E20C");

                    b.ToTable("ReceiptHeader", (string)null);
                });

            modelBuilder.Entity("DrawerPos.Shared.Shift", b =>
                {
                    b.Property<int>("ShiftId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ShiftID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShiftId"));

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int")
                        .HasColumnName("EmployeeID");

                    b.Property<TimeOnly?>("EndTime")
                        .HasColumnType("time");

                    b.Property<DateTime?>("ShiftDate")
                        .HasColumnType("datetime");

                    b.Property<TimeOnly?>("StartTime")
                        .HasColumnType("time");

                    b.HasKey("ShiftId")
                        .HasName("PK__Shifts__C0A838E136860778");

                    b.HasIndex(new[] { "EmployeeId" }, "IX_Shifts_EmployeeID");

                    b.ToTable("Shifts");
                });

            modelBuilder.Entity("DrawerPos.Shared.Store", b =>
                {
                    b.Property<int>("StoreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("StoreID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StoreId"));

                    b.Property<string>("Location")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("StoreName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("StoreId")
                        .HasName("PK__Stores__3B82F0E1DBDBED6F");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("DrawerPos.Shared.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionId"));

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("TransactionDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.HasKey("TransactionId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("DrawerPos.Shared.TransactionDetail", b =>
                {
                    b.Property<int>("TransactionDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionDetailId"));

                    b.Property<int>("IngredientId")
                        .HasColumnType("int");

                    b.Property<decimal>("PricePerUnit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("TransactionId")
                        .HasColumnType("int");

                    b.HasKey("TransactionDetailId");

                    b.HasIndex("ProductId");

                    b.HasIndex("TransactionId");

                    b.ToTable("TransactionDetails");
                });

            modelBuilder.Entity("DrawerPos.Shared.Unit", b =>
                {
                    b.Property<int>("UnitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UnitId"));

                    b.Property<string>("UnitName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UnitId");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("Order", b =>
                {
                    b.Property<string>("BillNo")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<decimal?>("Discount")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<DateTime>("OrderDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("PaymentMethod")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("StoreId")
                        .HasColumnType("int");

                    b.Property<decimal?>("SubTotal")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal?>("Total")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal?>("TotalAmount")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal?>("TotalDiscount")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("BillNo");

                    b.HasIndex("CustomerId");

                    b.HasIndex("StoreId");

                    b.HasIndex(new[] { "BillNo" }, "UQ__Orders__11F284183B3D694E")
                        .IsUnique();

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("DrawerPos.Shared.Employee", b =>
                {
                    b.HasOne("DrawerPos.Shared.Store", "Store")
                        .WithMany("Employees")
                        .HasForeignKey("StoreId")
                        .HasConstraintName("FK__Employees__Store__47DBAE45");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("DrawerPos.Shared.Ingredient", b =>
                {
                    b.HasOne("DrawerPos.Shared.Unit", "Unit")
                        .WithMany("Ingredients")
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("DrawerPos.Shared.IngredientStock", b =>
                {
                    b.HasOne("DrawerPos.Shared.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("DrawerPos.Shared.Inventory", b =>
                {
                    b.HasOne("DrawerPos.Shared.Product", "Product")
                        .WithMany("Inventories")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK__Inventory__Produ__5165187F");

                    b.HasOne("DrawerPos.Shared.Store", "Store")
                        .WithMany("Inventories")
                        .HasForeignKey("StoreId")
                        .HasConstraintName("FK__Inventory__Store__5070F446");

                    b.Navigation("Product");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("DrawerPos.Shared.OrderItem", b =>
                {
                    b.HasOne("Order", "BillNoNavigation")
                        .WithMany("OrderItems")
                        .HasForeignKey("BillNo")
                        .IsRequired();

                    b.HasOne("DrawerPos.Shared.Product", "Product")
                        .WithMany("OrderItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BillNoNavigation");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("DrawerPos.Shared.Payment", b =>
                {
                    b.HasOne("Order", "BillNoNavigation")
                        .WithMany("Payments")
                        .HasForeignKey("BillNo")
                        .IsRequired();

                    b.Navigation("BillNoNavigation");
                });

            modelBuilder.Entity("DrawerPos.Shared.Product", b =>
                {
                    b.HasOne("DrawerPos.Shared.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__Products__Catego__3B75D760");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("DrawerPos.Shared.ProductIngredient", b =>
                {
                    b.HasOne("DrawerPos.Shared.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DrawerPos.Shared.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingredient");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("DrawerPos.Shared.Shift", b =>
                {
                    b.HasOne("DrawerPos.Shared.Employee", "Employee")
                        .WithMany("Shifts")
                        .HasForeignKey("EmployeeId")
                        .HasConstraintName("FK__Shifts__Employee__4AB81AF0");

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("DrawerPos.Shared.TransactionDetail", b =>
                {
                    b.HasOne("DrawerPos.Shared.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DrawerPos.Shared.Transaction", "Transaction")
                        .WithMany("TransactionDetails")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingredient");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("Order", b =>
                {
                    b.HasOne("DrawerPos.Shared.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId");

                    b.HasOne("DrawerPos.Shared.Store", null)
                        .WithMany("Orders")
                        .HasForeignKey("StoreId");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("DrawerPos.Shared.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("DrawerPos.Shared.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("DrawerPos.Shared.Employee", b =>
                {
                    b.Navigation("Shifts");
                });

            modelBuilder.Entity("DrawerPos.Shared.Product", b =>
                {
                    b.Navigation("Inventories");

                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("DrawerPos.Shared.Store", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("Inventories");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("DrawerPos.Shared.Transaction", b =>
                {
                    b.Navigation("TransactionDetails");
                });

            modelBuilder.Entity("DrawerPos.Shared.Unit", b =>
                {
                    b.Navigation("Ingredients");
                });

            modelBuilder.Entity("Order", b =>
                {
                    b.Navigation("OrderItems");

                    b.Navigation("Payments");
                });
#pragma warning restore 612, 618
        }
    }
}
