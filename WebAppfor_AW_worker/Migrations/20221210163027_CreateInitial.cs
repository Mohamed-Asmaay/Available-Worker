using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppfor_AW_worker.Migrations
{
    public partial class CreateInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobTbl",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTbl", x => x.JobId);
                });

            migrationBuilder.CreateTable(
                name: "RegionTbl",
                columns: table => new
                {
                    RegionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionTbl", x => x.RegionId);
                });

            migrationBuilder.CreateTable(
                name: "UserTbl",
                columns: table => new
                {
                    UsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsPassword = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UsName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UsAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    UsGender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegionNameNavigationRegionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTbl", x => x.UsId);
                    table.ForeignKey(
                        name: "FK_UserTbl_RegionTbl_RegionNameNavigationRegionId",
                        column: x => x.RegionNameNavigationRegionId,
                        principalTable: "RegionTbl",
                        principalColumn: "RegionId");
                });

            migrationBuilder.CreateTable(
                name: "WorkerTbl",
                columns: table => new
                {
                    WrId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WrName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    WrEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WrPassword = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    WrGender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WrPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WrAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    JobName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WrPhoto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalId = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    WrAvability = table.Column<bool>(type: "bit", nullable: true),
                    JobNameNavigationJobId = table.Column<int>(type: "int", nullable: true),
                    RegionNameNavigationRegionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerTbl", x => x.WrId);
                    table.ForeignKey(
                        name: "FK_WorkerTbl_JobTbl_JobNameNavigationJobId",
                        column: x => x.JobNameNavigationJobId,
                        principalTable: "JobTbl",
                        principalColumn: "JobId");
                    table.ForeignKey(
                        name: "FK_WorkerTbl_RegionTbl_RegionNameNavigationRegionId",
                        column: x => x.RegionNameNavigationRegionId,
                        principalTable: "RegionTbl",
                        principalColumn: "RegionId");
                });

            migrationBuilder.CreateTable(
                name: "RequestTbl",
                columns: table => new
                {
                    ReqId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReqTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReqProblem = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ReqDescription = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: false),
                    ReqCost = table.Column<float>(type: "real", nullable: true),
                    WrId = table.Column<int>(type: "int", nullable: false),
                    UsId = table.Column<int>(type: "int", nullable: false),
                    ReqAccept = table.Column<bool>(type: "bit", nullable: true),
                    ReqDecline = table.Column<bool>(type: "bit", nullable: true),
                    ReqConfirmation = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestTbl", x => x.ReqId);
                    table.ForeignKey(
                        name: "FK_RequestTbl_UserTbl_UsId",
                        column: x => x.UsId,
                        principalTable: "UserTbl",
                        principalColumn: "UsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestTbl_WorkerTbl_WrId",
                        column: x => x.WrId,
                        principalTable: "WorkerTbl",
                        principalColumn: "WrId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComplaintTbl",
                columns: table => new
                {
                    ComId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsId = table.Column<int>(type: "int", nullable: true),
                    ReqId = table.Column<int>(type: "int", nullable: false),
                    ComDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ComDescription = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintTbl", x => x.ComId);
                    table.ForeignKey(
                        name: "FK_ComplaintTbl_RequestTbl_ReqId",
                        column: x => x.ReqId,
                        principalTable: "RequestTbl",
                        principalColumn: "ReqId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComplaintTbl_UserTbl_UsId",
                        column: x => x.UsId,
                        principalTable: "UserTbl",
                        principalColumn: "UsId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintTbl_ReqId",
                table: "ComplaintTbl",
                column: "ReqId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintTbl_UsId",
                table: "ComplaintTbl",
                column: "UsId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestTbl_UsId",
                table: "RequestTbl",
                column: "UsId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestTbl_WrId",
                table: "RequestTbl",
                column: "WrId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTbl_RegionNameNavigationRegionId",
                table: "UserTbl",
                column: "RegionNameNavigationRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerTbl_JobNameNavigationJobId",
                table: "WorkerTbl",
                column: "JobNameNavigationJobId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerTbl_RegionNameNavigationRegionId",
                table: "WorkerTbl",
                column: "RegionNameNavigationRegionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComplaintTbl");

            migrationBuilder.DropTable(
                name: "RequestTbl");

            migrationBuilder.DropTable(
                name: "UserTbl");

            migrationBuilder.DropTable(
                name: "WorkerTbl");

            migrationBuilder.DropTable(
                name: "JobTbl");

            migrationBuilder.DropTable(
                name: "RegionTbl");
        }
    }
}
