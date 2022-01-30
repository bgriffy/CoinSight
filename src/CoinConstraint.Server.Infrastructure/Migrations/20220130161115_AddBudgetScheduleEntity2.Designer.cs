﻿// <auto-generated />
using System;
using CoinConstraint.Server.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CoinConstraint.Server.Infrastructure.Migrations
{
    [DbContext(typeof(CoinConstraintContext))]
    [Migration("20220130161115_AddBudgetScheduleEntity2")]
    partial class AddBudgetScheduleEntity2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities.Bill", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int?>("BudgetID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("BudgetID");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities.Budget", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("ID"), 1L, 1);

                    b.Property<decimal>("BudgetedAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UUID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.ToTable("Budgets");
                });

            modelBuilder.Entity("CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities.BudgetSchedule", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("BudgetID")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastScheduledDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NextScheduledDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ScheduleFrequency")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("BudgetID");

                    b.ToTable("BudgetSchedules");
                });

            modelBuilder.Entity("CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities.Expense", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("ID"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("Automatic")
                        .HasColumnType("bit");

                    b.Property<int?>("BudgetID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Paid")
                        .HasColumnType("bit");

                    b.Property<bool>("Pay")
                        .HasColumnType("bit");

                    b.Property<string>("PaymentURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("BudgetID");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities.Note", b =>
                {
                    b.Property<int>("NoteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NoteID"), 1L, 1);

                    b.Property<int?>("BudgetID")
                        .HasColumnType("int");

                    b.Property<string>("NoteText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NoteID");

                    b.HasIndex("BudgetID");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities.Reminder", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BudgetID")
                        .HasColumnType("int");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UUID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.ToTable("Reminders");
                });

            modelBuilder.Entity("CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities.Total", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("BudgetID")
                        .HasColumnType("int");

                    b.Property<int>("Title")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("BudgetID");

                    b.ToTable("Totals");
                });

            modelBuilder.Entity("CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities.Bill", b =>
                {
                    b.HasOne("CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities.Budget", null)
                        .WithMany("Bills")
                        .HasForeignKey("BudgetID");
                });

            modelBuilder.Entity("CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities.BudgetSchedule", b =>
                {
                    b.HasOne("CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities.Budget", "Budget")
                        .WithMany("BudgetSchedules")
                        .HasForeignKey("BudgetID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Budget");
                });

            modelBuilder.Entity("CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities.Expense", b =>
                {
                    b.HasOne("CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities.Budget", null)
                        .WithMany("Expenses")
                        .HasForeignKey("BudgetID");
                });

            modelBuilder.Entity("CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities.Note", b =>
                {
                    b.HasOne("CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities.Budget", null)
                        .WithMany("Notes")
                        .HasForeignKey("BudgetID");
                });

            modelBuilder.Entity("CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities.Total", b =>
                {
                    b.HasOne("CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities.Budget", null)
                        .WithMany("Totals")
                        .HasForeignKey("BudgetID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities.Budget", b =>
                {
                    b.Navigation("Bills");

                    b.Navigation("BudgetSchedules");

                    b.Navigation("Expenses");

                    b.Navigation("Notes");

                    b.Navigation("Totals");
                });
#pragma warning restore 612, 618
        }
    }
}
