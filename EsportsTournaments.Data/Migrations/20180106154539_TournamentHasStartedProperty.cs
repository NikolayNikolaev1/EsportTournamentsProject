namespace EsportsTournaments.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class TournamentHasStartedProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasStarted",
                table: "Tournaments",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasStarted",
                table: "Tournaments");
        }
    }
}
