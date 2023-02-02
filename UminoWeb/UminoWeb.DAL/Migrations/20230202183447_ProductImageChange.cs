using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UminoWeb.DAL.Migrations
{
    public partial class ProductImageChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ProductImages",
                newName: "ImageName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "ProductImages",
                newName: "Name");
        }
    }
}
