using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FribergHomeAPI.Migrations
{
    /// <inheritdoc />
    public partial class apiUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApiUserId",
                table: "Agents",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Agents_ApiUserId",
                table: "Agents",
                column: "ApiUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Agents_AspNetUsers_ApiUserId",
                table: "Agents",
                column: "ApiUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Agents_AspNetUsers_ApiUserId",
                table: "Agents");

            migrationBuilder.DropIndex(
                name: "IX_Agents_ApiUserId",
                table: "Agents");

            migrationBuilder.DropColumn(
                name: "ApiUserId",
                table: "Agents");
        }
    }
}
