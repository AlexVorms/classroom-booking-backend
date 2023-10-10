using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace classroom_booking_backend.Migrations
{
    /// <inheritdoc />
    public partial class CreateAddFieldsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddFieldsForBookingEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookingId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookingEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddFieldsForBookingEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddFieldsForBookingEntity_Bookings_BookingEntityId",
                        column: x => x.BookingEntityId,
                        principalTable: "Bookings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddFieldsForBookingEntity_BookingEntityId",
                table: "AddFieldsForBookingEntity",
                column: "BookingEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddFieldsForBookingEntity");
        }
    }
}
