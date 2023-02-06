using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notissimus_Task.Migrations
{
    public partial class addColumnUrlToMusicOffers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "musicOffers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "musicOffers");
        }
    }
}
