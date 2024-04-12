using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastChat.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_creation_date_to_user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Chats",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 11, 15, 49, 5, 751, DateTimeKind.Utc).AddTicks(9921),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 10, 20, 21, 45, 118, DateTimeKind.Utc).AddTicks(639));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ChatMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 11, 15, 49, 5, 751, DateTimeKind.Utc).AddTicks(7808),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 10, 20, 21, 45, 117, DateTimeKind.Utc).AddTicks(9202));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 11, 15, 49, 5, 750, DateTimeKind.Utc).AddTicks(8085));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Chats",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 10, 20, 21, 45, 118, DateTimeKind.Utc).AddTicks(639),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 11, 15, 49, 5, 751, DateTimeKind.Utc).AddTicks(9921));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "ChatMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2024, 4, 10, 20, 21, 45, 117, DateTimeKind.Utc).AddTicks(9202),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2024, 4, 11, 15, 49, 5, 751, DateTimeKind.Utc).AddTicks(7808));
        }
    }
}
