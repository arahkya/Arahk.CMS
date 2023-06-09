using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arahk.CMS.Infrastructure.Persistants.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnstoChangeAuditTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NewValue",
                table: "ChangedAuditEntries",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreviousValue",
                table: "ChangedAuditEntries",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PropertyName",
                table: "ChangedAuditEntries",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewValue",
                table: "ChangedAuditEntries");

            migrationBuilder.DropColumn(
                name: "PreviousValue",
                table: "ChangedAuditEntries");

            migrationBuilder.DropColumn(
                name: "PropertyName",
                table: "ChangedAuditEntries");
        }
    }
}
