using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetDS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_departaments_departaments_parent_id",
                table: "departaments");

            migrationBuilder.AddForeignKey(
                name: "fk_departaments_departaments_parent_id",
                table: "departaments",
                column: "parent_id",
                principalTable: "departaments",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_departaments_departaments_parent_id",
                table: "departaments");

            migrationBuilder.AddForeignKey(
                name: "fk_departaments_departaments_parent_id",
                table: "departaments",
                column: "parent_id",
                principalTable: "departaments",
                principalColumn: "id");
        }
    }
}
