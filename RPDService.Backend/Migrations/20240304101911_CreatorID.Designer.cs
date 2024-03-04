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
    [Migration("20240304101911_CreatorID")]
    partial class CreatorID
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
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountOfHourCourseWork")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountOfHourLab")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountOfHourLecture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountOfHourPractice")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExamHours")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Faculty")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GroupName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumberOfDepartament")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SPZ")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SRS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpecialtyNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeOfControl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeOfCourseProject")
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

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<int>("CriticalInfoId")
                        .HasColumnType("int");

                    b.Property<int>("RpdInfoId")
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

                    b.Property<string>("TestProp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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
