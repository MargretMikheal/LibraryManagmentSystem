using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagmentSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"INSERT INTO [dbo].[AspNetRoles] VALUES ('{Guid.NewGuid()}', 'User', 'USER', '{Guid.NewGuid()}')");
            migrationBuilder.Sql($"INSERT INTO [dbo].[AspNetRoles] VALUES ('{Guid.NewGuid()}', 'Admin', 'ADMIN', '{Guid.NewGuid()}')");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($"DELETE FROM [dbo].[AspNetRoles]");
        }
    }
}
