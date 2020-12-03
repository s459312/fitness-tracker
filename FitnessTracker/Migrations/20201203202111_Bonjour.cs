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
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
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
                name: "Training",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Training", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coach",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GoalId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coach", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coach_Goal_GoalId",
                        column: x => x.GoalId,
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
                    GoalId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Goal_GoalId",
                        column: x => x.GoalId,
                        principalTable: "Goal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Exercise",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GoalId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Serie = table.Column<int>(type: "int", nullable: true),
                    Powtorzenia = table.Column<int>(type: "int", nullable: true),
                    Czas = table.Column<int>(type: "int", nullable: true),
                    Obciazenie = table.Column<int>(type: "int", nullable: true),
                    Dystans = table.Column<int>(type: "int", nullable: true),
                    TrainingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercise_Goal_GoalId",
                        column: x => x.GoalId,
                        principalTable: "Goal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Exercise_Training_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Training",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "UserTraining",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TrainingId = table.Column<int>(type: "int", nullable: false),
                    Favourite = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTraining", x => new { x.UserId, x.TrainingId });
                    table.ForeignKey(
                        name: "FK_UserTraining_Training_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Training",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTraining_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "History",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History", x => x.Id);
                    table.ForeignKey(
                        name: "FK_History_Exercise_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_History_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingExercise",
                columns: table => new
                {
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    TrainingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingExercise", x => new { x.ExerciseId, x.TrainingId });
                    table.ForeignKey(
                        name: "FK_TrainingExercise_Exercise_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Serie = table.Column<int>(type: "int", nullable: true),
                    Powtorzenia = table.Column<int>(type: "int", nullable: true),
                    Czas = table.Column<int>(type: "int", nullable: true),
                    Obciazenie = table.Column<int>(type: "int", nullable: true),
                    Dystans = table.Column<int>(type: "int", nullable: true),
                    HistoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryStats_History_HistoryId",
                        column: x => x.HistoryId,
                        principalTable: "History",
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
                table: "Training",
                columns: new[] { "Id", "Description", "IsPublic", "Name" },
                values: new object[,]
                {
                    { 1, null, true, "Trening_Publiczny_1" },
                    { 2, null, false, "Trening_Prywatny_1" },
                    { 3, null, false, "Trening_Prywatny_2" }
                });

            migrationBuilder.InsertData(
                table: "Coach",
                columns: new[] { "Id", "Email", "GoalId", "Name", "Phone", "Surname" },
                values: new object[,]
                {
                    { 1, "coach_1@example.com", 1, "CoachName_1", "123456789", "CoachSurname_1" },
                    { 2, "coach_2@example.com", 2, "CoachName_2", "987654321", "CoachSurname_2" }
                });

            migrationBuilder.InsertData(
                table: "Exercise",
                columns: new[] { "Id", "Czas", "Description", "Dystans", "GoalId", "Name", "Obciazenie", "Powtorzenia", "Serie", "TrainingId" },
                values: new object[,]
                {
                    { 1, null, null, null, 1, "Exercise_1", null, 1, 1, null },
                    { 2, null, null, null, 2, "Exercise_2", null, 2, 2, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "GoalId", "Name", "PasswordHash", "PasswordSalt", "RoleId", "Surname" },
                values: new object[,]
                {
                    { 1, "admin@gmail.com", 2, "Admin Name", new byte[] { 78, 138, 209, 211, 50, 25, 227, 29, 162, 179, 171, 150, 253, 102, 22, 149, 199, 113, 208, 12, 236, 211, 190, 105, 211, 186, 218, 160, 245, 231, 2, 247, 241, 132, 115, 214, 202, 179, 87, 172, 182, 255, 126, 40, 201, 12, 158, 208, 88, 16, 77, 85, 80, 74, 162, 181, 40, 219, 62, 189, 16, 65, 30, 47 }, new byte[] { 78, 79, 227, 106, 202, 225, 25, 219, 39, 125, 13, 154, 17, 87, 251, 120, 36, 52, 175, 105, 188, 7, 186, 193, 180, 198, 248, 51, 6, 175, 45, 99, 148, 215, 164, 218, 158, 219, 71, 245, 180, 107, 225, 18, 232, 90, 100, 113, 92, 194, 127, 126, 202, 74, 196, 187, 113, 178, 239, 239, 254, 252, 84, 34, 161, 254, 81, 213, 223, 0, 50, 21, 20, 130, 216, 134, 7, 195, 145, 123, 136, 51, 13, 102, 14, 113, 95, 127, 108, 31, 90, 105, 59, 45, 212, 72, 17, 122, 23, 181, 94, 61, 214, 67, 139, 180, 169, 181, 12, 72, 113, 3, 91, 241, 114, 223, 130, 245, 80, 84, 147, 68, 52, 43, 205, 42, 10, 205 }, 1, "Admin Surname" },
                    { 2, "moderator@gmail.com", 3, "Moderator Name", new byte[] { 78, 138, 209, 211, 50, 25, 227, 29, 162, 179, 171, 150, 253, 102, 22, 149, 199, 113, 208, 12, 236, 211, 190, 105, 211, 186, 218, 160, 245, 231, 2, 247, 241, 132, 115, 214, 202, 179, 87, 172, 182, 255, 126, 40, 201, 12, 158, 208, 88, 16, 77, 85, 80, 74, 162, 181, 40, 219, 62, 189, 16, 65, 30, 47 }, new byte[] { 78, 79, 227, 106, 202, 225, 25, 219, 39, 125, 13, 154, 17, 87, 251, 120, 36, 52, 175, 105, 188, 7, 186, 193, 180, 198, 248, 51, 6, 175, 45, 99, 148, 215, 164, 218, 158, 219, 71, 245, 180, 107, 225, 18, 232, 90, 100, 113, 92, 194, 127, 126, 202, 74, 196, 187, 113, 178, 239, 239, 254, 252, 84, 34, 161, 254, 81, 213, 223, 0, 50, 21, 20, 130, 216, 134, 7, 195, 145, 123, 136, 51, 13, 102, 14, 113, 95, 127, 108, 31, 90, 105, 59, 45, 212, 72, 17, 122, 23, 181, 94, 61, 214, 67, 139, 180, 169, 181, 12, 72, 113, 3, 91, 241, 114, 223, 130, 245, 80, 84, 147, 68, 52, 43, 205, 42, 10, 205 }, 2, "Moderator Surname" },
                    { 3, "user@gmail.com", 1, "User Name", new byte[] { 78, 138, 209, 211, 50, 25, 227, 29, 162, 179, 171, 150, 253, 102, 22, 149, 199, 113, 208, 12, 236, 211, 190, 105, 211, 186, 218, 160, 245, 231, 2, 247, 241, 132, 115, 214, 202, 179, 87, 172, 182, 255, 126, 40, 201, 12, 158, 208, 88, 16, 77, 85, 80, 74, 162, 181, 40, 219, 62, 189, 16, 65, 30, 47 }, new byte[] { 78, 79, 227, 106, 202, 225, 25, 219, 39, 125, 13, 154, 17, 87, 251, 120, 36, 52, 175, 105, 188, 7, 186, 193, 180, 198, 248, 51, 6, 175, 45, 99, 148, 215, 164, 218, 158, 219, 71, 245, 180, 107, 225, 18, 232, 90, 100, 113, 92, 194, 127, 126, 202, 74, 196, 187, 113, 178, 239, 239, 254, 252, 84, 34, 161, 254, 81, 213, 223, 0, 50, 21, 20, 130, 216, 134, 7, 195, 145, 123, 136, 51, 13, 102, 14, 113, 95, 127, 108, 31, 90, 105, 59, 45, 212, 72, 17, 122, 23, 181, 94, 61, 214, 67, 139, 180, 169, 181, 12, 72, 113, 3, 91, 241, 114, 223, 130, 245, 80, 84, 147, 68, 52, 43, 205, 42, 10, 205 }, 3, "User Surname" }
                });

            migrationBuilder.InsertData(
                table: "History",
                columns: new[] { "Id", "Date", "ExerciseId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2020, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 2, new DateTime(2020, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "TrainingExercise",
                columns: new[] { "ExerciseId", "TrainingId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 1 },
                    { 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "UserTraining",
                columns: new[] { "TrainingId", "UserId", "Favourite" },
                values: new object[,]
                {
                    { 1, 1, false },
                    { 2, 1, false },
                    { 3, 1, true }
                });

            migrationBuilder.InsertData(
                table: "HistoryStats",
                columns: new[] { "Id", "Czas", "Dystans", "HistoryId", "Obciazenie", "Powtorzenia", "Serie" },
                values: new object[] { 1, null, null, 1, null, 1, 1 });

            migrationBuilder.InsertData(
                table: "HistoryStats",
                columns: new[] { "Id", "Czas", "Dystans", "HistoryId", "Obciazenie", "Powtorzenia", "Serie" },
                values: new object[] { 2, null, null, 2, null, 2, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Coach_GoalId",
                table: "Coach",
                column: "GoalId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_GoalId",
                table: "Exercise",
                column: "GoalId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_TrainingId",
                table: "Exercise",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_History_ExerciseId_UserId_Date",
                table: "History",
                columns: new[] { "ExerciseId", "UserId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_History_UserId",
                table: "History",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryStats_HistoryId",
                table: "HistoryStats",
                column: "HistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
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
                name: "IX_UserTraining_TrainingId",
                table: "UserTraining",
                column: "TrainingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coach");

            migrationBuilder.DropTable(
                name: "HistoryStats");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "TrainingExercise");

            migrationBuilder.DropTable(
                name: "UserTraining");

            migrationBuilder.DropTable(
                name: "History");

            migrationBuilder.DropTable(
                name: "Exercise");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Training");

            migrationBuilder.DropTable(
                name: "Goal");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
