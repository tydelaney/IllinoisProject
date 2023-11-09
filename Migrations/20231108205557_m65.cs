using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IllinoisProject.Migrations
{
    /// <inheritdoc />
    public partial class m65 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Accounts_AccountId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_AccountId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Pictures");

            migrationBuilder.AddColumn<int>(
                name: "PictureId",
                table: "Accounts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_PictureId",
                table: "Accounts",
                column: "PictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Pictures_PictureId",
                table: "Accounts",
                column: "PictureId",
                principalTable: "Pictures",
                principalColumn: "PictureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Pictures_PictureId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_PictureId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "PictureId",
                table: "Accounts");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Pictures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_AccountId",
                table: "Pictures",
                column: "AccountId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Accounts_AccountId",
                table: "Pictures",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
