﻿// <auto-generated />
using System;
using EmployeeManagement.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EmployeeManagement.Migrations
{
    [DbContext(typeof(EmployeeDbContext))]
    [Migration("20220310124450_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.2");

            modelBuilder.Entity("CourseInternalEmployee", b =>
                {
                    b.Property<Guid>("AttendedCoursesId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EmployeesThatAttendedId")
                        .HasColumnType("TEXT");

                    b.HasKey("AttendedCoursesId", "EmployeesThatAttendedId");

                    b.HasIndex("EmployeesThatAttendedId");

                    b.ToTable("CourseInternalEmployee", (string)null);

                    b.HasData(
                        new
                        {
                            AttendedCoursesId = new Guid("37e03ca7-c730-4351-834c-b66f280cdb01"),
                            EmployeesThatAttendedId = new Guid("72f2f5fe-e50c-4966-8420-d50258aefdcb")
                        },
                        new
                        {
                            AttendedCoursesId = new Guid("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"),
                            EmployeesThatAttendedId = new Guid("72f2f5fe-e50c-4966-8420-d50258aefdcb")
                        },
                        new
                        {
                            AttendedCoursesId = new Guid("37e03ca7-c730-4351-834c-b66f280cdb01"),
                            EmployeesThatAttendedId = new Guid("f484ad8f-78fd-46d1-9f87-bbb1e676e37f")
                        },
                        new
                        {
                            AttendedCoursesId = new Guid("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"),
                            EmployeesThatAttendedId = new Guid("f484ad8f-78fd-46d1-9f87-bbb1e676e37f")
                        },
                        new
                        {
                            AttendedCoursesId = new Guid("844e14ce-c055-49e9-9610-855669c9859b"),
                            EmployeesThatAttendedId = new Guid("f484ad8f-78fd-46d1-9f87-bbb1e676e37f")
                        });
                });

            modelBuilder.Entity("EmployeeManagement.DataAccess.Entities.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsNew")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            Id = new Guid("37e03ca7-c730-4351-834c-b66f280cdb01"),
                            IsNew = false,
                            Title = "Company Introduction"
                        },
                        new
                        {
                            Id = new Guid("1fd115cf-f44c-4982-86bc-a8fe2e4ff83e"),
                            IsNew = false,
                            Title = "Respecting Your Colleagues"
                        },
                        new
                        {
                            Id = new Guid("844e14ce-c055-49e9-9610-855669c9859b"),
                            IsNew = false,
                            Title = "Dealing with Customers 101"
                        },
                        new
                        {
                            Id = new Guid("d6e0e4b7-9365-4332-9b29-bb7bf09664a6"),
                            IsNew = false,
                            Title = "Dealing with Customers - Advanced"
                        },
                        new
                        {
                            Id = new Guid("cbf6db3b-c4ee-46aa-9457-5fa8aefef33a"),
                            IsNew = false,
                            Title = "Disaster Management 101"
                        });
                });

            modelBuilder.Entity("EmployeeManagement.DataAccess.Entities.ExternalEmployee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ExternalEmployees");

                    b.HasData(
                        new
                        {
                            Id = new Guid("72f2f5fe-e50c-4966-8420-d50258aefdcb"),
                            Company = "IT for Everyone, Inc",
                            FirstName = "Amanda",
                            LastName = "Smith"
                        });
                });

            modelBuilder.Entity("EmployeeManagement.DataAccess.Entities.InternalEmployee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("JobLevel")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<bool>("MinimumRaiseGiven")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Salary")
                        .HasColumnType("TEXT");

                    b.Property<int>("YearsInService")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("InternalEmployees");

                    b.HasData(
                        new
                        {
                            Id = new Guid("72f2f5fe-e50c-4966-8420-d50258aefdcb"),
                            FirstName = "Megan",
                            JobLevel = 2,
                            LastName = "Jones",
                            MinimumRaiseGiven = false,
                            Salary = 3000m,
                            YearsInService = 2
                        },
                        new
                        {
                            Id = new Guid("f484ad8f-78fd-46d1-9f87-bbb1e676e37f"),
                            FirstName = "Jaimy",
                            JobLevel = 1,
                            LastName = "Johnson",
                            MinimumRaiseGiven = true,
                            Salary = 3400m,
                            YearsInService = 3
                        });
                });

            modelBuilder.Entity("CourseInternalEmployee", b =>
                {
                    b.HasOne("EmployeeManagement.DataAccess.Entities.Course", null)
                        .WithMany()
                        .HasForeignKey("AttendedCoursesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EmployeeManagement.DataAccess.Entities.InternalEmployee", null)
                        .WithMany()
                        .HasForeignKey("EmployeesThatAttendedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
