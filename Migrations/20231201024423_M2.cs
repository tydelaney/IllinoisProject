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
                name: "FK_AccountBlogPost_AspNetUsers_AccountId",
                table: "AccountBlogPost");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountBlogPost_BlogPosts_BlogPostId",
                table: "AccountBlogPost");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountBlogPost",
                table: "AccountBlogPost");

            migrationBuilder.RenameTable(
                name: "AccountBlogPost",
                newName: "AccountBlogPosts");

            migrationBuilder.RenameIndex(
                name: "IX_AccountBlogPost_BlogPostId",
                table: "AccountBlogPosts",
                newName: "IX_AccountBlogPosts_BlogPostId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountBlogPost_AccountId",
                table: "AccountBlogPosts",
                newName: "IX_AccountBlogPosts_AccountId");

            migrationBuilder.AlterColumn<string>(
                name: "BlogPostId",
                table: "AccountBlogPosts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountBlogPosts",
                table: "AccountBlogPosts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountBlogPosts_AspNetUsers_AccountId",
                table: "AccountBlogPosts",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountBlogPosts_BlogPosts_BlogPostId",
                table: "AccountBlogPosts",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountBlogPosts_AspNetUsers_AccountId",
                table: "AccountBlogPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountBlogPosts_BlogPosts_BlogPostId",
                table: "AccountBlogPosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountBlogPosts",
                table: "AccountBlogPosts");

            migrationBuilder.RenameTable(
                name: "AccountBlogPosts",
                newName: "AccountBlogPost");

            migrationBuilder.RenameIndex(
                name: "IX_AccountBlogPosts_BlogPostId",
                table: "AccountBlogPost",
                newName: "IX_AccountBlogPost_BlogPostId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountBlogPosts_AccountId",
                table: "AccountBlogPost",
                newName: "IX_AccountBlogPost_AccountId");

            migrationBuilder.AlterColumn<string>(
                name: "BlogPostId",
                table: "AccountBlogPost",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountBlogPost",
                table: "AccountBlogPost",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountBlogPost_AspNetUsers_AccountId",
                table: "AccountBlogPost",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountBlogPost_BlogPosts_BlogPostId",
                table: "AccountBlogPost",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
