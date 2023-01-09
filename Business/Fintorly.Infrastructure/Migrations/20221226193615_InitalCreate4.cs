using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fintorly.Infrastructure.Migrations
{
    public partial class InitalCreate4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "PaymentChannel",
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
                    { new Guid("3448a641-44ca-41ff-bb63-965d03960cd2"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 12, 26, 22, 36, 15, 398, DateTimeKind.Local).AddTicks(3470), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mentor" },
                    { new Guid("6aa676a1-d06d-47ae-ab38-2aeed3234146"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 12, 26, 22, 36, 15, 398, DateTimeKind.Local).AddTicks(3490), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Guest" },
                    { new Guid("96f40fe8-818b-494b-9215-bb677a440823"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 12, 26, 22, 36, 15, 398, DateTimeKind.Local).AddTicks(3480), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin" },
                    { new Guid("cd70ee9e-af65-403a-b72b-83f644824525"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 12, 26, 22, 36, 15, 398, DateTimeKind.Local).AddTicks(3460), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("3448a641-44ca-41ff-bb63-965d03960cd2"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("6aa676a1-d06d-47ae-ab38-2aeed3234146"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("96f40fe8-818b-494b-9215-bb677a440823"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("cd70ee9e-af65-403a-b72b-83f644824525"));

            migrationBuilder.AlterColumn<string>(
                name: "PaymentChannel",
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
                    { new Guid("1ad8b9d2-f1fe-4924-9156-bcae544739bc"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 12, 26, 22, 35, 30, 198, DateTimeKind.Local).AddTicks(7240), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin" },
                    { new Guid("699cc037-1763-4aa0-b8a7-859f88ed4952"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 12, 26, 22, 35, 30, 198, DateTimeKind.Local).AddTicks(7240), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mentor" },
                    { new Guid("9404cb2f-59ad-4420-9a1e-d8ff4cacc2ae"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 12, 26, 22, 35, 30, 198, DateTimeKind.Local).AddTicks(7230), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User" },
                    { new Guid("fb9ce492-0a29-4ba6-8945-7048ee208f9f"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 12, 26, 22, 35, 30, 198, DateTimeKind.Local).AddTicks(7250), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Guest" }
                });
        }
    }
}
