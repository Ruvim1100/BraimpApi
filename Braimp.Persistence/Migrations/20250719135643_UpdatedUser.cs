using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Braimp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentRequests_UserId",
                table: "EnrollmentRequests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnrollmentRequests_Users_UserId",
                table: "EnrollmentRequests",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnrollmentRequests_Users_UserId",
                table: "EnrollmentRequests");

            migrationBuilder.DropIndex(
                name: "IX_EnrollmentRequests_UserId",
                table: "EnrollmentRequests");
        }
    }
}
