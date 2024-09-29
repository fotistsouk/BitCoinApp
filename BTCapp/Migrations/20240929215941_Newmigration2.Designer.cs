﻿// <auto-generated />
using System;
using BTCapp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BTCapp.Migrations
{
    [DbContext(typeof(BTCDBContext))]
    [Migration("20240929215941_Newmigration2")]
    partial class Newmigration2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.33")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BTCapp.Domain.Price", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<float>("CloseAmount")
                        .HasColumnType("float");

                    b.Property<DateTime>("TimePoint")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("TimePoint")
                        .IsUnique();

                    b.ToTable("btcprice", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
