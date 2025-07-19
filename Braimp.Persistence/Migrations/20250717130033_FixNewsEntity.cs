using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Braimp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixNewsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "CourseNews");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageResourceId",
                table: "CourseNews",
                type: "uniqueidentifier",
                maxLength: 2048,
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageResourceId",
                table: "CourseNews");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "CourseNews",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: true);
        }
    }
}
