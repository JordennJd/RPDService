﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RPDSerice;

#nullable disable

namespace RPDSerice.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240303152553_CriticalUpdate")]
    partial class CriticalUpdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RPDSerice.Models.CriticalInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CountOfHourCourseProject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountOfHourCourseWork")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountOfHourLab")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountOfHourLecture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountOfHourPractice")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExamHours")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FO")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Faculty")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumberOfDepartament")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SPZ")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SRS")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpecialtyNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeOfControl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeOfCourseProject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CriticalInfo");
                });

            modelBuilder.Entity("RPDSerice.Models.Flags", b =>
                {
                    b.Property<bool>("isExcelDataInstalled")
                        .HasColumnType("bit");

                    b.ToTable("Flags");
                });

            modelBuilder.Entity("RPDSerice.Models.RPD", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CriticalInfoId")
                        .HasColumnType("int");

                    b.Property<int>("RpdInfoId")
                        .HasColumnType("int");

                    b.Property<int>("RpdlInfoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CriticalInfoId");

                    b.HasIndex("RpdInfoId");

                    b.ToTable("RPDs");
                });

            modelBuilder.Entity("RPDSerice.Models.RpdInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("RpdInfo");
                });

            modelBuilder.Entity("RPDSerice.Models.RPD", b =>
                {
                    b.HasOne("RPDSerice.Models.CriticalInfo", "CriticalInfo")
                        .WithMany()
                        .HasForeignKey("CriticalInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RPDSerice.Models.RpdInfo", "RpdInfo")
                        .WithMany()
                        .HasForeignKey("RpdInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CriticalInfo");

                    b.Navigation("RpdInfo");
                });
#pragma warning restore 612, 618
        }
    }
}
