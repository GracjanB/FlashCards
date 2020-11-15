using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashCards.Data.Migrations
{
    public partial class FixSubscribedCourseTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastTrainedDate",
                table: "SubscribedCourses");

            migrationBuilder.AddColumn<bool>(
                name: "IsSubscribed",
                table: "SubscribedCourses",
                type: "BIT",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastActivity",
                table: "SubscribedCourses",
                type: "DATETIME",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSubscribed",
                table: "SubscribedCourses");

            migrationBuilder.DropColumn(
                name: "LastActivity",
                table: "SubscribedCourses");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastTrainedDate",
                table: "SubscribedCourses",
                type: "DATETIME2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
