using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerTCP.Migrations
{
    public partial class fixMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AttachmentFileName",
                table: "UserMessages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasAttachment",
                table: "UserMessages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttachmentFileName",
                table: "UserMessages");

            migrationBuilder.DropColumn(
                name: "HasAttachment",
                table: "UserMessages");
        }
    }
}
