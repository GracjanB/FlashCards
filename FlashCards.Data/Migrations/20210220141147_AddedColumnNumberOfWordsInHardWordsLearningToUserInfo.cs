using Microsoft.EntityFrameworkCore.Migrations;

namespace FlashCards.Data.Migrations
{
    public partial class AddedColumnNumberOfWordsInHardWordsLearningToUserInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "NumberOfWordsInHardWordsLearning",
                table: "UserInfos",
                type: "SMALLINT",
                nullable: false,
                defaultValue: (short)10);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfWordsInHardWordsLearning",
                table: "UserInfos");
        }
    }
}
