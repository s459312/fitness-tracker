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
                    Dystans = table.Column<int>(type: "int", nullable: true)
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
                    table.ForeignKey(
                        name: "FK_TrainingExercise_Training_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Training",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseHistory",
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
                    table.PrimaryKey("PK_ExerciseHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseHistory_Exercise_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseHistory_Users_UserId",
                        column: x => x.UserId,
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
                name: "TrainingHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TrainingId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "Date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingHistory_Training_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Training",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainingHistory_Users_UserId",
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
                name: "ExerciseHistoryStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Serie = table.Column<int>(type: "int", nullable: true),
                    Powtorzenia = table.Column<int>(type: "int", nullable: true),
                    Czas = table.Column<int>(type: "int", nullable: true),
                    Obciazenie = table.Column<int>(type: "int", nullable: true),
                    Dystans = table.Column<int>(type: "int", nullable: true),
                    ExerciseHistoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseHistoryStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseHistoryStats_ExerciseHistory_ExerciseHistoryId",
                        column: x => x.ExerciseHistoryId,
                        principalTable: "ExerciseHistory",
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
                columns: new[] { "Id", "Czas", "Description", "Dystans", "GoalId", "Name", "Obciazenie", "Powtorzenia", "Serie" },
                values: new object[,]
                {
                    { 1, null, null, null, 1, "Przysiad ze sztangą", null, 8, 4 },
                    { 2, null, null, null, 3, "Wykroki ze sztangielkami", null, 8, 4 },
                    { 3, null, null, null, 3, "Przysiad w szerokim rozkroku", null, 8, 4 },
                    { 4, null, null, null, 3, "Wyciskanie sztangielek w pozycji leżącej", null, 12, 3 },
                    { 5, null, null, null, 3, "Brzuszki z nogami uniesionymi", null, 25, 3 },
                    { 6, null, null, null, 3, "Brzuszki skośne", null, 25, 3 },
                    { 7, 30, null, null, 3, "Plank", null, null, 3 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "GoalId", "Name", "PasswordHash", "PasswordSalt", "RoleId", "Surname" },
                values: new object[,]
                {
                    { 1, "admin@gmail.com", 2, "Admin Name", new byte[] { 110, 251, 54, 176, 130, 54, 246, 35, 251, 240, 183, 201, 227, 127, 92, 164, 149, 110, 135, 249, 244, 254, 176, 13, 209, 5, 219, 206, 255, 36, 41, 17, 208, 158, 181, 151, 158, 30, 141, 228, 108, 217, 137, 0, 152, 158, 147, 91, 154, 195, 185, 134, 251, 254, 234, 105, 203, 134, 21, 249, 217, 59, 185, 139 }, new byte[] { 34, 243, 144, 69, 135, 107, 22, 116, 177, 30, 24, 132, 167, 135, 136, 209, 77, 156, 7, 57, 98, 223, 114, 169, 6, 247, 165, 15, 100, 51, 57, 100, 44, 201, 162, 13, 119, 84, 163, 116, 61, 127, 181, 31, 3, 167, 211, 255, 174, 25, 16, 191, 182, 191, 158, 52, 45, 79, 191, 68, 212, 42, 240, 131, 239, 236, 220, 51, 1, 153, 28, 198, 26, 236, 35, 34, 17, 95, 224, 232, 100, 31, 240, 1, 1, 156, 52, 167, 27, 238, 98, 35, 246, 68, 155, 173, 44, 56, 231, 167, 102, 122, 219, 194, 40, 113, 196, 79, 236, 110, 209, 28, 115, 127, 152, 1, 67, 242, 86, 135, 54, 247, 193, 67, 42, 101, 169, 50 }, 1, "Admin Surname" },
                    { 2, "moderator@gmail.com", 3, "Moderator Name", new byte[] { 110, 251, 54, 176, 130, 54, 246, 35, 251, 240, 183, 201, 227, 127, 92, 164, 149, 110, 135, 249, 244, 254, 176, 13, 209, 5, 219, 206, 255, 36, 41, 17, 208, 158, 181, 151, 158, 30, 141, 228, 108, 217, 137, 0, 152, 158, 147, 91, 154, 195, 185, 134, 251, 254, 234, 105, 203, 134, 21, 249, 217, 59, 185, 139 }, new byte[] { 34, 243, 144, 69, 135, 107, 22, 116, 177, 30, 24, 132, 167, 135, 136, 209, 77, 156, 7, 57, 98, 223, 114, 169, 6, 247, 165, 15, 100, 51, 57, 100, 44, 201, 162, 13, 119, 84, 163, 116, 61, 127, 181, 31, 3, 167, 211, 255, 174, 25, 16, 191, 182, 191, 158, 52, 45, 79, 191, 68, 212, 42, 240, 131, 239, 236, 220, 51, 1, 153, 28, 198, 26, 236, 35, 34, 17, 95, 224, 232, 100, 31, 240, 1, 1, 156, 52, 167, 27, 238, 98, 35, 246, 68, 155, 173, 44, 56, 231, 167, 102, 122, 219, 194, 40, 113, 196, 79, 236, 110, 209, 28, 115, 127, 152, 1, 67, 242, 86, 135, 54, 247, 193, 67, 42, 101, 169, 50 }, 2, "Moderator Surname" },
                    { 3, "user@gmail.com", 1, "User Name", new byte[] { 110, 251, 54, 176, 130, 54, 246, 35, 251, 240, 183, 201, 227, 127, 92, 164, 149, 110, 135, 249, 244, 254, 176, 13, 209, 5, 219, 206, 255, 36, 41, 17, 208, 158, 181, 151, 158, 30, 141, 228, 108, 217, 137, 0, 152, 158, 147, 91, 154, 195, 185, 134, 251, 254, 234, 105, 203, 134, 21, 249, 217, 59, 185, 139 }, new byte[] { 34, 243, 144, 69, 135, 107, 22, 116, 177, 30, 24, 132, 167, 135, 136, 209, 77, 156, 7, 57, 98, 223, 114, 169, 6, 247, 165, 15, 100, 51, 57, 100, 44, 201, 162, 13, 119, 84, 163, 116, 61, 127, 181, 31, 3, 167, 211, 255, 174, 25, 16, 191, 182, 191, 158, 52, 45, 79, 191, 68, 212, 42, 240, 131, 239, 236, 220, 51, 1, 153, 28, 198, 26, 236, 35, 34, 17, 95, 224, 232, 100, 31, 240, 1, 1, 156, 52, 167, 27, 238, 98, 35, 246, 68, 155, 173, 44, 56, 231, 167, 102, 122, 219, 194, 40, 113, 196, 79, 236, 110, 209, 28, 115, 127, 152, 1, 67, 242, 86, 135, 54, 247, 193, 67, 42, 101, 169, 50 }, 3, "User Surname" }
                });

            migrationBuilder.InsertData(
                table: "ExerciseHistory",
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
                    { 6, 3 },
                    { 6, 2 },
                    { 5, 4 },
                    { 5, 2 },
                    { 7, 3 },
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
                table: "TrainingHistory",
                columns: new[] { "Id", "Date", "TrainingId", "UserId" },
                values: new object[,]
                {
                    { 3, new DateTime(2020, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2 },
                    { 2, new DateTime(2020, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2 },
                    { 1, new DateTime(2020, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 1 },
                    { 4, new DateTime(2020, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 5, new DateTime(2020, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "UserTraining",
                columns: new[] { "TrainingId", "UserId", "Favourite" },
                values: new object[,]
                {
                    { 1, 1, false },
                    { 2, 1, false },
                    { 3, 1, true },
                    { 3, 3, false },
                    { 3, 2, false },
                    { 4, 3, true }
                });

            migrationBuilder.InsertData(
                table: "ExerciseHistoryStats",
                columns: new[] { "Id", "Czas", "Dystans", "ExerciseHistoryId", "Obciazenie", "Powtorzenia", "Serie" },
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
                name: "IX_ExerciseHistory_ExerciseId_UserId_Date",
                table: "ExerciseHistory",
                columns: new[] { "ExerciseId", "UserId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseHistory_UserId",
                table: "ExerciseHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseHistoryStats_ExerciseHistoryId",
                table: "ExerciseHistoryStats",
                column: "ExerciseHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingExercise_TrainingId",
                table: "TrainingExercise",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingHistory_TrainingId",
                table: "TrainingHistory",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingHistory_UserId",
                table: "TrainingHistory",
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
                name: "ExerciseHistoryStats");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "TrainingExercise");

            migrationBuilder.DropTable(
                name: "TrainingHistory");

            migrationBuilder.DropTable(
                name: "UserTraining");

            migrationBuilder.DropTable(
                name: "ExerciseHistory");

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
