using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fintorly.Infrastructure.Migrations
{
    public partial class InitalCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserType",
                table: "ProfilePictures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "IsActive", "IsDeleted", "ModifiedBy", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("666a7c65-a19c-4f9f-ac7c-83f637070e4f"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 12, 26, 21, 42, 3, 719, DateTimeKind.Local).AddTicks(9190), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Guest" },
                    { new Guid("95f9208c-9868-4bcf-9a48-2b83f481e2de"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 12, 26, 21, 42, 3, 719, DateTimeKind.Local).AddTicks(9160), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User" },
                    { new Guid("9ad909ed-2d4b-4f7d-9c2a-4836354f8a6c"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 12, 26, 21, 42, 3, 719, DateTimeKind.Local).AddTicks(9170), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mentor" },
                    { new Guid("eda4d3dd-7e71-4d81-a574-3a5d40d2e79c"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 12, 26, 21, 42, 3, 719, DateTimeKind.Local).AddTicks(9180), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("666a7c65-a19c-4f9f-ac7c-83f637070e4f"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("95f9208c-9868-4bcf-9a48-2b83f481e2de"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("9ad909ed-2d4b-4f7d-9c2a-4836354f8a6c"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("eda4d3dd-7e71-4d81-a574-3a5d40d2e79c"));

            migrationBuilder.DropColumn(
                name: "UserType",
                table: "ProfilePictures");
        }
    }
}
