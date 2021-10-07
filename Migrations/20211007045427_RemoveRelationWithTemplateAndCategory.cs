using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineStore.Migrations
{
    public partial class RemoveRelationWithTemplateAndCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
