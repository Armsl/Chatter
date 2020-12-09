using Microsoft.EntityFrameworkCore.Migrations;

namespace Core3.Data.Migrations
{
    public partial class MyChatterMigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "When",
                table: "Messages",
                newName: "CurrentTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrentTime",
                table: "Messages",
                newName: "When");
        }
    }
}
