using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessTracker.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JwtId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Used = table.Column<bool>(type: "bit", nullable: false),
                    Invalidated = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Token);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Moderator" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "User" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "PasswordHash", "PasswordSalt", "RoleId", "Surname" },
                values: new object[] { 1, "admin@gmail.com", "Admin Name", new byte[] { 119, 201, 92, 34, 22, 132, 18, 36, 89, 162, 165, 179, 88, 31, 10, 46, 128, 6, 30, 210, 88, 195, 148, 233, 73, 165, 1, 79, 190, 214, 99, 211, 35, 72, 178, 83, 191, 235, 171, 16, 44, 65, 222, 41, 98, 55, 249, 136, 174, 92, 52, 151, 202, 217, 93, 15, 16, 163, 72, 197, 111, 110, 96, 140 }, new byte[] { 125, 130, 5, 238, 229, 63, 48, 65, 142, 85, 232, 205, 252, 73, 86, 107, 178, 221, 38, 42, 201, 100, 247, 40, 204, 30, 5, 179, 172, 100, 89, 30, 223, 237, 107, 238, 88, 38, 169, 169, 65, 119, 23, 157, 209, 246, 6, 185, 63, 103, 29, 113, 216, 235, 40, 34, 152, 217, 144, 145, 105, 222, 186, 176, 50, 97, 161, 54, 245, 121, 71, 143, 105, 73, 232, 103, 121, 133, 188, 106, 40, 96, 74, 245, 32, 240, 183, 228, 9, 109, 150, 205, 7, 188, 213, 177, 195, 225, 180, 181, 28, 31, 82, 142, 107, 230, 205, 42, 233, 47, 234, 119, 232, 144, 66, 83, 153, 12, 81, 19, 7, 62, 98, 213, 188, 191, 112, 77 }, 1, "Admin Surname" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "PasswordHash", "PasswordSalt", "RoleId", "Surname" },
                values: new object[] { 2, "moderator@gmail.com", "Moderator Name", new byte[] { 119, 201, 92, 34, 22, 132, 18, 36, 89, 162, 165, 179, 88, 31, 10, 46, 128, 6, 30, 210, 88, 195, 148, 233, 73, 165, 1, 79, 190, 214, 99, 211, 35, 72, 178, 83, 191, 235, 171, 16, 44, 65, 222, 41, 98, 55, 249, 136, 174, 92, 52, 151, 202, 217, 93, 15, 16, 163, 72, 197, 111, 110, 96, 140 }, new byte[] { 125, 130, 5, 238, 229, 63, 48, 65, 142, 85, 232, 205, 252, 73, 86, 107, 178, 221, 38, 42, 201, 100, 247, 40, 204, 30, 5, 179, 172, 100, 89, 30, 223, 237, 107, 238, 88, 38, 169, 169, 65, 119, 23, 157, 209, 246, 6, 185, 63, 103, 29, 113, 216, 235, 40, 34, 152, 217, 144, 145, 105, 222, 186, 176, 50, 97, 161, 54, 245, 121, 71, 143, 105, 73, 232, 103, 121, 133, 188, 106, 40, 96, 74, 245, 32, 240, 183, 228, 9, 109, 150, 205, 7, 188, 213, 177, 195, 225, 180, 181, 28, 31, 82, 142, 107, 230, 205, 42, 233, 47, 234, 119, 232, 144, 66, 83, 153, 12, 81, 19, 7, 62, 98, 213, 188, 191, 112, 77 }, 2, "Moderator Surname" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "PasswordHash", "PasswordSalt", "RoleId", "Surname" },
                values: new object[] { 3, "user@gmail.com", "User Name", new byte[] { 119, 201, 92, 34, 22, 132, 18, 36, 89, 162, 165, 179, 88, 31, 10, 46, 128, 6, 30, 210, 88, 195, 148, 233, 73, 165, 1, 79, 190, 214, 99, 211, 35, 72, 178, 83, 191, 235, 171, 16, 44, 65, 222, 41, 98, 55, 249, 136, 174, 92, 52, 151, 202, 217, 93, 15, 16, 163, 72, 197, 111, 110, 96, 140 }, new byte[] { 125, 130, 5, 238, 229, 63, 48, 65, 142, 85, 232, 205, 252, 73, 86, 107, 178, 221, 38, 42, 201, 100, 247, 40, 204, 30, 5, 179, 172, 100, 89, 30, 223, 237, 107, 238, 88, 38, 169, 169, 65, 119, 23, 157, 209, 246, 6, 185, 63, 103, 29, 113, 216, 235, 40, 34, 152, 217, 144, 145, 105, 222, 186, 176, 50, 97, 161, 54, 245, 121, 71, 143, 105, 73, 232, 103, 121, 133, 188, 106, 40, 96, 74, 245, 32, 240, 183, 228, 9, 109, 150, 205, 7, 188, 213, 177, 195, 225, 180, 181, 28, 31, 82, 142, 107, 230, 205, 42, 233, 47, 234, 119, 232, 144, 66, 83, 153, 12, 81, 19, 7, 62, 98, 213, 188, 191, 112, 77 }, 3, "User Surname" });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
