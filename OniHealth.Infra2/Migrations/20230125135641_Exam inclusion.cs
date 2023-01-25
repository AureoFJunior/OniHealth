using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OniHealth.Infra.Migrations
{
    public partial class Examinclusion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExamPreparationId",
                table: "Exam",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExamTimeId",
                table: "Exam",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LaboratoryId",
                table: "Exam",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Exam_ExamPreparationId",
                table: "Exam",
                column: "ExamPreparationId");

            migrationBuilder.CreateIndex(
                name: "IX_Exam_ExamTimeId",
                table: "Exam",
                column: "ExamTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_Exam_LaboratoryId",
                table: "Exam",
                column: "LaboratoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exam_ExamPreparation_ExamPreparationId",
                table: "Exam",
                column: "ExamPreparationId",
                principalTable: "ExamPreparation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exam_ExamTime_ExamTimeId",
                table: "Exam",
                column: "ExamTimeId",
                principalTable: "ExamTime",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exam_Laboratory_LaboratoryId",
                table: "Exam",
                column: "LaboratoryId",
                principalTable: "Laboratory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exam_ExamPreparation_ExamPreparationId",
                table: "Exam");

            migrationBuilder.DropForeignKey(
                name: "FK_Exam_ExamTime_ExamTimeId",
                table: "Exam");

            migrationBuilder.DropForeignKey(
                name: "FK_Exam_Laboratory_LaboratoryId",
                table: "Exam");

            migrationBuilder.DropIndex(
                name: "IX_Exam_ExamPreparationId",
                table: "Exam");

            migrationBuilder.DropIndex(
                name: "IX_Exam_ExamTimeId",
                table: "Exam");

            migrationBuilder.DropIndex(
                name: "IX_Exam_LaboratoryId",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "ExamPreparationId",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "ExamTimeId",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "LaboratoryId",
                table: "Exam");
        }
    }
}
