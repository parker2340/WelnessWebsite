using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WelnessWebsite.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkoutType",
                table: "Workout");

            migrationBuilder.DropColumn(
                name: "caloric",
                table: "DailyNutrition");

            migrationBuilder.DropColumn(
                name: "carbon",
                table: "DailyNutrition");

            migrationBuilder.DropColumn(
                name: "fat",
                table: "DailyNutrition");

            migrationBuilder.DropColumn(
                name: "protein",
                table: "DailyNutrition");

            migrationBuilder.DropColumn(
                name: "type",
                table: "DailyNutrition");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Workout",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "DailyNutrition",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<double>(
                name: "Calories",
                table: "DailyNutrition",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "carbohydrates_total_g",
                table: "DailyNutrition",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "cholesterol_mg",
                table: "DailyNutrition",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "fat_saturated_g",
                table: "DailyNutrition",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "fat_total_g",
                table: "DailyNutrition",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "fiber_g",
                table: "DailyNutrition",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "potassium_mg",
                table: "DailyNutrition",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "protein_g",
                table: "DailyNutrition",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "serving_size_g",
                table: "DailyNutrition",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "sodium_mg",
                table: "DailyNutrition",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "sugar_g",
                table: "DailyNutrition",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Workout");

            migrationBuilder.DropColumn(
                name: "Calories",
                table: "DailyNutrition");

            migrationBuilder.DropColumn(
                name: "carbohydrates_total_g",
                table: "DailyNutrition");

            migrationBuilder.DropColumn(
                name: "cholesterol_mg",
                table: "DailyNutrition");

            migrationBuilder.DropColumn(
                name: "fat_saturated_g",
                table: "DailyNutrition");

            migrationBuilder.DropColumn(
                name: "fat_total_g",
                table: "DailyNutrition");

            migrationBuilder.DropColumn(
                name: "fiber_g",
                table: "DailyNutrition");

            migrationBuilder.DropColumn(
                name: "potassium_mg",
                table: "DailyNutrition");

            migrationBuilder.DropColumn(
                name: "protein_g",
                table: "DailyNutrition");

            migrationBuilder.DropColumn(
                name: "serving_size_g",
                table: "DailyNutrition");

            migrationBuilder.DropColumn(
                name: "sodium_mg",
                table: "DailyNutrition");

            migrationBuilder.DropColumn(
                name: "sugar_g",
                table: "DailyNutrition");

            migrationBuilder.AddColumn<string>(
                name: "WorkoutType",
                table: "Workout",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "DailyNutrition",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "caloric",
                table: "DailyNutrition",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "carbon",
                table: "DailyNutrition",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "fat",
                table: "DailyNutrition",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "protein",
                table: "DailyNutrition",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "type",
                table: "DailyNutrition",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
