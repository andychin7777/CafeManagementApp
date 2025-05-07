using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CafeManagementApp.SQL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCafeEmployeeEndDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CafeEmployees_CafeId_EmployeeId",
                table: "CafeEmployees");

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndDate",
                table: "CafeEmployees",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartDate",
                table: "CafeEmployees",
                type: "date",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CafeEmployees_CafeId",
                table: "CafeEmployees",
                column: "CafeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CafeEmployees_CafeId",
                table: "CafeEmployees");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "CafeEmployees");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "CafeEmployees");

            migrationBuilder.CreateIndex(
                name: "IX_CafeEmployees_CafeId_EmployeeId",
                table: "CafeEmployees",
                columns: new[] { "CafeId", "EmployeeId" },
                unique: true);
        }
    }
}
