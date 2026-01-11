using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CCIMS.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddInvestigatorNameToLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InvestigatorName",
                table: "InvestigationLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvestigatorName",
                table: "InvestigationLogs");

            
        }
    }
}
