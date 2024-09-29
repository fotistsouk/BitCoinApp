﻿// <auto-generated />
using System;
using BTCapp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BTCapp.Migrations
{
    [DbContext(typeof(BTCDBContext))]
    [Migration("20240929164013_Newmigration")]
    partial class Newmigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.33")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BTCapp.Domain.Price", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<float>("CloseAmount")
                        .HasColumnType("real");

                    b.Property<DateTime>("TimePoint")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TimePoint")
                        .IsUnique();

                    b.ToTable("Prices");
                });
#pragma warning restore 612, 618
        }
    }
}
