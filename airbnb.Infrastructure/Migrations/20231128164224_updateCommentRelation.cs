using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace airbnb.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateCommentRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CommentValue",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentValue",
                table: "Comments");
        }
    }
}
