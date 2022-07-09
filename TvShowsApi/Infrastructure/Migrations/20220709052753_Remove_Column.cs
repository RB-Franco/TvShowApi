using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Remove_Column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TS_ImageThumbnailPath",
                table: "TB_TVSHOW");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TS_ImageThumbnailPath",
                table: "TB_TVSHOW",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
