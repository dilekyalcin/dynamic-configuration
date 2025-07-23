using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConfigurationReaderAPI.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ConfigurationItem",
                table: "ConfigurationItem");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "ConfigurationItem",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ConfigurationItem",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConfigurationItem",
                table: "ConfigurationItem",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ConfigurationItem",
                table: "ConfigurationItem");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ConfigurationItem");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "ConfigurationItem",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConfigurationItem",
                table: "ConfigurationItem",
                column: "Key");
        }
    }
}
