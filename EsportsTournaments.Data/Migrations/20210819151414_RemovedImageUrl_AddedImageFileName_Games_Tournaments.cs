namespace EsportsTournaments.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RemovedImageUrl_AddedImageFileName_Games_Tournaments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeamImageUrl",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "GameImageUrl",
                table: "Games");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Teams",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Games",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Games");

            migrationBuilder.AddColumn<string>(
                name: "TeamImageUrl",
                table: "Teams",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GameImageUrl",
                table: "Games",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");
        }
    }
}
