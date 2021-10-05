using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineStore.Migrations
{
    public partial class RewriteRelationOneToOneOfTemplatesAndCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryTemplate");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Template",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Template_CategoryId",
                table: "Template",
                column: "CategoryId",
                unique: true,
                filter: "[CategoryId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Template_Category_CategoryId",
                table: "Template",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Template_Category_CategoryId",
                table: "Template");

            migrationBuilder.DropIndex(
                name: "IX_Template_CategoryId",
                table: "Template");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Template");

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
    }
}
