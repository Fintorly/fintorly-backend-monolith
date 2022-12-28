using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fintorly.Infrastructure.Migrations
{
    public partial class InitalCreate6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("0da58992-6ff0-4861-8628-3dc046772f2e"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("2cd462c1-59c5-4fff-87e7-52e4cc88eac0"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("93bd5b1a-be65-4d95-94b4-c35741d8e773"));

            migrationBuilder.DeleteData(
                table: "OperationClaims",
                keyColumn: "Id",
                keyValue: new Guid("bcf0bd41-b86b-43f6-9c51-cdb7303af2dd"));

            migrationBuilder.AlterColumn<string>(
                name: "PhoneModel",
                table: "MentorAndOperationClaims",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "OsType",
                table: "MentorAndOperationClaims",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "IpAddress",
                table: "MentorAndOperationClaims",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                table: "MentorAndOperationClaims",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OsType",
                table: "MentorAndOperationClaims",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IpAddress",
                table: "MentorAndOperationClaims",
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
                    { new Guid("0da58992-6ff0-4861-8628-3dc046772f2e"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 12, 26, 22, 45, 38, 757, DateTimeKind.Local).AddTicks(1550), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Guest" },
                    { new Guid("2cd462c1-59c5-4fff-87e7-52e4cc88eac0"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 12, 26, 22, 45, 38, 757, DateTimeKind.Local).AddTicks(1530), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mentor" },
                    { new Guid("93bd5b1a-be65-4d95-94b4-c35741d8e773"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 12, 26, 22, 45, 38, 757, DateTimeKind.Local).AddTicks(1520), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User" },
                    { new Guid("bcf0bd41-b86b-43f6-9c51-cdb7303af2dd"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 12, 26, 22, 45, 38, 757, DateTimeKind.Local).AddTicks(1540), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin" }
                });
        }
    }
}
