using Microsoft.EntityFrameworkCore.Migrations;

namespace DbModelsCore.Migrations
{
    public partial class addColoumnColorCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ColorCode",
                schema: "dbo",
                table: "SliderImages",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorCode",
                schema: "dbo",
                table: "SliderImages");
        }
    }
}
