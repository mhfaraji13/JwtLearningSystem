using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JwtLearningSystem.Shared.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordHashIterations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PasswordHashIterations",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHashIterations",
                table: "Users");
        }
    }
}
