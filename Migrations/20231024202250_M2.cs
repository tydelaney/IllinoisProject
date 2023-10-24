using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IllinoisProject.Migrations
{
    /// <inheritdoc />
    public partial class M2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_Accounts_AccountId",
                table: "BlogPosts");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "BlogPosts",
                newName: "AccountNameAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPosts_AccountId",
                table: "BlogPosts",
                newName: "IX_BlogPosts_AccountNameAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_Accounts_AccountNameAccountId",
                table: "BlogPosts",
                column: "AccountNameAccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_Accounts_AccountNameAccountId",
                table: "BlogPosts");

            migrationBuilder.RenameColumn(
                name: "AccountNameAccountId",
                table: "BlogPosts",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogPosts_AccountNameAccountId",
                table: "BlogPosts",
                newName: "IX_BlogPosts_AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_Accounts_AccountId",
                table: "BlogPosts",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
