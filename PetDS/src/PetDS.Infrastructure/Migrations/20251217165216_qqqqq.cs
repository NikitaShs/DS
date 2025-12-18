using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetDS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class qqqqq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "delete_time",
                table: "departaments",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "delete_time",
                table: "departaments");
        }
    }
}
