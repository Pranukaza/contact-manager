using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactManagerStarter.Migrations
{
    /// <inheritdoc />
    public partial class PrimaryEmailIndexUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EmailAddresses_ContactId",
                table: "EmailAddresses");

            migrationBuilder.DropIndex(
                name: "IX_EmailAddresses_Email_Type",
                table: "EmailAddresses");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "EmailAddresses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_EmailAddresses_ContactId_Type",
                table: "EmailAddresses",
                columns: new[] { "ContactId", "Type" },
                unique: true,
                filter: "[Type] = 2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EmailAddresses_ContactId_Type",
                table: "EmailAddresses");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "EmailAddresses",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_EmailAddresses_ContactId",
                table: "EmailAddresses",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailAddresses_Email_Type",
                table: "EmailAddresses",
                columns: new[] { "Email", "Type" },
                unique: true,
                filter: "[Type] = 2");
        }
    }
}
