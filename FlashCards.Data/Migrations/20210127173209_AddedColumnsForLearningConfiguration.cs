using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashCards.Data.Migrations
{
    public partial class AddedColumnsForLearningConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "NumberOfWordsInLearningSession",
                table: "UserInfos",
                type: "SMALLINT",
                nullable: false,
                defaultValue: (short)10);

            migrationBuilder.AddColumn<short>(
                name: "NumberOfWordsInReviewSession",
                table: "UserInfos",
                type: "SMALLINT",
                nullable: false,
                defaultValue: (short)10);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfWordsInLearningSession",
                table: "UserInfos");

            migrationBuilder.DropColumn(
                name: "NumberOfWordsInReviewSession",
                table: "UserInfos");
        }
    }
}
