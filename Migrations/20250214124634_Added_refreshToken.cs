using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Authentication_BasicImplementation.Migrations
{
    /// <inheritdoc />
    public partial class Added_refreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "UserLoginDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "UserLoginDetails",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "UserLoginDetails");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "UserLoginDetails");
        }
    }
}
