﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using _09_Calculate.Data;

#nullable disable

namespace _09_Calculate.Migrations
{
    [DbContext(typeof(CalculatorContext))]
    partial class CalculatorContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("_09_Calculate.Data.DataInputVariant", b =>
                {
                    b.Property<int>("ID_DataInputVariant")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Operand_1")
                        .HasColumnType("double");

                    b.Property<double>("Operand_2")
                        .HasColumnType("double");

                    b.Property<string>("Result")
                        .HasColumnType("varchar(128)");

                    b.Property<int>("Type_operation")
                        .HasColumnType("int");

                    b.HasKey("ID_DataInputVariant");

                    b.ToTable("DataInputVariants");
                });
#pragma warning restore 612, 618
        }
    }
}