using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightDocs_System.Migrations
{
    public partial class updateColumn1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_FlightDocument_FlightDocumentId",
                table: "Document");

            migrationBuilder.DropIndex(
                name: "IX_Document_FlightDocumentId",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "FlightDocumentId",
                table: "Document");

            migrationBuilder.AddColumn<string>(
                name: "DocumentId",
                table: "FlightDocument",
                type: "nvarchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Document",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Document",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_FlightDocument_DocumentId",
                table: "FlightDocument",
                column: "DocumentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FlightDocument_Document_DocumentId",
                table: "FlightDocument",
                column: "DocumentId",
                principalTable: "Document",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FlightDocument_Document_DocumentId",
                table: "FlightDocument");

            migrationBuilder.DropIndex(
                name: "IX_FlightDocument_DocumentId",
                table: "FlightDocument");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "FlightDocument");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Document");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Document");

            migrationBuilder.AddColumn<string>(
                name: "FlightDocumentId",
                table: "Document",
                type: "nvarchar(255)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Document_FlightDocumentId",
                table: "Document",
                column: "FlightDocumentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Document_FlightDocument_FlightDocumentId",
                table: "Document",
                column: "FlightDocumentId",
                principalTable: "FlightDocument",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
