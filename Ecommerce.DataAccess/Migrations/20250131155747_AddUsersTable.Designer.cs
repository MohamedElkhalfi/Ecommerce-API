﻿// <auto-generated />
using System;
using Ecommerce.DataAccess.ConnexionDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Ecommerce.DataAccess.Migrations
{
    [DbContext(typeof(EcommerceContext))]
    [Migration("20250131155747_AddUsersTable")]
    partial class AddUsersTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Ecommerce")
                .HasAnnotation("EcommerceVersion", "1.1")
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Ecommerce.DataAccess.Model.Category", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Category", "Ecommerce");
                });

            modelBuilder.Entity("Ecommerce.DataAccess.Model.Client", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Address")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Email")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Name")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Username")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)");

                    b.HasKey("ID");

                    b.ToTable("Client", "Ecommerce");
                });

            modelBuilder.Entity("Ecommerce.DataAccess.Model.Order", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("Date")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(6, 2)");

                    b.Property<int?>("clientID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("clientID");

                    b.ToTable("Order", "Ecommerce");
                });

            modelBuilder.Entity("Ecommerce.DataAccess.Model.OrderItem", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("Order_ID")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(6, 2)");

                    b.Property<int?>("Product_ID")
                        .HasColumnType("int");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(6, 2)");

                    b.HasKey("ID");

                    b.HasIndex("Order_ID");

                    b.HasIndex("Product_ID");

                    b.ToTable("OrderItem", "Ecommerce");
                });

            modelBuilder.Entity("Ecommerce.DataAccess.Model.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("CategoryID")
                        .HasColumnType("int");

                    b.Property<decimal>("CurrentPrice")
                        .HasColumnType("decimal(10, 4)");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<bool?>("Is_Available")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<bool?>("Is_Promotion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<bool?>("Is_Selected")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("Name")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("PhotoName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(10, 4)");

                    b.HasKey("ID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Product", "Ecommerce");
                });

            modelBuilder.Entity("Ecommerce.DataAccess.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Users", "Ecommerce");
                });

            modelBuilder.Entity("Ecommerce.DataAccess.Model.Order", b =>
                {
                    b.HasOne("Ecommerce.DataAccess.Model.Client", "client")
                        .WithMany("Order_")
                        .HasForeignKey("clientID");

                    b.Navigation("client");
                });

            modelBuilder.Entity("Ecommerce.DataAccess.Model.OrderItem", b =>
                {
                    b.HasOne("Ecommerce.DataAccess.Model.Order", "Order_")
                        .WithMany("OrderItem_")
                        .HasForeignKey("Order_ID");

                    b.HasOne("Ecommerce.DataAccess.Model.Product", "Product_")
                        .WithMany()
                        .HasForeignKey("Product_ID");

                    b.Navigation("Order_");

                    b.Navigation("Product_");
                });

            modelBuilder.Entity("Ecommerce.DataAccess.Model.Product", b =>
                {
                    b.HasOne("Ecommerce.DataAccess.Model.Category", null)
                        .WithMany("Product_")
                        .HasForeignKey("CategoryID");
                });

            modelBuilder.Entity("Ecommerce.DataAccess.Model.Category", b =>
                {
                    b.Navigation("Product_");
                });

            modelBuilder.Entity("Ecommerce.DataAccess.Model.Client", b =>
                {
                    b.Navigation("Order_");
                });

            modelBuilder.Entity("Ecommerce.DataAccess.Model.Order", b =>
                {
                    b.Navigation("OrderItem_");
                });
#pragma warning restore 612, 618
        }
    }
}
