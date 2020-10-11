using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashCards.Data.Migrations
{
    public partial class ChangedLanguageLevelToEnumInFlashcards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "LanguageLevel",
                table: "Flashcards",
                type: "TINYINT",
                nullable: false,
                defaultValue: (byte)7,
                oldClrType: typeof(string),
                oldType: "NVARCHAR",
                oldMaxLength: 4,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LanguageLevel",
                table: "Flashcards",
                type: "NVARCHAR",
                maxLength: 4,
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "TINYINT",
                oldDefaultValue: (byte)7);
        }
    }
}
