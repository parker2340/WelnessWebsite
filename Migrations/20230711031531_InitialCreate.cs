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
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DailyWorkout",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    WorkoutType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyWorkout", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DailyWorkout_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeeklyNutrition",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Calories = table.Column<double>(type: "float", nullable: false),
                    serving_size_g = table.Column<double>(type: "float", nullable: false),
                    fat_total_g = table.Column<double>(type: "float", nullable: false),
                    fat_saturated_g = table.Column<double>(type: "float", nullable: false),
                    protein_g = table.Column<double>(type: "float", nullable: false),
                    sodium_mg = table.Column<double>(type: "float", nullable: false),
                    potassium_mg = table.Column<double>(type: "float", nullable: false),
                    cholesterol_mg = table.Column<double>(type: "float", nullable: false),
                    carbohydrates_total_g = table.Column<double>(type: "float", nullable: false),
                    fiber_g = table.Column<double>(type: "float", nullable: false),
                    sugar_g = table.Column<double>(type: "float", nullable: false),
                    WeekNumber = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeeklyNutrition", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WeeklyNutrition_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Workout",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DailyWorkoutID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Muscle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Difficulty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workout", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Workout_DailyWorkout_DailyWorkoutID",
                        column: x => x.DailyWorkoutID,
                        principalTable: "DailyWorkout",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyNutrition",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeeklyNutritionID = table.Column<int>(type: "int", nullable: false),
                    Calories = table.Column<double>(type: "float", nullable: false),
                    serving_size_g = table.Column<double>(type: "float", nullable: false),
                    fat_total_g = table.Column<double>(type: "float", nullable: false),
                    fat_saturated_g = table.Column<double>(type: "float", nullable: false),
                    protein_g = table.Column<double>(type: "float", nullable: false),
                    sodium_mg = table.Column<double>(type: "float", nullable: false),
                    potassium_mg = table.Column<double>(type: "float", nullable: false),
                    cholesterol_mg = table.Column<double>(type: "float", nullable: false),
                    carbohydrates_total_g = table.Column<double>(type: "float", nullable: false),
                    fiber_g = table.Column<double>(type: "float", nullable: false),
                    sugar_g = table.Column<double>(type: "float", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyNutrition", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DailyNutrition_WeeklyNutrition_WeeklyNutritionID",
                        column: x => x.WeeklyNutritionID,
                        principalTable: "WeeklyNutrition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Nutrition",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DailyNutritionID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Calories = table.Column<double>(type: "float", nullable: false),
                    serving_size_g = table.Column<double>(type: "float", nullable: false),
                    fat_total_g = table.Column<double>(type: "float", nullable: false),
                    fat_saturated_g = table.Column<double>(type: "float", nullable: false),
                    protein_g = table.Column<double>(type: "float", nullable: false),
                    sodium_mg = table.Column<double>(type: "float", nullable: false),
                    potassium_mg = table.Column<double>(type: "float", nullable: false),
                    cholesterol_mg = table.Column<double>(type: "float", nullable: false),
                    carbohydrates_total_g = table.Column<double>(type: "float", nullable: false),
                    fiber_g = table.Column<double>(type: "float", nullable: false),
                    sugar_g = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nutrition", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Nutrition_DailyNutrition_DailyNutritionID",
                        column: x => x.DailyNutritionID,
                        principalTable: "DailyNutrition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyNutrition_WeeklyNutritionID",
                table: "DailyNutrition",
                column: "WeeklyNutritionID");

            migrationBuilder.CreateIndex(
                name: "IX_DailyWorkout_UserID",
                table: "DailyWorkout",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Nutrition_DailyNutritionID",
                table: "Nutrition",
                column: "DailyNutritionID");

            migrationBuilder.CreateIndex(
                name: "IX_WeeklyNutrition_UserId",
                table: "WeeklyNutrition",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Workout_DailyWorkoutID",
                table: "Workout",
                column: "DailyWorkoutID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nutrition");

            migrationBuilder.DropTable(
                name: "Workout");

            migrationBuilder.DropTable(
                name: "DailyNutrition");

            migrationBuilder.DropTable(
                name: "DailyWorkout");

            migrationBuilder.DropTable(
                name: "WeeklyNutrition");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
