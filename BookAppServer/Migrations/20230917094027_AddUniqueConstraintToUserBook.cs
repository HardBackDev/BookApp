using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookAppServer.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintToUserBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserBooks_UserId",
                table: "UserBooks");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "76c97f52-fcce-41e3-acfa-7e6b5264eaf9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8c577cb4-aa71-4fab-a1b0-533a3f6dabcf");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3e498a70-5003-4c7e-992f-f17289fb52ce", null, "Admin", "ADMIN" },
                    { "54e9c26d-721b-4b7e-83db-fbe5752c9084", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserBooks_UserId_BookId",
                table: "UserBooks",
                columns: new[] { "UserId", "BookId" },
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserBooks_UserId_BookId",
                table: "UserBooks");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3e498a70-5003-4c7e-992f-f17289fb52ce");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "54e9c26d-721b-4b7e-83db-fbe5752c9084");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "76c97f52-fcce-41e3-acfa-7e6b5264eaf9", null, "Admin", "ADMIN" },
                    { "8c577cb4-aa71-4fab-a1b0-533a3f6dabcf", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserBooks_UserId",
                table: "UserBooks",
                column: "UserId");
        }
    }
}
