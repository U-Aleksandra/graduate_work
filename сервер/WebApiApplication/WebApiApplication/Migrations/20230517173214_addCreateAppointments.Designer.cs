﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApiApplication;

#nullable disable

namespace WebApiApplication.Migrations
{
    [DbContext(typeof(AppDatabaseContext))]
    [Migration("20230517173214_addCreateAppointments")]
    partial class addCreateAppointments
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApiApplication.Models.Appointments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateApointment")
                        .HasColumnType("Date");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SpecialistId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SpecialistId");

                    b.HasIndex("UserId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("WebApiApplication.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("WebApiApplication.Models.NameService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("nameService")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("NameServices");
                });

            modelBuilder.Entity("WebApiApplication.Models.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<TimeSpan>("BreakTime")
                        .HasColumnType("time");

                    b.Property<string>("DescriptionService")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NameServiceId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(20, 0)");

                    b.Property<TimeSpan>("ServicesTime")
                        .HasColumnType("time");

                    b.Property<int?>("SpecialistId")
                        .HasColumnType("int");

                    b.Property<bool>("StartPrice")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("NameServiceId");

                    b.HasIndex("SpecialistId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("WebApiApplication.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isSpecialist")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("WebApiApplication.Models.WorkSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("Date");

                    b.Property<TimeSpan>("EndBreak")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("EndWork")
                        .HasColumnType("time");

                    b.Property<int?>("SpecialistId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("StartBreak")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("StartWork")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("SpecialistId");

                    b.ToTable("WorkSchedules");
                });

            modelBuilder.Entity("WebApiApplication.Models.Specialist", b =>
                {
                    b.HasBaseType("WebApiApplication.Models.User");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameCategory")
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("CategoryId");

                    b.HasDiscriminator().HasValue("Specialist");
                });

            modelBuilder.Entity("WebApiApplication.Models.Appointments", b =>
                {
                    b.HasOne("WebApiApplication.Models.Specialist", "Specialist")
                        .WithMany()
                        .HasForeignKey("SpecialistId");

                    b.HasOne("WebApiApplication.Models.User", "User")
                        .WithMany("Appointments")
                        .HasForeignKey("UserId");

                    b.Navigation("Specialist");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApiApplication.Models.NameService", b =>
                {
                    b.HasOne("WebApiApplication.Models.Category", "Category")
                        .WithMany("nameServices")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("WebApiApplication.Models.Service", b =>
                {
                    b.HasOne("WebApiApplication.Models.NameService", "NameService")
                        .WithMany("Services")
                        .HasForeignKey("NameServiceId");

                    b.HasOne("WebApiApplication.Models.Specialist", "Specialist")
                        .WithMany("Services")
                        .HasForeignKey("SpecialistId");

                    b.Navigation("NameService");

                    b.Navigation("Specialist");
                });

            modelBuilder.Entity("WebApiApplication.Models.WorkSchedule", b =>
                {
                    b.HasOne("WebApiApplication.Models.Specialist", "Specialist")
                        .WithMany("WorkSchedules")
                        .HasForeignKey("SpecialistId");

                    b.Navigation("Specialist");
                });

            modelBuilder.Entity("WebApiApplication.Models.Specialist", b =>
                {
                    b.HasOne("WebApiApplication.Models.Category", "Category")
                        .WithMany("Specialist")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("WebApiApplication.Models.Category", b =>
                {
                    b.Navigation("Specialist");

                    b.Navigation("nameServices");
                });

            modelBuilder.Entity("WebApiApplication.Models.NameService", b =>
                {
                    b.Navigation("Services");
                });

            modelBuilder.Entity("WebApiApplication.Models.User", b =>
                {
                    b.Navigation("Appointments");
                });

            modelBuilder.Entity("WebApiApplication.Models.Specialist", b =>
                {
                    b.Navigation("Services");

                    b.Navigation("WorkSchedules");
                });
#pragma warning restore 612, 618
        }
    }
}
