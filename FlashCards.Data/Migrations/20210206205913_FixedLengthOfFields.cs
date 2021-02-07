using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashCards.Data.Migrations
{
    public partial class FixedLengthOfFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                fixedLength: true,
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(128)");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "UserInfos",
                fixedLength: true,
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(64)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "UserInfos",
                fixedLength: true,
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(64)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "UserInfos",
                fixedLength: true,
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(32)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "UserInfos",
                fixedLength: true,
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(64)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "UserInfos",
                fixedLength: true,
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(64)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Lessons",
                fixedLength: true,
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(64)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Lessons",
                fixedLength: true,
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NTEXT",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Lessons",
                fixedLength: true,
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "TranslatedPhraseSampleSentence",
                table: "Flashcards",
                fixedLength: true,
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TranslatedPhraseComment",
                table: "Flashcards",
                fixedLength: true,
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR(128)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TranslatedPhrase",
                table: "Flashcards",
                fixedLength: true,
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "PhraseSampleSentence",
                table: "Flashcards",
                fixedLength: true,
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhrasePronunciation",
                table: "Flashcards",
                fixedLength: true,
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhraseComment",
                table: "Flashcards",
                fixedLength: true,
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR",
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phrase",
                table: "Flashcards",
                fixedLength: true,
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR",
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Flashcards",
                fixedLength: true,
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Courses",
                fixedLength: true,
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "NVARCHAR",
                oldMaxLength: 128);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "NVARCHAR(128)",
                nullable: false,
                oldClrType: typeof(string),
                oldFixedLength: true,
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "UserInfos",
                type: "NVARCHAR(64)",
                nullable: true,
                oldClrType: typeof(string),
                oldFixedLength: true,
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "UserInfos",
                type: "NVARCHAR(64)",
                nullable: true,
                oldClrType: typeof(string),
                oldFixedLength: true,
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "UserInfos",
                type: "NVARCHAR(32)",
                nullable: true,
                oldClrType: typeof(string),
                oldFixedLength: true,
                oldMaxLength: 32,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                table: "UserInfos",
                type: "NVARCHAR(64)",
                nullable: true,
                oldClrType: typeof(string),
                oldFixedLength: true,
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "UserInfos",
                type: "NVARCHAR(64)",
                nullable: true,
                oldClrType: typeof(string),
                oldFixedLength: true,
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Lessons",
                type: "NVARCHAR(64)",
                nullable: false,
                oldClrType: typeof(string),
                oldFixedLength: true,
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Lessons",
                type: "NTEXT",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldFixedLength: true,
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Lessons",
                type: "NVARCHAR",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldFixedLength: true,
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "TranslatedPhraseSampleSentence",
                table: "Flashcards",
                type: "NVARCHAR",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldFixedLength: true,
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TranslatedPhraseComment",
                table: "Flashcards",
                type: "NVARCHAR(128)",
                nullable: true,
                oldClrType: typeof(string),
                oldFixedLength: true,
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TranslatedPhrase",
                table: "Flashcards",
                type: "NVARCHAR",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldFixedLength: true,
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "PhraseSampleSentence",
                table: "Flashcards",
                type: "NVARCHAR",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldFixedLength: true,
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhrasePronunciation",
                table: "Flashcards",
                type: "NVARCHAR",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldFixedLength: true,
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhraseComment",
                table: "Flashcards",
                type: "NVARCHAR",
                maxLength: 128,
                nullable: true,
                oldClrType: typeof(string),
                oldFixedLength: true,
                oldMaxLength: 128,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phrase",
                table: "Flashcards",
                type: "NVARCHAR",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldFixedLength: true,
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Flashcards",
                type: "NVARCHAR",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldFixedLength: true,
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Courses",
                type: "NVARCHAR",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldFixedLength: true,
                oldMaxLength: 64);
        }
    }
}
