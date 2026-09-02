using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClubPOS.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedMaintenanceLogStuffs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MaintenanceLogs",
                columns: table => new
                {
                    MaintenanceLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccommodationId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    LogDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BackInServiceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    ResolvedComment = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    NeedsCleaning = table.Column<bool>(type: "bit", nullable: false),
                    EmployeeResolved = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceLogs", x => x.MaintenanceLogId);
                    table.ForeignKey(
                        name: "FK_MaintenanceLogs_Accommodations_AccommodationId",
                        column: x => x.AccommodationId,
                        principalTable: "Accommodations",
                        principalColumn: "AccommodationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceTasks",
                columns: table => new
                {
                    MaintenanceTaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsEndUserFixable = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceTasks", x => x.MaintenanceTaskId);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceLogDetails",
                columns: table => new
                {
                    MaintenanceLogDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaintenanceLogId = table.Column<int>(type: "int", nullable: false),
                    MaintenanceTaskId = table.Column<int>(type: "int", nullable: false),
                    IsResolved = table.Column<bool>(type: "bit", nullable: false),
                    DateResolved = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceLogDetails", x => x.MaintenanceLogDetailId);
                    table.ForeignKey(
                        name: "FK_MaintenanceLogDetails_MaintenanceLogs_MaintenanceLogId",
                        column: x => x.MaintenanceLogId,
                        principalTable: "MaintenanceLogs",
                        principalColumn: "MaintenanceLogId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaintenanceLogDetails_MaintenanceTasks_MaintenanceTaskId",
                        column: x => x.MaintenanceTaskId,
                        principalTable: "MaintenanceTasks",
                        principalColumn: "MaintenanceTaskId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceLogDetails_MaintenanceLogId",
                table: "MaintenanceLogDetails",
                column: "MaintenanceLogId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceLogDetails_MaintenanceTaskId",
                table: "MaintenanceLogDetails",
                column: "MaintenanceTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceLogs_AccommodationId",
                table: "MaintenanceLogs",
                column: "AccommodationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaintenanceLogDetails");

            migrationBuilder.DropTable(
                name: "MaintenanceLogs");

            migrationBuilder.DropTable(
                name: "MaintenanceTasks");
        }
    }
}
