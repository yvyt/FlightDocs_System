using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightDocs_System.Migrations
{
    public partial class seedPermission1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "PermissionName" },
                values: new object[,]
                {
                    { "0e9097ed-1ed9-41c4-a835-514b471871cd", "Create:Permission" },
                    { "18c9bb4e-ba23-427e-9feb-154088e836ed", "View:FlightDocument" },
                    { "1e06f8cb-bd83-4a69-a038-3876cb71d8df", "Delete:Flight" },
                    { "2565eccf-2e1e-4f86-86e7-e91187a9f86b", "Edit:FlightDocument" },
                    { "3be48954-141b-42f5-9ff2-b7d3208ffee6", "Create:TypeDocument" },
                    { "3e11b374-a105-420d-89f6-fea33577b5f8", "Edit:Permission" },
                    { "4c3350fe-1264-4be0-979c-c02da767ac3b", "Edit:Flight" },
                    { "527f45dc-7be7-42f9-8b57-9e596c2e9cd3", "View:User" },
                    { "698f9d64-d8e9-4956-81ad-73dbad03a33b", "Delete:FlightDocument" },
                    { "6c62af64-4ddb-4696-9451-2b85d33d583b", "Edit:User" },
                    { "7a572c8e-d98d-4d55-9788-72c63b6daecc", "View:Role" },
                    { "8b7d72a9-371a-49a4-be16-dc0116a87415", "Create:Flight" },
                    { "9e3d96cb-ce4c-4f20-953f-1f896edc3939", "Create:FlightDocument" },
                    { "a2946505-3fd8-45c0-a9e0-82151ca9c90e", "Edit:Role" },
                    { "a2ae0ef0-9cbd-40e2-8bab-03e3fb068521", "Delete:TypeDocument" },
                    { "a6e8c0e4-cf43-4ee8-8533-b4707886ed43", "Delete:Role" },
                    { "aaec4775-ad81-4ef6-9949-179a76621db5", "Delete:Permission" },
                    { "b2e81850-4829-4aae-b35d-6e42be05cd61", "Create:Role" },
                    { "d494e6b1-9cc5-4923-95bc-d765014d8ae1", "Edit:TypeDocument" },
                    { "ef5e8f34-c17b-4543-b6c7-c6f59ce30338", "Delete:User" },
                    { "f408bba9-7a8e-4ff2-843d-e144baa7ae5b", "View:TypeDocument" },
                    { "f47eb069-c0a1-45b4-82b1-ed5202054a45", "Create:User" },
                    { "f7e04e11-a377-414c-8e59-0c7f60049178", "View:Flight" },
                    { "ff671f50-86f1-4032-9b09-5536c60d66b6", "View:Permission" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "0e9097ed-1ed9-41c4-a835-514b471871cd");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "18c9bb4e-ba23-427e-9feb-154088e836ed");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "1e06f8cb-bd83-4a69-a038-3876cb71d8df");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "2565eccf-2e1e-4f86-86e7-e91187a9f86b");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "3be48954-141b-42f5-9ff2-b7d3208ffee6");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "3e11b374-a105-420d-89f6-fea33577b5f8");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "4c3350fe-1264-4be0-979c-c02da767ac3b");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "527f45dc-7be7-42f9-8b57-9e596c2e9cd3");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "698f9d64-d8e9-4956-81ad-73dbad03a33b");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "6c62af64-4ddb-4696-9451-2b85d33d583b");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "7a572c8e-d98d-4d55-9788-72c63b6daecc");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "8b7d72a9-371a-49a4-be16-dc0116a87415");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "9e3d96cb-ce4c-4f20-953f-1f896edc3939");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "a2946505-3fd8-45c0-a9e0-82151ca9c90e");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "a2ae0ef0-9cbd-40e2-8bab-03e3fb068521");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "a6e8c0e4-cf43-4ee8-8533-b4707886ed43");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "aaec4775-ad81-4ef6-9949-179a76621db5");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "b2e81850-4829-4aae-b35d-6e42be05cd61");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "d494e6b1-9cc5-4923-95bc-d765014d8ae1");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "ef5e8f34-c17b-4543-b6c7-c6f59ce30338");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "f408bba9-7a8e-4ff2-843d-e144baa7ae5b");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "f47eb069-c0a1-45b4-82b1-ed5202054a45");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "f7e04e11-a377-414c-8e59-0c7f60049178");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "ff671f50-86f1-4032-9b09-5536c60d66b6");

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
    }
}
