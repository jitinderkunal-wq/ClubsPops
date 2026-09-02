using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClubPOS.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedProblemRelatedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProblemCategories",
                columns: table => new
                {
                    ProblemCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProblemCategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
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
                    table.PrimaryKey("PK_ProblemCategories", x => x.ProblemCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Problems",
                columns: table => new
                {
                    ProblemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: true),
                    ProblemCategoryId = table.Column<int>(type: "int", nullable: false),
                    ProblemExplaination = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NewMemberName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
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
                    table.PrimaryKey("PK_Problems", x => x.ProblemId);
                    table.ForeignKey(
                        name: "FK_Problems_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Problems_ProblemCategories_ProblemCategoryId",
                        column: x => x.ProblemCategoryId,
                        principalTable: "ProblemCategories",
                        principalColumn: "ProblemCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Problems_BookingId",
                table: "Problems",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Problems_ProblemCategoryId",
                table: "Problems",
                column: "ProblemCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Problems");

            migrationBuilder.DropTable(
                name: "ProblemCategories");
        }
    }
}
