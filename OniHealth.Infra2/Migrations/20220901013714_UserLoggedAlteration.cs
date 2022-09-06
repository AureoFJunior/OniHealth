using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OniHealth.Infra.Migrations
{
    public partial class UserLoggedAlteration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "IsLogged",
                table: "User",
                type: "smallint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLogged",
                table: "User");
        }
    }
}
