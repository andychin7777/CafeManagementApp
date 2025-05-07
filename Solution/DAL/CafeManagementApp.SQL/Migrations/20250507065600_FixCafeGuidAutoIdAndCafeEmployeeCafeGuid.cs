using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CafeManagementApp.SQL.Migrations
{
    /// <inheritdoc />
    public partial class FixCafeGuidAutoIdAndCafeEmployeeCafeGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CafeEmployees_Cafes_CafeId",
                table: "CafeEmployees");

            migrationBuilder.RenameColumn(
                name: "CafeId",
                table: "CafeEmployees",
                newName: "CafeGuid");

            migrationBuilder.RenameIndex(
                name: "IX_CafeEmployees_CafeId",
                table: "CafeEmployees",
                newName: "IX_CafeEmployees_CafeGuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "CafeGuid",
                table: "Cafes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_CafeEmployees_Cafes_CafeGuid",
                table: "CafeEmployees",
                column: "CafeGuid",
                principalTable: "Cafes",
                principalColumn: "CafeGuid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CafeEmployees_Cafes_CafeGuid",
                table: "CafeEmployees");

            migrationBuilder.RenameColumn(
                name: "CafeGuid",
                table: "CafeEmployees",
                newName: "CafeId");

            migrationBuilder.RenameIndex(
                name: "IX_CafeEmployees_CafeGuid",
                table: "CafeEmployees",
                newName: "IX_CafeEmployees_CafeId");

            migrationBuilder.AlterColumn<Guid>(
                name: "CafeGuid",
                table: "Cafes",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AddForeignKey(
                name: "FK_CafeEmployees_Cafes_CafeId",
                table: "CafeEmployees",
                column: "CafeId",
                principalTable: "Cafes",
                principalColumn: "CafeGuid");
        }
    }
}
