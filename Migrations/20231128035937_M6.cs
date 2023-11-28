using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IllinoisProject.Migrations
{
    /// <inheritdoc />
    public partial class M6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Authors",
                newName: "BlogPostAccountId");

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "Authors",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsEditor",
                table: "Authors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Authors_AccountId",
                table: "Authors",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_BlogPostId",
                table: "Authors",
                column: "BlogPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_AspNetUsers_AccountId",
                table: "Authors",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_BlogPosts_BlogPostId",
                table: "Authors",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "BlogPostId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_AspNetUsers_AccountId",
                table: "Authors");

            migrationBuilder.DropForeignKey(
                name: "FK_Authors_BlogPosts_BlogPostId",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Authors_AccountId",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Authors_BlogPostId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "IsEditor",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "BlogPostAccountId",
                table: "Authors",
                newName: "AuthorId");

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "Authors",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
