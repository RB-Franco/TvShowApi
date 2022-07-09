using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Create_Table_Show : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_TVSHOW",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TS_ReferenceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TS_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TS_Permalink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TS_StartDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TS_EndDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TS_Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TS_Network = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TS_Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TS_Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TS_CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_TVSHOW", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_TVSHOW");
        }
    }
}
