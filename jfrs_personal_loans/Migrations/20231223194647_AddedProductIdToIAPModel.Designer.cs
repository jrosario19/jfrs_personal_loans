﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using jfrs_personal_loans.Data;

namespace jfrs_personal_loans.Migrations
{
    [DbContext(typeof(JFRSPersonalLoansDBContext))]
    [Migration("20231223194647_AddedProductIdToIAPModel")]
    partial class AddedProductIdToIAPModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("jfrs_personal_loans.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("ResetPasswordCode");

                    b.Property<DateTime?>("ResetPasswordCodeTimeStamp")
                        .HasColumnType("datetime");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<string>("UserPhotoPath");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("jfrs_personal_loans.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("CellPhoneNumber");

                    b.Property<string>("Code");

                    b.Property<string>("CreatedByUser");

                    b.Property<DateTime>("CreatedOnDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Email");

                    b.Property<string>("FEId")
                        .IsRequired();

                    b.Property<string>("HomePhoneNumber");

                    b.Property<string>("Identification");

                    b.Property<bool>("IsActive");

                    b.Property<string>("LastName");

                    b.Property<string>("Latitude");

                    b.Property<string>("Longitude");

                    b.Property<string>("Name");

                    b.Property<string>("ReferalAddress");

                    b.Property<string>("ReferalName");

                    b.Property<string>("ReferalPhonenumber");

                    b.Property<string>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("jfrs_personal_loans.Models.CompanyConfiguration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("CreatedByUser");

                    b.Property<DateTime>("CreatedOnDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Currency");

                    b.Property<string>("FEId")
                        .IsRequired();

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("CompanyConfigurations");
                });

            modelBuilder.Entity("jfrs_personal_loans.Models.InAppPurchase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedByUser");

                    b.Property<DateTime>("CreatedOnDate")
                        .HasColumnType("datetime");

                    b.Property<string>("FEId")
                        .IsRequired();

                    b.Property<string>("InAppPurchaseToken");

                    b.Property<bool>("IsActive");

                    b.Property<string>("ProductId");

                    b.Property<string>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("InAppPurchases");
                });

            modelBuilder.Entity("jfrs_personal_loans.Models.Installment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedByUser");

                    b.Property<DateTime>("CreatedOnDate")
                        .HasColumnType("datetime");

                    b.Property<string>("FEId")
                        .IsRequired();

                    b.Property<decimal>("InstallmentAmount");

                    b.Property<bool>("IsActive");

                    b.Property<int>("LoanId");

                    b.Property<string>("OpenDate");

                    b.Property<string>("Status");

                    b.Property<string>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("Installments");
                });

            modelBuilder.Entity("jfrs_personal_loans.Models.Loan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Capital");

                    b.Property<int>("ClientId");

                    b.Property<string>("CreatedByUser");

                    b.Property<DateTime>("CreatedOnDate")
                        .HasColumnType("datetime");

                    b.Property<string>("DueDate");

                    b.Property<string>("FEId")
                        .IsRequired();

                    b.Property<decimal>("InstallmentAmount");

                    b.Property<decimal>("InterestAmount");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsAllowLoanArrears");

                    b.Property<int>("LoanArrearsAllowDays");

                    b.Property<decimal>("LoanArrearsInterest");

                    b.Property<string>("OpenDate");

                    b.Property<string>("PaymentFrequency");

                    b.Property<decimal>("Rate");

                    b.Property<string>("Status");

                    b.Property<string>("TenantId");

                    b.Property<decimal>("TotalAmount");

                    b.Property<int>("installmentQty");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("jfrs_personal_loans.Models.LoanConfiguration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedByUser");

                    b.Property<DateTime>("CreatedOnDate")
                        .HasColumnType("datetime");

                    b.Property<int>("DefaultInterest");

                    b.Property<string>("FEId")
                        .IsRequired();

                    b.Property<int>("FortnightDays");

                    b.Property<string>("IgnoredWeekDays");

                    b.Property<string>("InterestApplication");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsAllowLoanArrears");

                    b.Property<bool>("IsAllowToSetDayForPayment");

                    b.Property<int>("LoanArrearsAllowDays");

                    b.Property<string>("LoanArrearsApplication");

                    b.Property<int>("LoanArrearsInterest");

                    b.Property<int>("MonthDays");

                    b.Property<int>("PaymentFirstFortnightDay");

                    b.Property<string>("PaymentFrequency");

                    b.Property<int>("PaymentMonthtDay");

                    b.Property<int>("PaymentSecondFortnightDay");

                    b.Property<string>("PaymentWeekDay");

                    b.Property<string>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("LoanConfigurations");
                });

            modelBuilder.Entity("jfrs_personal_loans.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount");

                    b.Property<decimal>("ArrearAmount");

                    b.Property<string>("CreatedByUser");

                    b.Property<DateTime>("CreatedOnDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Date");

                    b.Property<string>("FEId")
                        .IsRequired();

                    b.Property<bool>("IsActive");

                    b.Property<int>("LoanId");

                    b.Property<string>("Status");

                    b.Property<string>("TenantId");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("jfrs_personal_loans.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("jfrs_personal_loans.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("jfrs_personal_loans.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("jfrs_personal_loans.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
