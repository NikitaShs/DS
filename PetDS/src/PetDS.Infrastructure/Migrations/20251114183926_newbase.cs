using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetDS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newbase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:ltree", ",,");

            migrationBuilder.CreateTable(
                name: "departaments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    identifier = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    parent_id = table.Column<Guid>(type: "uuid", nullable: true),
                    path = table.Column<string>(type: "ltree", nullable: false),
                    depth = table.Column<short>(type: "smallint", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    create_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    update_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    location_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_departaments", x => x.id);
                    table.ForeignKey(
                        name: "fk_departaments_departaments_parent_id",
                        column: x => x.parent_id,
                        principalTable: "departaments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "locations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    lanaCode = table.Column<string>(type: "text", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    create_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    update_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Address = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_locations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "positions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    discription = table.Column<string>(type: "text", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    create_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    update_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_positions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "departamentLocations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    departament_id = table.Column<Guid>(type: "uuid", nullable: false),
                    departament_id1 = table.Column<Guid>(type: "uuid", nullable: false),
                    location_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_departament_locations", x => x.id);
                    table.ForeignKey(
                        name: "fk_departament_locations_departaments_departament_id",
                        column: x => x.departament_id,
                        principalTable: "departaments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_departament_locations_departaments_departament_id1",
                        column: x => x.departament_id1,
                        principalTable: "departaments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_departament_locations_locations_location_id",
                        column: x => x.location_id,
                        principalTable: "locations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "departamentPositions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    departament_id = table.Column<Guid>(type: "uuid", nullable: false),
                    departament_id1 = table.Column<Guid>(type: "uuid", nullable: false),
                    position_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_departament_positions", x => x.id);
                    table.ForeignKey(
                        name: "fk_departament_positions_departaments_departament_id",
                        column: x => x.departament_id,
                        principalTable: "departaments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_departament_positions_departaments_departament_id1",
                        column: x => x.departament_id1,
                        principalTable: "departaments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_departament_positions_positions_position_id",
                        column: x => x.position_id,
                        principalTable: "positions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_departament_locations_departament_id",
                table: "departamentLocations",
                column: "departament_id");

            migrationBuilder.CreateIndex(
                name: "ix_departament_locations_departament_id1",
                table: "departamentLocations",
                column: "departament_id1");

            migrationBuilder.CreateIndex(
                name: "ix_departament_locations_location_id",
                table: "departamentLocations",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "ix_departament_positions_departament_id",
                table: "departamentPositions",
                column: "departament_id");

            migrationBuilder.CreateIndex(
                name: "ix_departament_positions_departament_id1",
                table: "departamentPositions",
                column: "departament_id1");

            migrationBuilder.CreateIndex(
                name: "ix_departament_positions_position_id",
                table: "departamentPositions",
                column: "position_id");

            migrationBuilder.CreateIndex(
                name: "ix_departaments_name",
                table: "departaments",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_departaments_parent_id",
                table: "departaments",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "ix_locations_name",
                table: "locations",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_positions_name",
                table: "positions",
                column: "name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "departamentLocations");

            migrationBuilder.DropTable(
                name: "departamentPositions");

            migrationBuilder.DropTable(
                name: "locations");

            migrationBuilder.DropTable(
                name: "departaments");

            migrationBuilder.DropTable(
                name: "positions");
        }
    }
}
