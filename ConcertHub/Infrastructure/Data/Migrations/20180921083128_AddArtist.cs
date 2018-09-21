using Microsoft.EntityFrameworkCore.Migrations;

namespace ConcertHub.Infrastructure.Data.Migrations
{
    public partial class AddArtist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ArtistId",
                table: "Gigs",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateTable(
                name: "Artist",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artist", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gigs_ArtistId",
                table: "Gigs",
                column: "ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gigs_Artist_ArtistId",
                table: "Gigs",
                column: "ArtistId",
                principalTable: "Artist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gigs_Artist_ArtistId",
                table: "Gigs");

            migrationBuilder.DropTable(
                name: "Artist");

            migrationBuilder.DropIndex(
                name: "IX_Gigs_ArtistId",
                table: "Gigs");

            migrationBuilder.AlterColumn<string>(
                name: "ArtistId",
                table: "Gigs",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
