using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class FixEnrollNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RollNumber",
                table: "Registrations");

            migrationBuilder.AddColumn<long>(
                name: "EnrollNumber",
                table: "Registrations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnrollNumber",
                table: "Registrations");

            migrationBuilder.AddColumn<string>(
                name: "RollNumber",
                table: "Registrations",
                type: "longtext",
                nullable: false);
        }
    }
}
