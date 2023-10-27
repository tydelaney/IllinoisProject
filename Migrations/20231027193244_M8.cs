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
            migrationBuilder.AddColumn<string>(
                name: "applicationUserId",
                table: "Accounts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_applicationUserId",
                table: "Accounts",
                column: "applicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AspNetUsers_applicationUserId",
                table: "Accounts",
                column: "applicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AspNetUsers_applicationUserId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_applicationUserId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "applicationUserId",
                table: "Accounts");
        }
    }
}
