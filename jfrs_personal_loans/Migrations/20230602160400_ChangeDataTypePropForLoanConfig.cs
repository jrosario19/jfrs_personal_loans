using Microsoft.EntityFrameworkCore.Migrations;

namespace jfrs_personal_loans.Migrations
{
    public partial class ChangeDataTypePropForLoanConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PaymentWeekDay",
                table: "LoanConfigurations",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PaymentWeekDay",
                table: "LoanConfigurations",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
