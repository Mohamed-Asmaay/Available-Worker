using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppfor_AW_worker.Migrations
{
    public partial class jopPhotoinsert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JopPhoto",
                table: "JobTbl",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JopPhoto",
                table: "JobTbl");
        }
    }
}
