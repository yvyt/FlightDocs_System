using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightDocs_System.Migrations
{
    public partial class seedPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "PermissionName" },
                values: new object[,]
                {
                    { "1460f7e3-ca81-4661-8c92-f6a9ead11a8a", "Create:Permission" },
                    { "1d535b76-554f-48b9-9b38-58fe4f89c800", "View:Permission" },
                    { "29d3478e-2f8d-4513-978a-e6ea5e181b9c", "Delete:FlightDocument" },
                    { "2e4b171c-dca2-4df8-8b05-83edecdbc736", "View:User" },
                    { "31b22bca-e1b9-454f-99d1-b50565768bf1", "Create:TypeDocument" },
                    { "384d81c4-6573-44c2-bbeb-6935c4757e4c", "Create:Flight" },
                    { "4ab49bb5-5cee-4f32-9807-d7ffc6a8e3b6", "Create:Role" },
                    { "55ec326c-d135-49fb-99db-de374f97b537", "Edit:TypeDocument" },
                    { "6995c796-0806-4eac-8e2d-2027ea09a2a5", "Edit:User" },
                    { "6b40c0d2-4cd6-4f9e-81a0-13716725c325", "Edit:Permission" },
                    { "6ec0b05e-4a7f-4a6c-af14-e2b1c5b96816", "Delete:User" },
                    { "816fd96b-bd91-4172-af38-e2fac1729eb5", "Delete:Permission" },
                    { "88c22a13-73bc-4bb0-bdb8-1360bdc5d734", "Delete:Role" },
                    { "899d53ef-3c8f-487d-a050-4493fcfc51be", "View:Role" },
                    { "8ca9e737-0db5-4e8c-88c0-0201ff6a4ae4", "View:FlightDocument" },
                    { "93c85a2d-9371-447e-bc25-f18560f346b9", "View:Flight" },
                    { "94072d3b-0455-4278-b96d-6f32dbfb8981", "Create:User" },
                    { "9b9cf7ec-701e-400b-a577-f043fad6fe83", "View:TypeDocument" },
                    { "d9bc9459-22ce-42d7-926e-740974515dd0", "Delete:TypeDocument" },
                    { "df9c77cc-3b91-4afa-afa2-a975ef38c059", "Edit:FlightDocument" },
                    { "f528225f-ff96-472c-8e84-6366f7bf564c", "Delete:Flight" },
                    { "f723b822-34bb-4fa1-97ee-995cf775dd9d", "Edit:Role" },
                    { "f929b758-0d8a-4395-adfd-c2c4bc65a587", "Edit:Flight" },
                    { "fa74aa7f-3087-4838-bd8d-b2fdb6e68ffe", "Create:FlightDocument" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "1460f7e3-ca81-4661-8c92-f6a9ead11a8a");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "1d535b76-554f-48b9-9b38-58fe4f89c800");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "29d3478e-2f8d-4513-978a-e6ea5e181b9c");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "2e4b171c-dca2-4df8-8b05-83edecdbc736");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "31b22bca-e1b9-454f-99d1-b50565768bf1");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "384d81c4-6573-44c2-bbeb-6935c4757e4c");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "4ab49bb5-5cee-4f32-9807-d7ffc6a8e3b6");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "55ec326c-d135-49fb-99db-de374f97b537");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "6995c796-0806-4eac-8e2d-2027ea09a2a5");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "6b40c0d2-4cd6-4f9e-81a0-13716725c325");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "6ec0b05e-4a7f-4a6c-af14-e2b1c5b96816");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "816fd96b-bd91-4172-af38-e2fac1729eb5");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "88c22a13-73bc-4bb0-bdb8-1360bdc5d734");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "899d53ef-3c8f-487d-a050-4493fcfc51be");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "8ca9e737-0db5-4e8c-88c0-0201ff6a4ae4");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "93c85a2d-9371-447e-bc25-f18560f346b9");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "94072d3b-0455-4278-b96d-6f32dbfb8981");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "9b9cf7ec-701e-400b-a577-f043fad6fe83");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "d9bc9459-22ce-42d7-926e-740974515dd0");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "df9c77cc-3b91-4afa-afa2-a975ef38c059");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "f528225f-ff96-472c-8e84-6366f7bf564c");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "f723b822-34bb-4fa1-97ee-995cf775dd9d");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "f929b758-0d8a-4395-adfd-c2c4bc65a587");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "fa74aa7f-3087-4838-bd8d-b2fdb6e68ffe");
        }
    }
}
