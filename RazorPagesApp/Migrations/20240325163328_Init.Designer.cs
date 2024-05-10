﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PizzaShop.Data;

#nullable disable

namespace PizzaShop.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240325163328_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("PizzaShop.Data.Model.PizzaOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Beef")
                        .HasColumnType("bit");

                    b.Property<bool>("Cheese")
                        .HasColumnType("bit");

                    b.Property<bool>("Ham")
                        .HasColumnType("bit");

                    b.Property<bool>("Mushroom")
                        .HasColumnType("bit");

                    b.Property<bool>("Pepperoni")
                        .HasColumnType("bit");

                    b.Property<string>("PizzaName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("PizzaPrice")
                        .HasColumnType("real");

                    b.Property<bool>("TomatoSauce")
                        .HasColumnType("bit");

                    b.Property<bool>("Tuna")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("pizzaOrders");
                });
#pragma warning restore 612, 618
        }
    }
}