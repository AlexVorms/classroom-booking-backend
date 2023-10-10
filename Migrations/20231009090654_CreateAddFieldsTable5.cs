using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace classroom_booking_backend.Migrations
{
    /// <inheritdoc />
    public partial class CreateAddFieldsTable5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddFieldsForBookingEntity_Bookings_BookingEntityId",
                table: "AddFieldsForBookingEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AddFieldsForBookingEntity",
                table: "AddFieldsForBookingEntity");

            migrationBuilder.RenameTable(
                name: "AddFieldsForBookingEntity",
                newName: "FieldsBooking");

            migrationBuilder.RenameIndex(
                name: "IX_AddFieldsForBookingEntity_BookingEntityId",
                table: "FieldsBooking",
                newName: "IX_FieldsBooking_BookingEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FieldsBooking",
                table: "FieldsBooking",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FieldsBooking_Bookings_BookingEntityId",
                table: "FieldsBooking",
                column: "BookingEntityId",
                principalTable: "Bookings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FieldsBooking_Bookings_BookingEntityId",
                table: "FieldsBooking");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FieldsBooking",
                table: "FieldsBooking");

            migrationBuilder.RenameTable(
                name: "FieldsBooking",
                newName: "AddFieldsForBookingEntity");

            migrationBuilder.RenameIndex(
                name: "IX_FieldsBooking_BookingEntityId",
                table: "AddFieldsForBookingEntity",
                newName: "IX_AddFieldsForBookingEntity_BookingEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AddFieldsForBookingEntity",
                table: "AddFieldsForBookingEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AddFieldsForBookingEntity_Bookings_BookingEntityId",
                table: "AddFieldsForBookingEntity",
                column: "BookingEntityId",
                principalTable: "Bookings",
                principalColumn: "Id");
        }
    }
}
