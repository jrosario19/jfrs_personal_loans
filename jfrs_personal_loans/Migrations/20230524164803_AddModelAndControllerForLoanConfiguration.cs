using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace jfrs_personal_loans.Migrations
{
    public partial class AddModelAndControllerForLoanConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoanConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedByUser = table.Column<string>(nullable: true),
                    CreatedOnDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    FEId = table.Column<string>(nullable: false),
                    PaymentFrequency = table.Column<string>(nullable: true),
                    InterestApplication = table.Column<string>(nullable: true),
                    DefaultInterest = table.Column<int>(nullable: false),
                    IsAllowLoanArrears = table.Column<bool>(nullable: false),
                    LoanArrearsApplication = table.Column<string>(nullable: true),
                    LoanArrearsInterest = table.Column<int>(nullable: false),
                    LoanArrearsAllowDays = table.Column<int>(nullable: false),
                    IgnoredWeekDays = table.Column<string>(nullable: true),
                    MonthDays = table.Column<int>(nullable: false),
                    FortnightDays = table.Column<int>(nullable: false),
                    IsAllowToSetDayForPayment = table.Column<bool>(nullable: false),
                    PaymentWeekDay = table.Column<int>(nullable: false),
                    PaymentFirstFortnightDay = table.Column<int>(nullable: false),
                    PaymentSecondFortnightDay = table.Column<int>(nullable: false),
                    PaymentMonthtDay = table.Column<int>(nullable: false),
                    TenantId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanConfigurations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoanConfigurations_TenantId",
                table: "LoanConfigurations",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanConfigurations");
        }
    }
}
