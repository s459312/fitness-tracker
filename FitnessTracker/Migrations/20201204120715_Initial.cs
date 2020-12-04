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
                    { 1, "admin@gmail.com", 2, "Admin Name", new byte[] { 224, 233, 64, 141, 111, 254, 250, 42, 31, 11, 216, 163, 185, 233, 2, 253, 18, 145, 154, 67, 33, 203, 57, 76, 203, 153, 231, 38, 93, 172, 117, 2, 166, 191, 204, 142, 188, 169, 8, 170, 39, 72, 93, 100, 172, 102, 211, 201, 20, 102, 72, 207, 30, 120, 211, 47, 251, 92, 149, 165, 247, 232, 177, 202 }, new byte[] { 237, 18, 4, 250, 240, 113, 136, 55, 251, 2, 164, 219, 60, 187, 47, 106, 18, 27, 43, 86, 82, 115, 187, 119, 182, 129, 111, 5, 205, 249, 162, 217, 43, 118, 181, 79, 89, 212, 30, 200, 70, 233, 23, 12, 56, 49, 8, 25, 36, 73, 113, 206, 107, 171, 11, 212, 15, 162, 213, 1, 102, 60, 188, 44, 252, 50, 195, 123, 83, 31, 229, 209, 247, 57, 137, 222, 212, 178, 131, 108, 212, 118, 85, 178, 3, 90, 230, 210, 15, 68, 241, 253, 80, 188, 94, 173, 91, 206, 173, 219, 151, 42, 110, 77, 44, 209, 13, 167, 10, 86, 143, 88, 116, 234, 190, 226, 35, 177, 211, 127, 8, 148, 235, 153, 76, 197, 68, 6 }, 1, "Admin Surname" },
                    { 2, "moderator@gmail.com", 3, "Moderator Name", new byte[] { 224, 233, 64, 141, 111, 254, 250, 42, 31, 11, 216, 163, 185, 233, 2, 253, 18, 145, 154, 67, 33, 203, 57, 76, 203, 153, 231, 38, 93, 172, 117, 2, 166, 191, 204, 142, 188, 169, 8, 170, 39, 72, 93, 100, 172, 102, 211, 201, 20, 102, 72, 207, 30, 120, 211, 47, 251, 92, 149, 165, 247, 232, 177, 202 }, new byte[] { 237, 18, 4, 250, 240, 113, 136, 55, 251, 2, 164, 219, 60, 187, 47, 106, 18, 27, 43, 86, 82, 115, 187, 119, 182, 129, 111, 5, 205, 249, 162, 217, 43, 118, 181, 79, 89, 212, 30, 200, 70, 233, 23, 12, 56, 49, 8, 25, 36, 73, 113, 206, 107, 171, 11, 212, 15, 162, 213, 1, 102, 60, 188, 44, 252, 50, 195, 123, 83, 31, 229, 209, 247, 57, 137, 222, 212, 178, 131, 108, 212, 118, 85, 178, 3, 90, 230, 210, 15, 68, 241, 253, 80, 188, 94, 173, 91, 206, 173, 219, 151, 42, 110, 77, 44, 209, 13, 167, 10, 86, 143, 88, 116, 234, 190, 226, 35, 177, 211, 127, 8, 148, 235, 153, 76, 197, 68, 6 }, 2, "Moderator Surname" },
                    { 3, "user@gmail.com", 1, "User Name", new byte[] { 224, 233, 64, 141, 111, 254, 250, 42, 31, 11, 216, 163, 185, 233, 2, 253, 18, 145, 154, 67, 33, 203, 57, 76, 203, 153, 231, 38, 93, 172, 117, 2, 166, 191, 204, 142, 188, 169, 8, 170, 39, 72, 93, 100, 172, 102, 211, 201, 20, 102, 72, 207, 30, 120, 211, 47, 251, 92, 149, 165, 247, 232, 177, 202 }, new byte[] { 237, 18, 4, 250, 240, 113, 136, 55, 251, 2, 164, 219, 60, 187, 47, 106, 18, 27, 43, 86, 82, 115, 187, 119, 182, 129, 111, 5, 205, 249, 162, 217, 43, 118, 181, 79, 89, 212, 30, 200, 70, 233, 23, 12, 56, 49, 8, 25, 36, 73, 113, 206, 107, 171, 11, 212, 15, 162, 213, 1, 102, 60, 188, 44, 252, 50, 195, 123, 83, 31, 229, 209, 247, 57, 137, 222, 212, 178, 131, 108, 212, 118, 85, 178, 3, 90, 230, 210, 15, 68, 241, 253, 80, 188, 94, 173, 91, 206, 173, 219, 151, 42, 110, 77, 44, 209, 13, 167, 10, 86, 143, 88, 116, 234, 190, 226, 35, 177, 211, 127, 8, 148, 235, 153, 76, 197, 68, 6 }, 3, "User Surname" }
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
                    { 5, new DateTime(2020, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2 },
                    { 4, new DateTime(2020, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2 },
                    { 1, new DateTime(2020, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 2, new DateTime(2020, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1 },
                    { 3, new DateTime(2020, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 1 }
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
