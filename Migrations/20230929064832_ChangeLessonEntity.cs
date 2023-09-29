using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace classroom_booking_backend.Migrations
{
    /// <inheritdoc />
    public partial class ChangeLessonEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "Teacher",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Lessons",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LessonType",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfessorId",
                table: "Lessons",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_ProfessorId",
                table: "Lessons",
                column: "ProfessorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Teacher_ProfessorId",
                table: "Lessons",
                column: "ProfessorId",
                principalTable: "Teacher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Teacher_ProfessorId",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_ProfessorId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "LessonType",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "ProfessorId",
                table: "Lessons");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
