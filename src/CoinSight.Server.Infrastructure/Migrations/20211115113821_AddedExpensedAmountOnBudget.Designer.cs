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
    [Migration("20211115113821_AddedExpensedAmountOnBudget")]
    partial class AddedExpensedAmountOnBudget
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CoinConstraint.Domain.AggregateModels.BudgetAggregate.Bill", b =>
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

            modelBuilder.Entity("CoinConstraint.Domain.AggregateModels.BudgetAggregate.Budget", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<decimal>("BudgetedAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("ExpensedAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UUID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserUUID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("UserUUID");

                    b.ToTable("Budgets");
                });

            modelBuilder.Entity("CoinConstraint.Domain.AggregateModels.BudgetAggregate.Expense", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("ID"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("Automatic")
                        .HasColumnType("bit");

                    b.Property<int>("BudgetID")
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

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("BudgetID");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("CoinConstraint.Domain.AggregateModels.BudgetAggregate.Reminder", b =>
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

            modelBuilder.Entity("CoinConstraint.Domain.AggregateModels.BudgetAggregate.Total", b =>
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

            modelBuilder.Entity("CoinConstraint.Domain.AggregateModels.UserAggregate.User", b =>
                {
                    b.Property<Guid>("UUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UUID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CoinConstraint.Domain.AggregateModels.BudgetAggregate.Bill", b =>
                {
                    b.HasOne("CoinConstraint.Domain.AggregateModels.BudgetAggregate.Budget", null)
                        .WithMany("Bills")
                        .HasForeignKey("BudgetID");
                });

            modelBuilder.Entity("CoinConstraint.Domain.AggregateModels.BudgetAggregate.Budget", b =>
                {
                    b.HasOne("CoinConstraint.Domain.AggregateModels.UserAggregate.User", "User")
                        .WithMany("Budgets")
                        .HasForeignKey("UserUUID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CoinConstraint.Domain.AggregateModels.BudgetAggregate.Expense", b =>
                {
                    b.HasOne("CoinConstraint.Domain.AggregateModels.BudgetAggregate.Budget", null)
                        .WithMany("Expenses")
                        .HasForeignKey("BudgetID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CoinConstraint.Domain.AggregateModels.BudgetAggregate.Total", b =>
                {
                    b.HasOne("CoinConstraint.Domain.AggregateModels.BudgetAggregate.Budget", null)
                        .WithMany("Totals")
                        .HasForeignKey("BudgetID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CoinConstraint.Domain.AggregateModels.BudgetAggregate.Budget", b =>
                {
                    b.Navigation("Bills");

                    b.Navigation("Expenses");

                    b.Navigation("Totals");
                });

            modelBuilder.Entity("CoinConstraint.Domain.AggregateModels.UserAggregate.User", b =>
                {
                    b.Navigation("Budgets");
                });
#pragma warning restore 612, 618
        }
    }
}
