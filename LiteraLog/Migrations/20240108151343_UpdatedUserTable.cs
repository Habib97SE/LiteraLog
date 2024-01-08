using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiteraLog.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "publishedYear",
                table: "Books",
                newName: "PublishedYear");

            migrationBuilder.RenameColumn(
                name: "comment",
                table: "Books",
                newName: "Comment");

            migrationBuilder.AlterColumn<float>(
                name: "Rating",
                table: "Books",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublishedYear",
                table: "Books",
                newName: "publishedYear");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "Books",
                newName: "comment");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Books",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
