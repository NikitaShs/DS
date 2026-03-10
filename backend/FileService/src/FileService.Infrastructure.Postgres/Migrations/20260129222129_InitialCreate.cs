using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileService.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MediaAsset",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AssetType = table.Column<string>(type: "text", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdateAd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StatusMedia = table.Column<string>(type: "text", nullable: false),
                    asset_type = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    Key = table.Column<string>(type: "jsonb", nullable: false),
                    MediaData = table.Column<string>(type: "jsonb", nullable: false),
                    Owner = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaAsset", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MediaAsset");
        }
    }
}
