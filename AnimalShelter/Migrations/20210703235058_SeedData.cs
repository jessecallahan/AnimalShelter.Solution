using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimalShelter.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "AnimalId", "Age", "Family", "Markings", "Name", "Species" },
                values: new object[,]
                {
                    { 1, 6, "unknown", "Spotted/Orange/White", "Matilda", "Cat" },
                    { 2, 19, "Husky", "Female", "Maeve", "Dog" },
                    { 3, 7, "unknown", "Black/White/Red", "Matilda", "Cat" },
                    { 4, 10, "Pomeranian", "Spotted", "Pip", "Dog" },
                    { 5, 7, "Lab", "Black", "Chance", "Dog" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "AnimalId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "AnimalId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "AnimalId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "AnimalId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "AnimalId",
                keyValue: 5);
        }
    }
}
