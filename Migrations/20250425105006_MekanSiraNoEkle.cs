using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GezGorTurkiye1.Migrations
{
    /// <inheritdoc />
    public partial class MekanSiraNoEkle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SiraNo",
                table: "Mekanlar",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SiraNo",
                table: "Mekanlar");
        }
    }
}
