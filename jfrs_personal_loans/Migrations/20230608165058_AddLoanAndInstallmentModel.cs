using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace jfrs_personal_loans.Migrations
{
    public partial class AddLoanAndInstallmentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedByUser = table.Column<string>(nullable: true),
                    CreatedOnDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    FEId = table.Column<string>(nullable: false),
                    OpenDate = table.Column<DateTime>(type: "date", nullable: false),
                    DueDate = table.Column<DateTime>(type: "date", nullable: false),
                    Capital = table.Column<int>(nullable: false),
                    Rate = table.Column<decimal>(nullable: false),
                    InterestAmount = table.Column<int>(nullable: false),
                    TotalAmount = table.Column<int>(nullable: false),
                    PaymentFrequency = table.Column<string>(nullable: true),
                    installmentQty = table.Column<int>(nullable: false),
                    InstallmentAmount = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    IsAllowLoanArrears = table.Column<bool>(nullable: false),
                    LoanArrearsInterest = table.Column<int>(nullable: false),
                    LoanArrearsAllowDays = table.Column<int>(nullable: false),
                    TenantId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Installments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedByUser = table.Column<string>(nullable: true),
                    CreatedOnDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    FEId = table.Column<string>(nullable: false),
                    OpenDate = table.Column<DateTime>(type: "date", nullable: false),
                    InstallmentAmount = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    TenantId = table.Column<string>(nullable: true),
                    LoanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Installments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Installments_Loans_LoanId",
                        column: x => x.LoanId,
                        principalTable: "Loans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Installments_LoanId",
                table: "Installments",
                column: "LoanId");

            migrationBuilder.CreateIndex(
                name: "IX_Installments_TenantId",
                table: "Installments",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_TenantId",
                table: "Loans",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Installments");

            migrationBuilder.DropTable(
                name: "Loans");
        }
    }
}
