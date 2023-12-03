using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IllinoisProject.Migrations
{
    /// <inheritdoc />
    public partial class M7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSelected",
                table: "AccountBlogPosts");

            migrationBuilder.AddColumn<string>(
                name: "PermissionType",
                table: "AccountBlogPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PermissionType",
                table: "AccountBlogPosts");

            migrationBuilder.AddColumn<bool>(
                name: "IsSelected",
                table: "AccountBlogPosts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
