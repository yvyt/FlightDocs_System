﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlightDocs_System.Migrations
{
    public partial class addPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PermissionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "rolePermissions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PermissionId = table.Column<string>(type: "nvarchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rolePermissions_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rolePermissions_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Id", "PermissionName" },
                values: new object[,]
                {
                    { "0394611d-4d10-4bf2-a374-d937b2bec3bb", "Delete:Role" },
                    { "03deadca-18f2-4c75-a49d-6d46d49ed291", "Create:Role" },
                    { "0df82cb6-32c6-4fdc-9e23-47217b5506cd", "Delete:Flight" },
                    { "1bd55c64-42df-4bb2-8d77-9aeefc1ba242", "Delete:User" },
                    { "1e0cc53b-cb4e-4872-b06e-eac7fc90410b", "Edit:Role" },
                    { "2dd2a111-5ace-402d-a8f9-a17afc3dbff5", "View:TypeDocument" },
                    { "31dc3a3c-0e10-4324-af04-4bd786835cbf", "Edit:User" },
                    { "38548ba9-4e64-4835-8e8c-6cc463a560cd", "View:Permission" },
                    { "423b72f1-8dad-433f-b12d-c112271a91b3", "View:Role" },
                    { "489cc48c-da84-441d-9e33-43eeb4df7f79", "Create:User" },
                    { "48cc2d0c-c27a-4715-907f-64300f9d15f8", "Delete:TypeDocument" },
                    { "55a4d9dc-2420-4f35-b456-fc94aa9405de", "View:FlightDocument" },
                    { "66a7cdf6-7049-4559-aeb8-8e3028ad3925", "Edit:TypeDocument" },
                    { "6a3db5b4-7c5a-4804-bd58-36e23732fe5d", "Edit:FlightDocument" },
                    { "8332d612-f8a5-4ddb-919f-c824a4068aa8", "Create:Permission" },
                    { "8790cdbd-30c6-4fb4-a99b-877ffddeb833", "Edit:Permission" },
                    { "931e525d-0b7c-4ffe-b102-0da75332cff4", "Create:Flight" },
                    { "9a1f09fc-643a-4447-9b40-fd8f84c4cd9e", "View:Flight" },
                    { "9ea55360-fff7-4d7f-afab-2bd633fbc24e", "Edit:Flight" },
                    { "b99ecbb6-1bdc-46a3-874d-f8bb82b6c8db", "View:User" },
                    { "bbd66cd9-12cf-4a63-87d0-1deecaa023e2", "Create:TypeDocument" },
                    { "d4a64c23-6e15-4df7-bc1b-52968d5d23df", "Delete:FlightDocument" },
                    { "e46d80fc-9298-4a95-829a-38a9098a6d26", "Create:FlightDocument" },
                    { "f0070f39-72b6-410d-bd27-36a530ac5197", "Delete:Permission" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_rolePermissions_PermissionId",
                table: "rolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_rolePermissions_RoleId",
                table: "rolePermissions",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rolePermissions");

            migrationBuilder.DropTable(
                name: "Permission");
        }
    }
}
