using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class EstablishmentIdAddedToAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KzName",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "RuName",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "EstablishmentId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstablishmentId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "KzName",
                table: "Products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RuName",
                table: "Products",
                type: "text",
                nullable: true);
        }
    }
}
