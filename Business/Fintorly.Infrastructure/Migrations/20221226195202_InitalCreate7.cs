using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fintorly.Infrastructure.Migrations
{
    public partial class InitalCreate7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("1ce9f557-6939-477a-9410-b026433c0d87"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("1f1e12f1-d01b-43b1-873f-ebf16930f9a6"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("6c806f14-21c5-4e4c-9cd8-081115b05ee9"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("ea6cef4c-4abd-4525-9785-ee1dd14bb6fa"));

            migrationBuilder.AlterColumn<string>(
                name: "PhoneModel",
                table: "ApplicationRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "OsType",
                table: "ApplicationRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "IpAddress",
                table: "ApplicationRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "AdminId",
                table: "ApplicationRequests",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "IsActive", "IsDeleted", "ModifiedBy", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("4c54c2c6-757a-4389-8ceb-11d373b460f2"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 12, 26, 22, 52, 2, 677, DateTimeKind.Local).AddTicks(8050), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User" },
                    { new Guid("57fa6ffb-b565-49a2-b527-3d47b740f5d3"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 12, 26, 22, 52, 2, 677, DateTimeKind.Local).AddTicks(8070), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin" },
                    { new Guid("e588ac3c-c289-4ae2-940d-779860834b48"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 12, 26, 22, 52, 2, 677, DateTimeKind.Local).AddTicks(8080), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Guest" },
                    { new Guid("ffafddde-1250-461a-9d68-229f15ce61f5"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 12, 26, 22, 52, 2, 677, DateTimeKind.Local).AddTicks(8060), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mentor" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("4c54c2c6-757a-4389-8ceb-11d373b460f2"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("57fa6ffb-b565-49a2-b527-3d47b740f5d3"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("e588ac3c-c289-4ae2-940d-779860834b48"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("ffafddde-1250-461a-9d68-229f15ce61f5"));

            migrationBuilder.AlterColumn<string>(
                name: "PhoneModel",
                table: "ApplicationRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OsType",
                table: "ApplicationRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IpAddress",
                table: "ApplicationRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AdminId",
                table: "ApplicationRequests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "IsActive", "IsDeleted", "ModifiedBy", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { new Guid("1ce9f557-6939-477a-9410-b026433c0d87"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 12, 26, 22, 46, 55, 69, DateTimeKind.Local).AddTicks(9760), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mentor" },
                    { new Guid("1f1e12f1-d01b-43b1-873f-ebf16930f9a6"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 12, 26, 22, 46, 55, 69, DateTimeKind.Local).AddTicks(9770), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin" },
                    { new Guid("6c806f14-21c5-4e4c-9cd8-081115b05ee9"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 12, 26, 22, 46, 55, 69, DateTimeKind.Local).AddTicks(9750), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User" },
                    { new Guid("ea6cef4c-4abd-4525-9785-ee1dd14bb6fa"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 12, 26, 22, 46, 55, 69, DateTimeKind.Local).AddTicks(9790), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Guest" }
                });
        }
    }
}
