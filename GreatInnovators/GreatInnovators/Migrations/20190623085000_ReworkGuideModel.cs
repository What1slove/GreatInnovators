using Microsoft.EntityFrameworkCore.Migrations;

namespace GreatInnovators.Migrations
{
    public partial class ReworkGuideModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuideCities_Hospitals_GuideId",
                table: "GuideCities");

            migrationBuilder.DropForeignKey(
                name: "FK_GuideLanguages_Hospitals_GuideId",
                table: "GuideLanguages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hospitals",
                table: "Hospitals");

            migrationBuilder.RenameTable(
                name: "Hospitals",
                newName: "Guides");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Guides",
                table: "Guides",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GuideCities_Guides_GuideId",
                table: "GuideCities",
                column: "GuideId",
                principalTable: "Guides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GuideLanguages_Guides_GuideId",
                table: "GuideLanguages",
                column: "GuideId",
                principalTable: "Guides",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GuideCities_Guides_GuideId",
                table: "GuideCities");

            migrationBuilder.DropForeignKey(
                name: "FK_GuideLanguages_Guides_GuideId",
                table: "GuideLanguages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Guides",
                table: "Guides");

            migrationBuilder.RenameTable(
                name: "Guides",
                newName: "Hospitals");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hospitals",
                table: "Hospitals",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GuideCities_Hospitals_GuideId",
                table: "GuideCities",
                column: "GuideId",
                principalTable: "Hospitals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GuideLanguages_Hospitals_GuideId",
                table: "GuideLanguages",
                column: "GuideId",
                principalTable: "Hospitals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
