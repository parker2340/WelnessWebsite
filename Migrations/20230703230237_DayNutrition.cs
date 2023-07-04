using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WelnessWebsite.Migrations
{
    public partial class DayNutrition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkoutType",
                table: "DailyWorkout");

            migrationBuilder.RenameColumn(
                name: "Protein",
                table: "DailyNutrition",
                newName: "protein");

            migrationBuilder.RenameColumn(
                name: "Fat",
                table: "DailyNutrition",
                newName: "fat");

            migrationBuilder.RenameColumn(
                name: "Diber",
                table: "DailyNutrition",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "Carbs",
                table: "DailyNutrition",
                newName: "carbon");

            migrationBuilder.RenameColumn(
                name: "Calories",
                table: "DailyNutrition",
                newName: "caloric");

            migrationBuilder.AddColumn<string>(
                name: "WorkoutType",
                table: "DailyWorkout",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "DailyNutrition",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkoutType",
                table: "DailyWorkout");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "DailyNutrition");

            migrationBuilder.RenameColumn(
                name: "protein",
                table: "DailyNutrition",
                newName: "Protein");

            migrationBuilder.RenameColumn(
                name: "fat",
                table: "DailyNutrition",
                newName: "Fat");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "DailyNutrition",
                newName: "Diber");

            migrationBuilder.RenameColumn(
                name: "carbon",
                table: "DailyNutrition",
                newName: "Carbs");

            migrationBuilder.RenameColumn(
                name: "caloric",
                table: "DailyNutrition",
                newName: "Calories");

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "DailyWorkout",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
