using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace jfrs_personal_loans.Migrations
{
    public partial class AddedInAppPurchasesFeature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InAppPurchases",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedByUser = table.Column<string>(nullable: true),
                    CreatedOnDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    FEId = table.Column<string>(nullable: false),
                    InAppPurchaseToken = table.Column<int>(nullable: false),
                    TenantId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InAppPurchases", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InAppPurchases_TenantId",
                table: "InAppPurchases",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InAppPurchases");
        }
    }
}
