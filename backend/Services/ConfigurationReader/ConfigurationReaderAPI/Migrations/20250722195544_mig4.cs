using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConfigurationReaderAPI.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ConfigurationItem");

            migrationBuilder.RenameColumn(
                name: "ValueType",
                table: "ConfigurationItem",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "Key",
                table: "ConfigurationItem",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "ConfigurationItem",
                newName: "ValueType");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ConfigurationItem",
                newName: "Key");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ConfigurationItem",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
