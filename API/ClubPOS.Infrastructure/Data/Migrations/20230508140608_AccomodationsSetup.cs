using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClubPOS.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AccomodationsSetup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.CreateTable(
                name: "AccommodationItems",
                columns: table => new
                {
                    AccommodationItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DepositAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccommodationItems", x => x.AccommodationItemId);
                });

            migrationBuilder.CreateTable(
                name: "AccommodationStatuses",
                columns: table => new
                {
                    AccommodationStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccommodationStatuses", x => x.AccommodationStatusId);
                });

            migrationBuilder.CreateTable(
                name: "AccommodationTypes",
                columns: table => new
                {
                    AccommodationTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxOccupants = table.Column<int>(type: "int", nullable: false),
                    CompatibilityValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccommodationTypes", x => x.AccommodationTypeId);
                });

            migrationBuilder.CreateTable(
                name: "AccommodationRates",
                columns: table => new
                {
                    AccommodationRateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccommodationTypeId = table.Column<int>(type: "int", nullable: false),
                    Hours = table.Column<int>(type: "int", nullable: true),
                    RateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RateStartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RateEndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Tax1 = table.Column<int>(type: "int", nullable: true),
                    Tax2 = table.Column<int>(type: "int", nullable: true),
                    Tax3 = table.Column<int>(type: "int", nullable: true),
                    Tax4 = table.Column<int>(type: "int", nullable: true),
                    Tax5 = table.Column<int>(type: "int", nullable: true),
                    IsStandardRate = table.Column<bool>(type: "bit", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccommodationRates", x => x.AccommodationRateId);
                    table.ForeignKey(
                        name: "FK_AccommodationRates_AccommodationTypes_AccommodationTypeId",
                        column: x => x.AccommodationTypeId,
                        principalTable: "AccommodationTypes",
                        principalColumn: "AccommodationTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accommodations",
                columns: table => new
                {
                    AccommodationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccommodationTypeId = table.Column<int>(type: "int", nullable: false),
                    AccommodationStatusId = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Floor = table.Column<int>(type: "int", nullable: false),
                    DeActivate = table.Column<bool>(type: "bit", nullable: false),
                    RequiresCreditCard = table.Column<bool>(type: "bit", nullable: false),
                    HasMaintenanceReservation = table.Column<bool>(type: "bit", nullable: false),
                    EmployeeLocker = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsGhost = table.Column<bool>(type: "bit", nullable: false),
                    HasGloryHole = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accommodations", x => x.AccommodationId);
                    table.ForeignKey(
                        name: "FK_Accommodations_AccommodationStatuses_AccommodationStatusId",
                        column: x => x.AccommodationStatusId,
                        principalTable: "AccommodationStatuses",
                        principalColumn: "AccommodationStatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Accommodations_AccommodationTypes_AccommodationTypeId",
                        column: x => x.AccommodationTypeId,
                        principalTable: "AccommodationTypes",
                        principalColumn: "AccommodationTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccommodationSubItems",
                columns: table => new
                {
                    AccommodationTypeId = table.Column<int>(type: "int", nullable: false),
                    AccommodationItemId = table.Column<int>(type: "int", nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false),
                    MaxQty = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccommodationSubItems", x => new { x.AccommodationTypeId, x.AccommodationItemId });
                    table.ForeignKey(
                        name: "FK_AccommodationSubItems_AccommodationItems_AccommodationItemId",
                        column: x => x.AccommodationItemId,
                        principalTable: "AccommodationItems",
                        principalColumn: "AccommodationItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccommodationSubItems_AccommodationTypes_AccommodationTypeId",
                        column: x => x.AccommodationTypeId,
                        principalTable: "AccommodationTypes",
                        principalColumn: "AccommodationTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccommodationStatusHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccommodationId = table.Column<int>(type: "int", nullable: false),
                    AccommodationStatusId = table.Column<int>(type: "int", nullable: false),
                    StatusChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccommodationStatusHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccommodationStatusHistories_AccommodationStatuses_AccommodationStatusId",
                        column: x => x.AccommodationStatusId,
                        principalTable: "AccommodationStatuses",
                        principalColumn: "AccommodationStatusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccommodationStatusHistories_Accommodations_AccommodationId",
                        column: x => x.AccommodationId,
                        principalTable: "Accommodations",
                        principalColumn: "AccommodationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationRates_AccommodationTypeId",
                table: "AccommodationRates",
                column: "AccommodationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Accommodations_AccommodationStatusId",
                table: "Accommodations",
                column: "AccommodationStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Accommodations_AccommodationTypeId",
                table: "Accommodations",
                column: "AccommodationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationStatusHistories_AccommodationId",
                table: "AccommodationStatusHistories",
                column: "AccommodationId");

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationStatusHistories_AccommodationStatusId",
                table: "AccommodationStatusHistories",
                column: "AccommodationStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationSubItems_AccommodationItemId",
                table: "AccommodationSubItems",
                column: "AccommodationItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccommodationRates");

            migrationBuilder.DropTable(
                name: "AccommodationStatusHistories");

            migrationBuilder.DropTable(
                name: "AccommodationSubItems");

            migrationBuilder.DropTable(
                name: "Accommodations");

            migrationBuilder.DropTable(
                name: "AccommodationItems");

            migrationBuilder.DropTable(
                name: "AccommodationStatuses");

            migrationBuilder.DropTable(
                name: "AccommodationTypes");

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomCharge = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RoomName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoomType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });
        }
    }
}
