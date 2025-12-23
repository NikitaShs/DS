using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetDS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class qq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "delete_time",
                table: "departaments",
                newName: "deleted_at");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "deleted_at",
                table: "departaments",
                newName: "delete_time");
        }
    }
}
