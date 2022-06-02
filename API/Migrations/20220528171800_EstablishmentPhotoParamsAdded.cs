using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class EstablishmentPhotoParamsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoPublicId",
                table: "Establishments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Establishments",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPublicId",
                table: "Establishments");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Establishments");
        }
    }
}
