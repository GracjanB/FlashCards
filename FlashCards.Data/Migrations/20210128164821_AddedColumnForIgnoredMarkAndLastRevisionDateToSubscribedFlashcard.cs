using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashCards.Data.Migrations
{
    public partial class AddedColumnForIgnoredMarkAndLastRevisionDateToSubscribedFlashcard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastRevisionDate",
                table: "SubscribedFlashcards",
                type: "DATETIME2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "MarkedAsIgnored",
                table: "SubscribedFlashcards",
                type: "BIT",
                nullable: false,
                defaultValueSql: "0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastRevisionDate",
                table: "SubscribedFlashcards");

            migrationBuilder.DropColumn(
                name: "MarkedAsIgnored",
                table: "SubscribedFlashcards");
        }
    }
}
