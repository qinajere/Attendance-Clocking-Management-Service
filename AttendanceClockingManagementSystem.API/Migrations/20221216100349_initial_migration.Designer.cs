﻿// <auto-generated />
using System;
using AttendanceClockingManagementSystem.API.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AttendanceClockingManagementSystem.API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221216100349_initial_migration")]
    partial class initial_migration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AttendanceClockingManagementSystem.API.DataAccess.Model.Attendance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<TimeSpan>("ClockIn")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("ClockOut")
                        .HasColumnType("time");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("EmployeeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QRCodeID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("QRCodeID")
                        .IsUnique();

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("AttendanceClockingManagementSystem.API.DataAccess.Model.OfficeTiming", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<TimeSpan>("ArrivalTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("KnockOffTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("OfficeTimings");
                });

            modelBuilder.Entity("AttendanceClockingManagementSystem.API.DataAccess.Model.QRCode", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("EmployeeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ScanStatus")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("QRCodes");
                });

            modelBuilder.Entity("AttendanceClockingManagementSystem.API.DataAccess.Model.Attendance", b =>
                {
                    b.HasOne("AttendanceClockingManagementSystem.API.DataAccess.Model.QRCode", "QRCode")
                        .WithOne("Attendance")
                        .HasForeignKey("AttendanceClockingManagementSystem.API.DataAccess.Model.Attendance", "QRCodeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("QRCode");
                });

            modelBuilder.Entity("AttendanceClockingManagementSystem.API.DataAccess.Model.QRCode", b =>
                {
                    b.Navigation("Attendance")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
