using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessTracker.Migrations
{
    public partial class Bonjour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Goal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goal", x => x.Id);
                });

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
                name: "Coach",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdGoalId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coach", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coach_Goal_IdGoalId",
                        column: x => x.IdGoalId,
                        principalTable: "Goal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Exercise",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdGoalId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Serie = table.Column<int>(type: "int", nullable: true),
                    Powtorzenia = table.Column<int>(type: "int", nullable: true),
                    Czas = table.Column<int>(type: "int", nullable: true),
                    Obciazenie = table.Column<int>(type: "int", nullable: true),
                    Dystans = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercise_Goal_IdGoalId",
                        column: x => x.IdGoalId,
                        principalTable: "Goal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    GoalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Goal_GoalId",
                        column: x => x.GoalId,
                        principalTable: "Goal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "History",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    IdExercise = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "Date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History", x => new { x.IdUser, x.IdExercise, x.Date });
                    table.ForeignKey(
                        name: "FK_History_Exercise_IdExercise",
                        column: x => x.IdExercise,
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_History_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
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

            migrationBuilder.CreateTable(
                name: "Training",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Training", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Training_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistoryStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdHistoryIdUser = table.Column<int>(type: "int", nullable: true),
                    IdHistoryIdExercise = table.Column<int>(type: "int", nullable: true),
                    IdHistoryDate = table.Column<DateTime>(type: "Date", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Serie = table.Column<int>(type: "int", nullable: true),
                    Powtorzenia = table.Column<int>(type: "int", nullable: true),
                    Czas = table.Column<int>(type: "int", nullable: true),
                    Obciazenie = table.Column<int>(type: "int", nullable: true),
                    Dystans = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryStats_History_IdHistoryIdUser_IdHistoryIdExercise_IdHistoryDate",
                        columns: x => new { x.IdHistoryIdUser, x.IdHistoryIdExercise, x.IdHistoryDate },
                        principalTable: "History",
                        principalColumns: new[] { "IdUser", "IdExercise", "Date" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseTraining",
                columns: table => new
                {
                    IdExercise = table.Column<int>(type: "int", nullable: false),
                    IdTraining = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseTraining", x => new { x.IdExercise, x.IdTraining });
                    table.ForeignKey(
                        name: "FK_ExerciseTraining_Exercise_IdExercise",
                        column: x => x.IdExercise,
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseTraining_Training_IdTraining",
                        column: x => x.IdTraining,
                        principalTable: "Training",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTraining",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false),
                    IdTraining = table.Column<int>(type: "int", nullable: false),
                    Favourite = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTraining", x => new { x.IdUser, x.IdTraining });
                    table.ForeignKey(
                        name: "FK_UserTraining_Training_IdTraining",
                        column: x => x.IdTraining,
                        principalTable: "Training",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTraining_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Goal",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Redukcja tkanki tłuszczowej" },
                    { 2, "Przybranie masy mięśniowej" },
                    { 3, "Rekompozycja sylwetki" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Moderator" },
                    { 3, "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "GoalId", "Name", "PasswordHash", "PasswordSalt", "RoleId", "Surname" },
                values: new object[] { 1, "admin@gmail.com", 2, "Admin Name", new byte[] { 207, 11, 19, 35, 127, 93, 30, 172, 40, 24, 22, 93, 58, 178, 139, 86, 228, 148, 218, 202, 11, 145, 83, 8, 171, 104, 27, 200, 129, 63, 23, 64, 48, 65, 54, 71, 36, 13, 254, 212, 66, 96, 233, 144, 230, 71, 82, 56, 73, 82, 239, 160, 90, 193, 98, 29, 22, 81, 220, 251, 239, 186, 110, 107 }, new byte[] { 148, 14, 94, 23, 240, 241, 184, 152, 20, 95, 237, 68, 204, 234, 113, 43, 4, 93, 242, 26, 122, 249, 97, 79, 52, 106, 186, 18, 99, 120, 46, 91, 40, 62, 239, 229, 42, 134, 246, 7, 118, 123, 86, 136, 92, 217, 42, 111, 182, 209, 81, 161, 62, 71, 51, 164, 22, 71, 9, 240, 237, 180, 104, 1, 252, 255, 146, 247, 225, 76, 103, 204, 13, 101, 129, 247, 21, 8, 25, 40, 57, 95, 151, 77, 71, 241, 110, 219, 221, 108, 85, 194, 251, 126, 205, 181, 61, 19, 70, 33, 106, 15, 164, 135, 179, 136, 109, 50, 62, 167, 155, 129, 62, 119, 166, 12, 150, 179, 101, 194, 77, 135, 180, 231, 114, 138, 117, 216 }, 1, "Admin Surname" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "GoalId", "Name", "PasswordHash", "PasswordSalt", "RoleId", "Surname" },
                values: new object[] { 2, "moderator@gmail.com", 3, "Moderator Name", new byte[] { 207, 11, 19, 35, 127, 93, 30, 172, 40, 24, 22, 93, 58, 178, 139, 86, 228, 148, 218, 202, 11, 145, 83, 8, 171, 104, 27, 200, 129, 63, 23, 64, 48, 65, 54, 71, 36, 13, 254, 212, 66, 96, 233, 144, 230, 71, 82, 56, 73, 82, 239, 160, 90, 193, 98, 29, 22, 81, 220, 251, 239, 186, 110, 107 }, new byte[] { 148, 14, 94, 23, 240, 241, 184, 152, 20, 95, 237, 68, 204, 234, 113, 43, 4, 93, 242, 26, 122, 249, 97, 79, 52, 106, 186, 18, 99, 120, 46, 91, 40, 62, 239, 229, 42, 134, 246, 7, 118, 123, 86, 136, 92, 217, 42, 111, 182, 209, 81, 161, 62, 71, 51, 164, 22, 71, 9, 240, 237, 180, 104, 1, 252, 255, 146, 247, 225, 76, 103, 204, 13, 101, 129, 247, 21, 8, 25, 40, 57, 95, 151, 77, 71, 241, 110, 219, 221, 108, 85, 194, 251, 126, 205, 181, 61, 19, 70, 33, 106, 15, 164, 135, 179, 136, 109, 50, 62, 167, 155, 129, 62, 119, 166, 12, 150, 179, 101, 194, 77, 135, 180, 231, 114, 138, 117, 216 }, 2, "Moderator Surname" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "GoalId", "Name", "PasswordHash", "PasswordSalt", "RoleId", "Surname" },
                values: new object[] { 3, "user@gmail.com", 1, "User Name", new byte[] { 207, 11, 19, 35, 127, 93, 30, 172, 40, 24, 22, 93, 58, 178, 139, 86, 228, 148, 218, 202, 11, 145, 83, 8, 171, 104, 27, 200, 129, 63, 23, 64, 48, 65, 54, 71, 36, 13, 254, 212, 66, 96, 233, 144, 230, 71, 82, 56, 73, 82, 239, 160, 90, 193, 98, 29, 22, 81, 220, 251, 239, 186, 110, 107 }, new byte[] { 148, 14, 94, 23, 240, 241, 184, 152, 20, 95, 237, 68, 204, 234, 113, 43, 4, 93, 242, 26, 122, 249, 97, 79, 52, 106, 186, 18, 99, 120, 46, 91, 40, 62, 239, 229, 42, 134, 246, 7, 118, 123, 86, 136, 92, 217, 42, 111, 182, 209, 81, 161, 62, 71, 51, 164, 22, 71, 9, 240, 237, 180, 104, 1, 252, 255, 146, 247, 225, 76, 103, 204, 13, 101, 129, 247, 21, 8, 25, 40, 57, 95, 151, 77, 71, 241, 110, 219, 221, 108, 85, 194, 251, 126, 205, 181, 61, 19, 70, 33, 106, 15, 164, 135, 179, 136, 109, 50, 62, 167, 155, 129, 62, 119, 166, 12, 150, 179, 101, 194, 77, 135, 180, 231, 114, 138, 117, 216 }, 3, "User Surname" });

            migrationBuilder.CreateIndex(
                name: "IX_Coach_IdGoalId",
                table: "Coach",
                column: "IdGoalId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_IdGoalId",
                table: "Exercise",
                column: "IdGoalId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseTraining_IdTraining",
                table: "ExerciseTraining",
                column: "IdTraining");

            migrationBuilder.CreateIndex(
                name: "IX_History_IdExercise",
                table: "History",
                column: "IdExercise");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryStats_IdHistoryIdUser_IdHistoryIdExercise_IdHistoryDate",
                table: "HistoryStats",
                columns: new[] { "IdHistoryIdUser", "IdHistoryIdExercise", "IdHistoryDate" });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Training_UserId",
                table: "Training",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_GoalId",
                table: "Users",
                column: "GoalId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTraining_IdTraining",
                table: "UserTraining",
                column: "IdTraining");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coach");

            migrationBuilder.DropTable(
                name: "ExerciseTraining");

            migrationBuilder.DropTable(
                name: "HistoryStats");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "UserTraining");

            migrationBuilder.DropTable(
                name: "History");

            migrationBuilder.DropTable(
                name: "Training");

            migrationBuilder.DropTable(
                name: "Exercise");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Goal");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
