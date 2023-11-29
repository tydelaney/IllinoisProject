using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IllinoisProject.Migrations
{
    /// <inheritdoc />
    public partial class M8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountBlogPost_BlogPosts_BlogPostId1",
                table: "AccountBlogPost");

            migrationBuilder.DropIndex(
                name: "IX_AccountBlogPost_BlogPostId1",
                table: "AccountBlogPost");

            migrationBuilder.DropColumn(
                name: "BlogPostId1",
                table: "AccountBlogPost");

            migrationBuilder.AlterColumn<int>(
                name: "BlogPostId",
                table: "AccountBlogPost",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "BlogId",
                table: "AccountBlogPost",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AccountBlogPost_BlogPostId",
                table: "AccountBlogPost",
                column: "BlogPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountBlogPost_BlogPosts_BlogPostId",
                table: "AccountBlogPost",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountBlogPost_BlogPosts_BlogPostId",
                table: "AccountBlogPost");

            migrationBuilder.DropIndex(
                name: "IX_AccountBlogPost_BlogPostId",
                table: "AccountBlogPost");

            migrationBuilder.DropColumn(
                name: "BlogId",
                table: "AccountBlogPost");

            migrationBuilder.AlterColumn<string>(
                name: "BlogPostId",
                table: "AccountBlogPost",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BlogPostId1",
                table: "AccountBlogPost",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AccountBlogPost_BlogPostId1",
                table: "AccountBlogPost",
                column: "BlogPostId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountBlogPost_BlogPosts_BlogPostId1",
                table: "AccountBlogPost",
                column: "BlogPostId1",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
