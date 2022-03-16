using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class DefineUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_States_Code",
                table: "States",
                column: "Code",
                unique: true,
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_States_Name",
                table: "States",
                column: "Name",
                unique: true,
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Alpha2Code",
                table: "Countries",
                column: "Alpha2Code",
                unique: true,
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Alpha3Code",
                table: "Countries",
                column: "Alpha3Code",
                unique: true,
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Name",
                table: "Countries",
                column: "Name",
                unique: true,
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_NumberCode",
                table: "Countries",
                column: "NumberCode",
                unique: true,
                filter: "IsDeleted = 0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_States_Code",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_States_Name",
                table: "States");

            migrationBuilder.DropIndex(
                name: "IX_Countries_Alpha2Code",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Countries_Alpha3Code",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Countries_Name",
                table: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Countries_NumberCode",
                table: "Countries");
        }
    }
}
