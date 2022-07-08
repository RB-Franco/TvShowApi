using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Creating_New_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TS_ReferenceId",
                table: "TB_TVSHOW",
                newName: "TS_Url");

            migrationBuilder.AddColumn<string>(
                name: "TS_Description",
                table: "TB_TVSHOW",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TS_Description_source",
                table: "TB_TVSHOW",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TS_Genres",
                table: "TB_TVSHOW",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TS_ImageThumbnailPath",
                table: "TB_TVSHOW",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TS_Runtime",
                table: "TB_TVSHOW",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TB_EPISODE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TS_Season = table.Column<int>(type: "int", nullable: false),
                    TS_Number = table.Column<int>(type: "int", nullable: false),
                    TS_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TS_AirDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShowId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_EPISODE", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_EPISODE_TB_TVSHOW_ShowId",
                        column: x => x.ShowId,
                        principalTable: "TB_TVSHOW",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TB_FAVORITES",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShowId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_FAVORITES", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_FAVORITES_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_FAVORITES_TB_TVSHOW_ShowId",
                        column: x => x.ShowId,
                        principalTable: "TB_TVSHOW",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_EPISODE_ShowId",
                table: "TB_EPISODE",
                column: "ShowId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_FAVORITES_ShowId",
                table: "TB_FAVORITES",
                column: "ShowId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_FAVORITES_UserId",
                table: "TB_FAVORITES",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_EPISODE");

            migrationBuilder.DropTable(
                name: "TB_FAVORITES");

            migrationBuilder.DropColumn(
                name: "TS_Description",
                table: "TB_TVSHOW");

            migrationBuilder.DropColumn(
                name: "TS_Description_source",
                table: "TB_TVSHOW");

            migrationBuilder.DropColumn(
                name: "TS_Genres",
                table: "TB_TVSHOW");

            migrationBuilder.DropColumn(
                name: "TS_ImageThumbnailPath",
                table: "TB_TVSHOW");

            migrationBuilder.DropColumn(
                name: "TS_Runtime",
                table: "TB_TVSHOW");

            migrationBuilder.RenameColumn(
                name: "TS_Url",
                table: "TB_TVSHOW",
                newName: "TS_ReferenceId");
        }
    }
}
