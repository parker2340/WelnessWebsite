using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WelnessWebsite.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyWorkout",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyWorkout", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WeeklyNutrition",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Calories = table.Column<int>(type: "int", nullable: false),
                    Fat = table.Column<int>(type: "int", nullable: false),
                    Carbs = table.Column<int>(type: "int", nullable: false),
                    Protein = table.Column<int>(type: "int", nullable: false),
                    Fiber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeeklyNutrition", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Workout",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    muscle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dificulty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    instructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DailyWorkoutID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workout", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Workout_DailyWorkout_DailyWorkoutID",
                        column: x => x.DailyWorkoutID,
                        principalTable: "DailyWorkout",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "DailyNutrition",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeeklyID = table.Column<int>(type: "int", nullable: false),
                    Calories = table.Column<int>(type: "int", nullable: false),
                    Fat = table.Column<int>(type: "int", nullable: false),
                    Carbs = table.Column<int>(type: "int", nullable: false),
                    Protein = table.Column<int>(type: "int", nullable: false),
                    Diber = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WeeklyNutritionID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyNutrition", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DailyNutrition_WeeklyNutrition_WeeklyNutritionID",
                        column: x => x.WeeklyNutritionID,
                        principalTable: "WeeklyNutrition",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailConfirmed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeeklyNutritionID = table.Column<int>(type: "int", nullable: true),
                    WorkoutID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                    table.ForeignKey(
                        name: "FK_User_DailyWorkout_WorkoutID",
                        column: x => x.WorkoutID,
                        principalTable: "DailyWorkout",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_User_WeeklyNutrition_WeeklyNutritionID",
                        column: x => x.WeeklyNutritionID,
                        principalTable: "WeeklyNutrition",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyNutrition_WeeklyNutritionID",
                table: "DailyNutrition",
                column: "WeeklyNutritionID");

            migrationBuilder.CreateIndex(
                name: "IX_User_WeeklyNutritionID",
                table: "User",
                column: "WeeklyNutritionID");

            migrationBuilder.CreateIndex(
                name: "IX_User_WorkoutID",
                table: "User",
                column: "WorkoutID");

            migrationBuilder.CreateIndex(
                name: "IX_Workout_DailyWorkoutID",
                table: "Workout",
                column: "DailyWorkoutID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyNutrition");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Workout");

            migrationBuilder.DropTable(
                name: "WeeklyNutrition");

            migrationBuilder.DropTable(
                name: "DailyWorkout");
        }
    }
}
