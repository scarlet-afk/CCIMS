using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CCIMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddInvestigatorToInvestigationLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InvestigatorId",
                table: "InvestigationLogs",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_InvestigationLogs_InvestigatorId",
                table: "InvestigationLogs",
                column: "InvestigatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvestigationLogs_AspNetUsers_InvestigatorId",
                table: "InvestigationLogs",
                column: "InvestigatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvestigationLogs_AspNetUsers_InvestigatorId",
                table: "InvestigationLogs");

            migrationBuilder.DropIndex(
                name: "IX_InvestigationLogs_InvestigatorId",
                table: "InvestigationLogs");

            migrationBuilder.DropColumn(
                name: "InvestigatorId",
                table: "InvestigationLogs");
        }
    }
}
