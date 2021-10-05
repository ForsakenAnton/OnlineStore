using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineStore.Migrations
{
    public partial class RewriteRelationManyToOneOfTemplatesAndCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "TemplateId",
                table: "Category",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_TemplateId",
                table: "Category",
                column: "TemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Template_TemplateId",
                table: "Category",
                column: "TemplateId",
                principalTable: "Template",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Template_TemplateId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_TemplateId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "TemplateId",
                table: "Category");

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
    }
}
