using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppfor_AW_worker.Migrations
{
    public partial class updatewr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WrJob",
                table: "WorkerTbl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WrJob",
                table: "WorkerTbl",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
