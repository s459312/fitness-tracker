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
                    GoalId = table.Column<int>(type: "int", nullable: false)
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
                    GoalId = table.Column<int>(type: "int", nullable: false),
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
                    IdExercise = table.Column<int>(type: "int", nullable: false),
                    IdTraining = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingExercise", x => new { x.IdExercise, x.IdTraining });
                    table.ForeignKey(
                        name: "FK_TrainingExercise_Exercise_IdExercise",
                        column: x => x.IdExercise,
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainingExercise_Training_IdTraining",
                        column: x => x.IdTraining,
                        principalTable: "Training",
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
                        onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.CreateTable(
                name: "HistoryStats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryIdUser = table.Column<int>(type: "int", nullable: false),
                    HistoryIdExercise = table.Column<int>(type: "int", nullable: false),
                    HistoryDate = table.Column<DateTime>(type: "Date", nullable: false),
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
                        name: "FK_HistoryStats_History_HistoryIdUser_HistoryIdExercise_HistoryDate",
                        columns: x => new { x.HistoryIdUser, x.HistoryIdExercise, x.HistoryDate },
                        principalTable: "History",
                        principalColumns: new[] { "IdUser", "IdExercise", "Date" },
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
                columns: new[] { "Id", "Czas", "Description", "Dystans", "GoalId", "Name", "Obciazenie", "Powtorzenia", "Serie" },
                values: new object[,]
                {
                    { 1, null, null, null, 1, "Exercise_1", null, 1, 1 },
                    { 2, null, null, null, 2, "Exercise_2", null, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "GoalId", "Name", "PasswordHash", "PasswordSalt", "RoleId", "Surname" },
                values: new object[,]
                {
                    { 1, "admin@gmail.com", 2, "Admin Name", new byte[] { 103, 24, 114, 37, 67, 189, 179, 91, 187, 49, 247, 18, 246, 4, 158, 109, 222, 213, 22, 44, 99, 48, 204, 160, 107, 174, 15, 80, 184, 54, 181, 70, 163, 156, 42, 129, 40, 174, 143, 247, 182, 8, 144, 17, 169, 90, 25, 33, 74, 123, 49, 109, 64, 112, 184, 155, 236, 196, 61, 238, 178, 84, 96, 227 }, new byte[] { 58, 181, 238, 119, 192, 109, 143, 182, 44, 83, 15, 194, 190, 45, 102, 44, 195, 26, 123, 182, 25, 6, 76, 93, 184, 94, 86, 142, 30, 217, 11, 71, 245, 224, 28, 147, 148, 252, 90, 97, 83, 221, 98, 168, 216, 24, 124, 185, 157, 168, 223, 227, 8, 25, 114, 228, 232, 162, 180, 153, 178, 167, 150, 30, 109, 46, 36, 1, 242, 112, 70, 168, 22, 3, 51, 112, 249, 64, 201, 200, 95, 158, 237, 87, 91, 195, 59, 9, 65, 119, 31, 252, 146, 204, 246, 189, 83, 8, 79, 104, 255, 222, 11, 26, 110, 204, 61, 238, 194, 183, 1, 118, 82, 233, 193, 23, 156, 116, 77, 48, 196, 139, 64, 228, 198, 8, 233, 82 }, 1, "Admin Surname" },
                    { 2, "moderator@gmail.com", 3, "Moderator Name", new byte[] { 103, 24, 114, 37, 67, 189, 179, 91, 187, 49, 247, 18, 246, 4, 158, 109, 222, 213, 22, 44, 99, 48, 204, 160, 107, 174, 15, 80, 184, 54, 181, 70, 163, 156, 42, 129, 40, 174, 143, 247, 182, 8, 144, 17, 169, 90, 25, 33, 74, 123, 49, 109, 64, 112, 184, 155, 236, 196, 61, 238, 178, 84, 96, 227 }, new byte[] { 58, 181, 238, 119, 192, 109, 143, 182, 44, 83, 15, 194, 190, 45, 102, 44, 195, 26, 123, 182, 25, 6, 76, 93, 184, 94, 86, 142, 30, 217, 11, 71, 245, 224, 28, 147, 148, 252, 90, 97, 83, 221, 98, 168, 216, 24, 124, 185, 157, 168, 223, 227, 8, 25, 114, 228, 232, 162, 180, 153, 178, 167, 150, 30, 109, 46, 36, 1, 242, 112, 70, 168, 22, 3, 51, 112, 249, 64, 201, 200, 95, 158, 237, 87, 91, 195, 59, 9, 65, 119, 31, 252, 146, 204, 246, 189, 83, 8, 79, 104, 255, 222, 11, 26, 110, 204, 61, 238, 194, 183, 1, 118, 82, 233, 193, 23, 156, 116, 77, 48, 196, 139, 64, 228, 198, 8, 233, 82 }, 2, "Moderator Surname" },
                    { 3, "user@gmail.com", 1, "User Name", new byte[] { 103, 24, 114, 37, 67, 189, 179, 91, 187, 49, 247, 18, 246, 4, 158, 109, 222, 213, 22, 44, 99, 48, 204, 160, 107, 174, 15, 80, 184, 54, 181, 70, 163, 156, 42, 129, 40, 174, 143, 247, 182, 8, 144, 17, 169, 90, 25, 33, 74, 123, 49, 109, 64, 112, 184, 155, 236, 196, 61, 238, 178, 84, 96, 227 }, new byte[] { 58, 181, 238, 119, 192, 109, 143, 182, 44, 83, 15, 194, 190, 45, 102, 44, 195, 26, 123, 182, 25, 6, 76, 93, 184, 94, 86, 142, 30, 217, 11, 71, 245, 224, 28, 147, 148, 252, 90, 97, 83, 221, 98, 168, 216, 24, 124, 185, 157, 168, 223, 227, 8, 25, 114, 228, 232, 162, 180, 153, 178, 167, 150, 30, 109, 46, 36, 1, 242, 112, 70, 168, 22, 3, 51, 112, 249, 64, 201, 200, 95, 158, 237, 87, 91, 195, 59, 9, 65, 119, 31, 252, 146, 204, 246, 189, 83, 8, 79, 104, 255, 222, 11, 26, 110, 204, 61, 238, 194, 183, 1, 118, 82, 233, 193, 23, 156, 116, 77, 48, 196, 139, 64, 228, 198, 8, 233, 82 }, 3, "User Surname" }
                });

            migrationBuilder.InsertData(
                table: "History",
                columns: new[] { "Date", "IdExercise", "IdUser" },
                values: new object[,]
                {
                    { new DateTime(2020, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { new DateTime(2020, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "TrainingExercise",
                columns: new[] { "IdExercise", "IdTraining" },
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
                columns: new[] { "IdTraining", "IdUser", "Favourite" },
                values: new object[,]
                {
                    { 1, 1, false },
                    { 2, 1, false },
                    { 3, 1, true }
                });

            migrationBuilder.InsertData(
                table: "HistoryStats",
                columns: new[] { "Id", "Czas", "Dystans", "HistoryDate", "HistoryIdExercise", "HistoryIdUser", "Obciazenie", "Powtorzenia", "Serie" },
                values: new object[] { 1, null, null, new DateTime(2020, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, null, 1, 1 });

            migrationBuilder.InsertData(
                table: "HistoryStats",
                columns: new[] { "Id", "Czas", "Dystans", "HistoryDate", "HistoryIdExercise", "HistoryIdUser", "Obciazenie", "Powtorzenia", "Serie" },
                values: new object[] { 2, null, null, new DateTime(2020, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, null, 2, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Coach_GoalId",
                table: "Coach",
                column: "GoalId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_GoalId",
                table: "Exercise",
                column: "GoalId");

            migrationBuilder.CreateIndex(
                name: "IX_History_IdExercise",
                table: "History",
                column: "IdExercise");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryStats_HistoryIdUser_HistoryIdExercise_HistoryDate",
                table: "HistoryStats",
                columns: new[] { "HistoryIdUser", "HistoryIdExercise", "HistoryDate" });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingExercise_IdTraining",
                table: "TrainingExercise",
                column: "IdTraining");

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
