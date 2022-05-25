using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class AppUserDtoAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IIN",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IIN",
                table: "AspNetUsers");
        }
    }
}
