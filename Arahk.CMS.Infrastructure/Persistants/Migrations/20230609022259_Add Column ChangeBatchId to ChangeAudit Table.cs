using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arahk.CMS.Infrastructure.Persistants.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnChangeBatchIdtoChangeAuditTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ChangedBatchId",
                table: "ChangedAuditEntries",
                type: "uniqueidentifier",
                maxLength: 40,
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangedBatchId",
                table: "ChangedAuditEntries");
        }
    }
}
