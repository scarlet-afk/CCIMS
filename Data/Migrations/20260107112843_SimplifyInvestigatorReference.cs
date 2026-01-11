using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CCIMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class SimplifyInvestigatorReference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvestigationLogs_AspNetUsers_InvestigatorId",
                table: "InvestigationLogs");

            migrationBuilder.DropIndex(
                name: "IX_InvestigationLogs_InvestigatorId",
                table: "InvestigationLogs");

            migrationBuilder.AlterColumn<string>(
                name: "InvestigatorId",
                table: "InvestigationLogs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InvestigatorId",
                table: "InvestigationLogs",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
    }
}
