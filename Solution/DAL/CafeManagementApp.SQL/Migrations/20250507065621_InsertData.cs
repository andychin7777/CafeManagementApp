using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CafeManagementApp.SQL.Migrations
{
    /// <inheritdoc />
    public partial class InsertData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            AddCafe(migrationBuilder);
            AddEmployee(migrationBuilder);
            AddCafeEmployee(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            ClearAllAndReseed(migrationBuilder);
        }

        private void ClearAllAndReseed(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM CafeEmployees");
            migrationBuilder.Sql("DBCC CHECKIDENT ('CafeEmployees', RESEED, 0)");

            migrationBuilder.Sql("DELETE FROM Cafes");

            migrationBuilder.Sql("DELETE FROM Employees");
            migrationBuilder.Sql("DBCC CHECKIDENT ('Employees', RESEED, 0)");
        }

        private void AddCafe(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cafes",
                columns: new[] { "CafeGuid", "Name", "Description", "Logo", "Location" },
                values: new object[,]
                {
                    { Guid.Parse("a1b2c3d4-e5f6-7890-a1b2-c3d4e5f67890"), "Cafe Mocha", "A cozy cafe with a variety of coffee and snacks.", null, "Downtown" },
                    { Guid.Parse("b2c3d4e5-f678-90a1-b2c3-d4e5f67890a1"), "Sunrise Cafe", "Perfect place for breakfast and brunch.", null, "Uptown" },
                    { Guid.Parse("c3d4e5f6-7890-a1b2-c3d4-e5f67890a1b2"), "Green Leaf Cafe", "A cafe with a focus on organic and healthy options.", null, "Suburbs" },
                    { Guid.Parse("d4e5f678-90a1-b2c3-d4e5-f67890a1b2c3"), "Blue Lagoon Cafe", "A seaside cafe with a relaxing ambiance.", null, "Beachside" },
                    { Guid.Parse("e5f67890-a1b2-c3d4-e5f6-7890a1b2c3d4"), "Mountain View Cafe", "A cafe with stunning mountain views.", null, "Highlands" },
                    { Guid.Parse("f67890a1-b2c3-d4e5-f678-90a1b2c3d4e5"), "Urban Grind", "A modern cafe for city dwellers.", null, "City Center" },
                    { Guid.Parse("67890a1b-2c3d-4e5f-6789-0a1b2c3d4e5f"), "Rustic Retreat", "A rustic-themed cafe in the countryside.", null, "Countryside" },
                    { Guid.Parse("7890a1b2-c3d4-e5f6-7890-a1b2c3d4e5f6"), "The Coffee Corner", "A small corner cafe with great coffee.", null, "Old Town" },
                    { Guid.Parse("890a1b2c-3d4e-5f67-890a-1b2c3d4e5f67"), "Artisan Brews", "A cafe specializing in artisan coffee.", null, "Art District" },
                    { Guid.Parse("90a1b2c3-d4e5-f678-90a1-b2c3d4e5f678"), "The Book Nook Cafe", "A cozy cafe for book lovers.", null, "Library Lane" }
                }
            );
        }

        private void AddEmployee(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeIdString", "Name", "EmailAddress", "PhoneNumber", "Gender" },
                values: new object[,]
                {
                    { "UI0000001", "John Doe", "john.doe@example.com", "81234567", "Male" },
                    { "UI0000002", "Jane Smith", "jane.smith@example.com", "91234567", "Female" },
                    { "UI0000003", "Alice Johnson", "alice.johnson@example.com", "81234568", "Female" },
                    { "UI0000004", "Bob Brown", "bob.brown@example.com", "91234568", "Male" },
                    { "UI0000005", "Charlie Davis", "charlie.davis@example.com", "81234569", "Male" },
                    { "UI0000006", "Diana Evans", "diana.evans@example.com", "91234569", "Female" },
                    { "UI0000007", "Ethan Harris", "ethan.harris@example.com", "81234570", "Male" },
                    { "UI0000008", "Fiona Green", "fiona.green@example.com", "91234570", "Female" },
                    { "UI0000009", "George White", "george.white@example.com", "81234571", "Male" },
                    { "UI0000010", "Hannah Black", "hannah.black@example.com", "91234571", "Female" },
                    { "UI0000011", "Ian Gray", "ian.gray@example.com", "81234572", "Male" },
                    { "UI0000012", "Julia Brown", "julia.brown@example.com", "91234572", "Female" },
                    { "UI0000013", "Kevin Wilson", "kevin.wilson@example.com", "81234573", "Male" },
                    { "UI0000014", "Laura Adams", "laura.adams@example.com", "91234573", "Female" },
                    { "UI0000015", "Michael Scott", "michael.scott@example.com", "81234574", "Male" },
                    { "UI0000016", "Nina Taylor", "nina.taylor@example.com", "91234574", "Female" },
                    { "UI0000017", "Oscar Martinez", "oscar.martinez@example.com", "81234575", "Male" },
                    { "UI0000018", "Paula Walker", "paula.walker@example.com", "91234575", "Female" },
                    { "UI0000019", "Quinn Hall", "quinn.hall@example.com", "81234576", "Male" },
                    { "UI0000020", "Rachel King", "rachel.king@example.com", "91234576", "Female" }
                }
            );
        }

        private void AddCafeEmployee(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CafeEmployees",
                columns: new[] { "CafeGuid", "EmployeeId", "StartDate", "EndDate" },
                values: new object[,]
                {
                    { Guid.Parse("a1b2c3d4-e5f6-7890-a1b2-c3d4e5f67890"), 1, "2025-01-01", null },
                    { Guid.Parse("a1b2c3d4-e5f6-7890-a1b2-c3d4e5f67890"), 2, "2025-01-01", null },
                    { Guid.Parse("b2c3d4e5-f678-90a1-b2c3-d4e5f67890a1"), 3, "2025-01-01", null },
                    { Guid.Parse("b2c3d4e5-f678-90a1-b2c3-d4e5f67890a1"), 4, "2025-01-01", null },
                    { Guid.Parse("c3d4e5f6-7890-a1b2-c3d4-e5f67890a1b2"), 5, "2025-01-01", null },
                    { Guid.Parse("c3d4e5f6-7890-a1b2-c3d4-e5f67890a1b2"), 6, "2025-01-01", null },
                    { Guid.Parse("d4e5f678-90a1-b2c3-d4e5-f67890a1b2c3"), 7, "2025-01-01", null },
                    { Guid.Parse("d4e5f678-90a1-b2c3-d4e5-f67890a1b2c3"), 8, "2025-01-01", null },
                    { Guid.Parse("e5f67890-a1b2-c3d4-e5f6-7890a1b2c3d4"), 9, "2025-01-01", null },
                    { Guid.Parse("e5f67890-a1b2-c3d4-e5f6-7890a1b2c3d4"), 10, "2025-01-01", null },
                    { Guid.Parse("f67890a1-b2c3-d4e5-f678-90a1b2c3d4e5"), 11, "2025-01-01", null },
                    { Guid.Parse("f67890a1-b2c3-d4e5-f678-90a1b2c3d4e5"), 12, "2025-01-01", null },
                    { Guid.Parse("f67890a1-b2c3-d4e5-f678-90a1b2c3d4e5"), 13, "2025-01-01", null },
                    { Guid.Parse("67890a1b-2c3d-4e5f-6789-0a1b2c3d4e5f"), 14, "2025-01-01", null },
                    { Guid.Parse("7890a1b2-c3d4-e5f6-7890-a1b2c3d4e5f6"), 15, "2025-01-01", null },
                    { Guid.Parse("7890a1b2-c3d4-e5f6-7890-a1b2c3d4e5f6"), 16, "2025-01-01", null },
                    { Guid.Parse("890a1b2c-3d4e-5f67-890a-1b2c3d4e5f67"), 17, "2025-01-01", null },
                    { Guid.Parse("890a1b2c-3d4e-5f67-890a-1b2c3d4e5f67"), 18, "2025-01-01", null },
                    { Guid.Parse("90a1b2c3-d4e5-f678-90a1-b2c3d4e5f678"), 19, "2025-01-01", null },
                    { Guid.Parse("90a1b2c3-d4e5-f678-90a1-b2c3d4e5f678"), 20, "2025-01-01", null }
                }
            );
        }
    }
}
