using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace classroom_booking_backend.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBookingEntity2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FieldsBooking_Bookings_BookingEntityId",
                table: "FieldsBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Shedules_SheduleEntityDate",
                table: "Lessons");

            migrationBuilder.DropTable(
                name: "Shedules");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_SheduleEntityDate",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_FieldsBooking_BookingEntityId",
                table: "FieldsBooking");

            migrationBuilder.DropColumn(
                name: "SheduleEntityDate",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "BookingEntityId",
                table: "FieldsBooking");

            migrationBuilder.AddColumn<string>(
                name: "BookingId",
                table: "FieldsBooking",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "FieldsBooking");

            migrationBuilder.AddColumn<DateTime>(
                name: "SheduleEntityDate",
                table: "Lessons",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BookingEntityId",
                table: "FieldsBooking",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Shedules",
                columns: table => new
                {
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shedules", x => x.Date);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_SheduleEntityDate",
                table: "Lessons",
                column: "SheduleEntityDate");

            migrationBuilder.CreateIndex(
                name: "IX_FieldsBooking_BookingEntityId",
                table: "FieldsBooking",
                column: "BookingEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_FieldsBooking_Bookings_BookingEntityId",
                table: "FieldsBooking",
                column: "BookingEntityId",
                principalTable: "Bookings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Shedules_SheduleEntityDate",
                table: "Lessons",
                column: "SheduleEntityDate",
                principalTable: "Shedules",
                principalColumn: "Date");
        }
    }
}
