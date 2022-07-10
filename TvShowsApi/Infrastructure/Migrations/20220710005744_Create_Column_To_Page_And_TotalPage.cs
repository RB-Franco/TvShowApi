using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Create_Column_To_Page_And_TotalPage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TS_Page",
                table: "TB_TVSHOW",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TS_TotalPage",
                table: "TB_TVSHOW",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TS_Page",
                table: "TB_TVSHOW");

            migrationBuilder.DropColumn(
                name: "TS_TotalPage",
                table: "TB_TVSHOW");
        }
    }
}
