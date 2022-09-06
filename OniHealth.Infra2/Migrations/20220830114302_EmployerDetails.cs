using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OniHealth.Infra.Migrations
{
    public partial class EmployerDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Employer",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Salary",
                table: "Employer",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Employer",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Employer");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Employer");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Employer");
        }
    }
}
