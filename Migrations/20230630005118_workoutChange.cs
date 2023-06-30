using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WelnessWebsite.Migrations
{
    public partial class workoutChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workout_DailyWorkout_DailyWorkoutID",
                table: "Workout");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Workout");

            migrationBuilder.RenameColumn(
                name: "DailyWorkoutID",
                table: "Workout",
                newName: "DailyWorkoutID");

            migrationBuilder.RenameIndex(
                name: "IX_Workout_DailyWorkoutID",
                table: "Workout",
                newName: "IX_Workout_DailyWorkoutID");

            migrationBuilder.AddForeignKey(
                name: "FK_Workout_DailyWorkout_DailyWorkoutID",
                table: "Workout",
                column: "DailyWorkoutID",
                principalTable: "DailyWorkout",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workout_DailyWorkout_DailyWorkoutID",
                table: "Workout");

            migrationBuilder.RenameColumn(
                name: "DailyWorkoutID",
                table: "Workout",
                newName: "DailyWorkoutID");

            migrationBuilder.RenameIndex(
                name: "IX_Workout_DailyWorkoutID",
                table: "Workout",
                newName: "IX_Workout_DailyWorkoutID");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Workout",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Workout_DailyWorkout_DailyWorkoutID",
                table: "Workout",
                column: "DailyWorkoutID",
                principalTable: "DailyWorkout",
                principalColumn: "ID");
        }
    }
}
