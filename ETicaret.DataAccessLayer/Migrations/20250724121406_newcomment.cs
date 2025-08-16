using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaret.DataAccessLayer.Migrations
{
    public partial class newcomment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductID",
                table: "UserComments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "UserComments");
        }
    }
}
