using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fintorly.Infrastructure.Migrations
{
    public partial class InitalCreate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Iban",
                table: "Mentors",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "IsActive", "IsDeleted", "ModifiedBy", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("1ad8b9d2-f1fe-4924-9156-bcae544739bc"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 12, 26, 22, 35, 30, 198, DateTimeKind.Local).AddTicks(7240), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin" },
                    { new Guid("699cc037-1763-4aa0-b8a7-859f88ed4952"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 12, 26, 22, 35, 30, 198, DateTimeKind.Local).AddTicks(7240), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mentor" },
                    { new Guid("9404cb2f-59ad-4420-9a1e-d8ff4cacc2ae"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 12, 26, 22, 35, 30, 198, DateTimeKind.Local).AddTicks(7230), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User" },
                    { new Guid("fb9ce492-0a29-4ba6-8945-7048ee208f9f"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 12, 26, 22, 35, 30, 198, DateTimeKind.Local).AddTicks(7250), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Guest" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("1ad8b9d2-f1fe-4924-9156-bcae544739bc"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("699cc037-1763-4aa0-b8a7-859f88ed4952"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("9404cb2f-59ad-4420-9a1e-d8ff4cacc2ae"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("fb9ce492-0a29-4ba6-8945-7048ee208f9f"));

            migrationBuilder.AlterColumn<string>(
                name: "Iban",
                table: "Mentors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
    }
}
