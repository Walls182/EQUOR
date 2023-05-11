using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EQUOR.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
     name: "Managers",
     columns: table => new
     {
         IdManager = table.Column<int>(type: "int", nullable: false)
             .Annotation("SqlServer:Identity", "1, 1"),
         Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
         Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
         Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
         IdCompany = table.Column<int>(type: "int", nullable: false),
         IdRole = table.Column<int>(type: "int", nullable: false)
     },
     constraints: table =>
     {
         table.PrimaryKey("PK_Managers", x => x.IdManager);
         table.ForeignKey(
             name: "FK_Managers_Companies_IdCompany",
             column: x => x.IdCompany,
             principalTable: "Companies",
             principalColumn: "IdCompany",
             onDelete: ReferentialAction.Cascade);
         table.ForeignKey(
             name: "FK_Managers_Roles_IdRole",
             column: x => x.IdRole,
             principalTable: "Roles",
             principalColumn: "IdRole",
             onDelete: ReferentialAction.NoAction); // se cambia a NoAction
     });


            migrationBuilder.CreateTable(
                name: "ProductSearches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    SearchDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSearches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSearches_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "IdProduct",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Managers_IdCompany",
                table: "Managers",
                column: "IdCompany");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_IdRole",
                table: "Managers",
                column: "IdRole");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSearches_ProductId",
                table: "ProductSearches",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "ProductSearches");
        }
    }
}
