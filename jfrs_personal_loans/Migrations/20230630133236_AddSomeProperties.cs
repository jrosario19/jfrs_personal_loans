using Microsoft.EntityFrameworkCore.Migrations;

namespace jfrs_personal_loans.Migrations
{
    public partial class AddSomeProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Clients_ClientId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Loans_ClientId",
                table: "Loans");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Loans_ClientId",
                table: "Loans",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Clients_ClientId",
                table: "Loans",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
