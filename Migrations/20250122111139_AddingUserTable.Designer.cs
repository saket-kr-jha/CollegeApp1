﻿// <auto-generated />
using System;
using DotNetCore_New.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DotNetCore_New.Migrations
{
    [DbContext(typeof(CollegeDBContext))]
    [Migration("20250122111139_AddingUserTable")]
    partial class AddingUserTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DotNetCore_New.Data.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepartmentId"));

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("DepartmentId");

                    b.ToTable("Departments", (string)null);

                    b.HasData(
                        new
                        {
                            DepartmentId = 1,
                            DepartmentName = "CSE",
                            Description = "Computer Science Engineering"
                        },
                        new
                        {
                            DepartmentId = 2,
                            DepartmentName = "ECE",
                            Description = "Electronics and Communication Engineering"
                        });
                });

            modelBuilder.Entity("DotNetCore_New.Data.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentId"));

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("StudentEmail")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("StudentName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("StudentPhone")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("StudentId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Students", (string)null);

                    b.HasData(
                        new
                        {
                            StudentId = 1,
                            DOB = new DateTime(1995, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StudentEmail = "saketkumar180@gmail.com",
                            StudentName = "Saket Jha",
                            StudentPhone = "9177881115"
                        },
                        new
                        {
                            StudentId = 2,
                            DOB = new DateTime(1994, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StudentEmail = "subhamsingh@gmail.com",
                            StudentName = "Subham singh",
                            StudentPhone = "8437074075"
                        },
                        new
                        {
                            StudentId = 3,
                            DOB = new DateTime(1994, 11, 19, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StudentEmail = "tinkusingh@gmail.com",
                            StudentName = "Tinku Singh",
                            StudentPhone = "8978246007"
                        },
                        new
                        {
                            StudentId = 4,
                            DOB = new DateTime(1994, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StudentEmail = "aruntogi@gmail.com",
                            StudentName = "Arun Togi",
                            StudentPhone = "8897534078"
                        });
                });

            modelBuilder.Entity("DotNetCore_New.Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("DotNetCore_New.Data.Student", b =>
                {
                    b.HasOne("DotNetCore_New.Data.Department", "Department")
                        .WithMany("Students")
                        .HasForeignKey("DepartmentId")
                        .HasConstraintName("FK_Students_Department");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("DotNetCore_New.Data.Department", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
