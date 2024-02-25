using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightDocs_System.Migrations
{
    public partial class seedPermission2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "119638e0-2642-4d6f-a59c-1070dc11cbae", "Edit:Role" },
                    { "173c5d7c-e4fb-404b-9b18-fd978c864c28", "Create:Flight" },
                    { "20d95e83-0ba4-419e-8068-a8ead9ed7dfe", "View:Permission" },
                    { "3d970618-a0fb-4b8e-8d78-6c32a4dfb0a1", "Delete:User" },
                    { "51a54f5c-0385-4d21-a094-bdc29e1f0d90", "View:TypeDocument" },
                    { "5b276b3a-6366-48f2-a832-026019a209a2", "Edit:FlightDocument" },
                    { "6c03fbdc-dfac-4133-bed3-21f8398d0dd7", "View:Flight" },
                    { "6cdc6e04-a524-44e1-8ece-7ffb440f5c1b", "Delete:Flight" },
                    { "9ab85c60-326e-469e-b773-b5842a66467d", "Create:User" },
                    { "9cb0fde9-8260-4c88-a995-d88b176a9ace", "Delete:TypeDocument" },
                    { "9f7163d3-5de4-4693-8d78-97f13bbf3e2d", "Edit:TypeDocument" },
                    { "a77b327d-80d7-475c-9600-3c43e03be34d", "Create:Role" },
                    { "c761c6eb-241a-440a-a14d-014685bc8bb8", "View:FlightDocument" },
                    { "c9c3d29c-9314-4d86-a3ac-a7f927d84ca7", "Edit:Flight" },
                    { "d706ed1c-49d0-4888-bb33-09664a250146", "Create:FlightDocument" },
                    { "d8e7c6b4-71f8-4172-8ac6-7ee492337388", "Create:TypeDocument" },
                    { "df730c7e-d95d-4c9c-9faa-e8e9d4bc255a", "Delete:FlightDocument" },
                    { "e0b5c87c-5a63-4ad7-b9aa-a852dee01069", "View:User" },
                    { "fe3fe573-03e9-45b7-a899-cad14610a980", "Edit:User" },
                    { "fe9c4a6c-d5cd-413d-8d58-02301c0d9eab", "View:Role" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "119638e0-2642-4d6f-a59c-1070dc11cbae");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "173c5d7c-e4fb-404b-9b18-fd978c864c28");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "20d95e83-0ba4-419e-8068-a8ead9ed7dfe");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "3d970618-a0fb-4b8e-8d78-6c32a4dfb0a1");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "51a54f5c-0385-4d21-a094-bdc29e1f0d90");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "5b276b3a-6366-48f2-a832-026019a209a2");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "6c03fbdc-dfac-4133-bed3-21f8398d0dd7");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "6cdc6e04-a524-44e1-8ece-7ffb440f5c1b");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "9ab85c60-326e-469e-b773-b5842a66467d");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "9cb0fde9-8260-4c88-a995-d88b176a9ace");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "9f7163d3-5de4-4693-8d78-97f13bbf3e2d");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "a77b327d-80d7-475c-9600-3c43e03be34d");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "c761c6eb-241a-440a-a14d-014685bc8bb8");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "c9c3d29c-9314-4d86-a3ac-a7f927d84ca7");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "d706ed1c-49d0-4888-bb33-09664a250146");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "d8e7c6b4-71f8-4172-8ac6-7ee492337388");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "df730c7e-d95d-4c9c-9faa-e8e9d4bc255a");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "e0b5c87c-5a63-4ad7-b9aa-a852dee01069");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "fe3fe573-03e9-45b7-a899-cad14610a980");

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "Id",
                keyValue: "fe9c4a6c-d5cd-413d-8d58-02301c0d9eab");

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
    }
}
