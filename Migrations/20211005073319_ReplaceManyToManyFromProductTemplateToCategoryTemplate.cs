using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineStore.Migrations
{
    public partial class ReplaceManyToManyFromProductTemplateToCategoryTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductTemplate");

            migrationBuilder.CreateTable(
                name: "CategoryTemplate",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    TemplatesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTemplate", x => new { x.CategoriesId, x.TemplatesId });
                    table.ForeignKey(
                        name: "FK_CategoryTemplate_Category_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryTemplate_Template_TemplatesId",
                        column: x => x.TemplatesId,
                        principalTable: "Template",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTemplate_TemplatesId",
                table: "CategoryTemplate",
                column: "TemplatesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryTemplate");

            migrationBuilder.CreateTable(
                name: "ProductTemplate",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    TemplatesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTemplate", x => new { x.ProductsId, x.TemplatesId });
                    table.ForeignKey(
                        name: "FK_ProductTemplate_Product_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTemplate_Template_TemplatesId",
                        column: x => x.TemplatesId,
                        principalTable: "Template",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductTemplate_TemplatesId",
                table: "ProductTemplate",
                column: "TemplatesId");
        }
    }
}
