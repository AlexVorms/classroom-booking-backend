using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace classroom_booking_backend.Migrations
{
    /// <inheritdoc />
    public partial class AddLessons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SheduleEntityDate",
                table: "Lessons",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Lessons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Shedules",
                columns: table => new
                {
                    Date = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shedules", x => x.Date);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_SheduleEntityDate",
                table: "Lessons",
                column: "SheduleEntityDate");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Shedules_SheduleEntityDate",
                table: "Lessons",
                column: "SheduleEntityDate",
                principalTable: "Shedules",
                principalColumn: "Date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Shedules_SheduleEntityDate",
                table: "Lessons");

            migrationBuilder.DropTable(
                name: "Shedules");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_SheduleEntityDate",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "SheduleEntityDate",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Lessons");
        }
    }
}
