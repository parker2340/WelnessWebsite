using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WelnessWebsite.Migrations
{
    public partial class newer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workout_DailyWorkout_DailyWorkoutID",
                table: "Workout");

            migrationBuilder.AlterColumn<int>(
                name: "DailyWorkoutID",
                table: "Workout",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "DailyWorkout",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Workout_DailyWorkout_DailyWorkoutID",
                table: "Workout",
                column: "DailyWorkoutID",
                principalTable: "DailyWorkout",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workout_DailyWorkout_DailyWorkoutID",
                table: "Workout");

            migrationBuilder.DropColumn(
                name: "type",
                table: "DailyWorkout");

            migrationBuilder.AlterColumn<int>(
                name: "DailyWorkoutID",
                table: "Workout",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Workout_DailyWorkout_DailyWorkoutID",
                table: "Workout",
                column: "DailyWorkoutID",
                principalTable: "DailyWorkout",
                principalColumn: "ID");
        }
    }
}
