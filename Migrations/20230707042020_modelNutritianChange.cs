using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WelnessWebsite.Migrations
{
    public partial class modelNutritianChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nutrition_DailyNutrition_DailyNutritionID",
                table: "Nutrition");

            migrationBuilder.AlterColumn<int>(
                name: "DailyNutritionID",
                table: "Nutrition",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Nutrition_DailyNutrition_DailyNutritionID",
                table: "Nutrition",
                column: "DailyNutritionID",
                principalTable: "DailyNutrition",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nutrition_DailyNutrition_DailyNutritionID",
                table: "Nutrition");

            migrationBuilder.AlterColumn<int>(
                name: "DailyNutritionID",
                table: "Nutrition",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Nutrition_DailyNutrition_DailyNutritionID",
                table: "Nutrition",
                column: "DailyNutritionID",
                principalTable: "DailyNutrition",
                principalColumn: "ID");
        }
    }
}
