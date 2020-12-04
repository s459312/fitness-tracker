using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FitnessTracker.Migrations
{
    public partial class Initial : Migration
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
                    Date = table.Column<DateTime>(type: "Date", nullable: false)
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
                    { 3, null, false, "Trening_Prywatny_2" },
                    { 4, null, true, "Trening_Publiczny_2" }
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
                    { 1, null, null, null, 1, "Przysiad ze sztangą", null, 8, 4, null },
                    { 2, null, null, null, 3, "Wykroki ze sztangielkami", null, 8, 4, null },
                    { 3, null, null, null, 3, "Przysiad w szerokim rozkroku", null, 8, 4, null },
                    { 4, null, null, null, 3, "Wyciskanie sztangielek w pozycji leżącej", null, 12, 3, null },
                    { 5, null, null, null, 3, "Brzuszki z nogami uniesionymi", null, 25, 3, null },
                    { 6, null, null, null, 3, "Brzuszki skośne", null, 25, 3, null },
                    { 7, 30, null, null, 3, "Plank", null, null, 3, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "GoalId", "Name", "PasswordHash", "PasswordSalt", "RoleId", "Surname" },
                values: new object[,]
                {
                    { 1, "admin@gmail.com", 2, "Admin Name", new byte[] { 251, 142, 167, 240, 0, 112, 131, 3, 186, 79, 72, 200, 25, 148, 124, 10, 44, 20, 72, 83, 207, 253, 134, 111, 133, 48, 101, 118, 210, 168, 145, 110, 176, 6, 94, 187, 220, 18, 118, 56, 28, 213, 30, 135, 195, 130, 74, 199, 19, 165, 57, 135, 31, 164, 48, 54, 88, 113, 175, 235, 36, 16, 143, 185 }, new byte[] { 115, 102, 209, 4, 86, 94, 42, 255, 122, 18, 83, 120, 136, 146, 98, 7, 115, 82, 138, 254, 233, 184, 23, 255, 33, 234, 48, 191, 202, 120, 156, 92, 133, 175, 36, 181, 240, 172, 163, 8, 55, 221, 194, 140, 168, 122, 170, 144, 213, 247, 233, 163, 3, 214, 8, 114, 138, 89, 103, 143, 245, 11, 0, 192, 202, 145, 18, 165, 154, 249, 16, 27, 207, 57, 27, 39, 235, 204, 246, 98, 11, 55, 131, 139, 129, 116, 197, 238, 56, 247, 26, 133, 42, 53, 139, 46, 100, 79, 162, 168, 103, 204, 105, 236, 110, 197, 223, 42, 224, 97, 116, 72, 177, 228, 249, 22, 163, 253, 54, 113, 30, 156, 204, 107, 166, 44, 29, 250 }, 1, "Admin Surname" },
                    { 2, "moderator@gmail.com", 3, "Moderator Name", new byte[] { 251, 142, 167, 240, 0, 112, 131, 3, 186, 79, 72, 200, 25, 148, 124, 10, 44, 20, 72, 83, 207, 253, 134, 111, 133, 48, 101, 118, 210, 168, 145, 110, 176, 6, 94, 187, 220, 18, 118, 56, 28, 213, 30, 135, 195, 130, 74, 199, 19, 165, 57, 135, 31, 164, 48, 54, 88, 113, 175, 235, 36, 16, 143, 185 }, new byte[] { 115, 102, 209, 4, 86, 94, 42, 255, 122, 18, 83, 120, 136, 146, 98, 7, 115, 82, 138, 254, 233, 184, 23, 255, 33, 234, 48, 191, 202, 120, 156, 92, 133, 175, 36, 181, 240, 172, 163, 8, 55, 221, 194, 140, 168, 122, 170, 144, 213, 247, 233, 163, 3, 214, 8, 114, 138, 89, 103, 143, 245, 11, 0, 192, 202, 145, 18, 165, 154, 249, 16, 27, 207, 57, 27, 39, 235, 204, 246, 98, 11, 55, 131, 139, 129, 116, 197, 238, 56, 247, 26, 133, 42, 53, 139, 46, 100, 79, 162, 168, 103, 204, 105, 236, 110, 197, 223, 42, 224, 97, 116, 72, 177, 228, 249, 22, 163, 253, 54, 113, 30, 156, 204, 107, 166, 44, 29, 250 }, 2, "Moderator Surname" },
                    { 3, "user@gmail.com", 1, "User Name", new byte[] { 251, 142, 167, 240, 0, 112, 131, 3, 186, 79, 72, 200, 25, 148, 124, 10, 44, 20, 72, 83, 207, 253, 134, 111, 133, 48, 101, 118, 210, 168, 145, 110, 176, 6, 94, 187, 220, 18, 118, 56, 28, 213, 30, 135, 195, 130, 74, 199, 19, 165, 57, 135, 31, 164, 48, 54, 88, 113, 175, 235, 36, 16, 143, 185 }, new byte[] { 115, 102, 209, 4, 86, 94, 42, 255, 122, 18, 83, 120, 136, 146, 98, 7, 115, 82, 138, 254, 233, 184, 23, 255, 33, 234, 48, 191, 202, 120, 156, 92, 133, 175, 36, 181, 240, 172, 163, 8, 55, 221, 194, 140, 168, 122, 170, 144, 213, 247, 233, 163, 3, 214, 8, 114, 138, 89, 103, 143, 245, 11, 0, 192, 202, 145, 18, 165, 154, 249, 16, 27, 207, 57, 27, 39, 235, 204, 246, 98, 11, 55, 131, 139, 129, 116, 197, 238, 56, 247, 26, 133, 42, 53, 139, 46, 100, 79, 162, 168, 103, 204, 105, 236, 110, 197, 223, 42, 224, 97, 116, 72, 177, 228, 249, 22, 163, 253, 54, 113, 30, 156, 204, 107, 166, 44, 29, 250 }, 3, "User Surname" }
                });

            migrationBuilder.InsertData(
                table: "History",
                columns: new[] { "Id", "Date", "ExerciseId", "UserId" },
                values: new object[,]
                {
                    { 4, new DateTime(2020, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1 },
                    { 3, new DateTime(2020, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 2, new DateTime(2020, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1 },
                    { 1, new DateTime(2020, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "TrainingExercise",
                columns: new[] { "ExerciseId", "TrainingId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 7, 3 },
                    { 6, 3 },
                    { 5, 4 },
                    { 5, 2 },
                    { 6, 2 },
                    { 4, 1 },
                    { 3, 4 },
                    { 3, 1 },
                    { 2, 4 },
                    { 2, 3 },
                    { 2, 1 },
                    { 1, 3 },
                    { 1, 2 },
                    { 4, 4 }
                });

            migrationBuilder.InsertData(
                table: "UserTraining",
                columns: new[] { "TrainingId", "UserId", "Favourite" },
                values: new object[,]
                {
                    { 3, 3, false },
                    { 1, 1, false },
                    { 2, 1, false },
                    { 3, 1, true },
                    { 3, 2, false },
                    { 4, 3, true }
                });

            migrationBuilder.InsertData(
                table: "HistoryStats",
                columns: new[] { "Id", "Czas", "Dystans", "HistoryId", "Obciazenie", "Powtorzenia", "Serie" },
                values: new object[,]
                {
                    { 1, null, null, 1, null, 1, 1 },
                    { 2, null, null, 1, null, 4, 2 },
                    { 3, null, null, 1, null, 6, 3 },
                    { 4, null, null, 2, null, 3, 1 },
                    { 5, null, null, 2, null, 6, 2 },
                    { 6, null, null, 2, null, 8, 3 },
                    { 7, null, null, 3, null, 9, 7 },
                    { 8, null, null, 3, null, 2, 2 },
                    { 9, null, null, 4, null, 1, 8 },
                    { 10, null, null, 4, null, 5, 4 }
                });

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
