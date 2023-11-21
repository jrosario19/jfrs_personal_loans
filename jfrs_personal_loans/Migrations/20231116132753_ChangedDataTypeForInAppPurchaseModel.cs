using Microsoft.EntityFrameworkCore.Migrations;

namespace jfrs_personal_loans.Migrations
{
    public partial class ChangedDataTypeForInAppPurchaseModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InAppPurchaseToken",
                table: "InAppPurchases",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "InAppPurchaseToken",
                table: "InAppPurchases",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
