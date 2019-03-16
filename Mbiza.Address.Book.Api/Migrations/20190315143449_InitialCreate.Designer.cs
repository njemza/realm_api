﻿// <auto-generated />
using System;
using Mbiza.Address.Book.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Mbiza.Address.Book.Api.Migrations
{
    [DbContext(typeof(AddressBookDBContext))]
    [Migration("20190315143449_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Mbiza.Address.Book.Api.Models.AddressBook", b =>
                {
                    b.Property<int>("AddressBookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("AddressBookId")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EmailAddress")
                        .HasColumnName("EmailAddress");

                    b.Property<string>("FirstName")
                        .HasColumnName("FirstName");

                    b.Property<string>("HomeNumber")
                        .HasColumnName("HomeNumber");

                    b.Property<string>("LastName")
                        .HasColumnName("LastName");

                    b.Property<string>("MobileNumber")
                        .HasColumnName("MobileNumber");

                    b.Property<string>("OfficeNumber")
                        .HasColumnName("OfficeNumber");

                    b.HasKey("AddressBookId");
                    
                    b.ToTable("AddressBook");
                });
#pragma warning restore 612, 618
        }
    }
}
