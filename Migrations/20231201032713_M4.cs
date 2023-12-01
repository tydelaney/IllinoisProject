using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IllinoisProject.Migrations
{
    /// <inheritdoc />
    public partial class M4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_BlogPosts_BlogPostId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "AccId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "BlogId",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "BlogPostId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AccountId",
                table: "Comments",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_AccountId",
                table: "Comments",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_BlogPosts_BlogPostId",
                table: "Comments",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_AccountId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_BlogPosts_BlogPostId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_AccountId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "BlogPostId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "AccId",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BlogId",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_BlogPosts_BlogPostId",
                table: "Comments",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id");
        }
    }
}
