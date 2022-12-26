using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fintorly.Infrastructure.Migrations
{
    public partial class InitalCreate5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mentors_Advertisements_AdvertisementId",
                table: "Mentors");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "ProfilePictureId",
                table: "Mentors",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "AdvertisementId",
                table: "Mentors",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Mentors_Advertisements_AdvertisementId",
                table: "Mentors",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mentors_Advertisements_AdvertisementId",
                table: "Mentors");

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

            migrationBuilder.AlterColumn<Guid>(
                name: "ProfilePictureId",
                table: "Mentors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AdvertisementId",
                table: "Mentors",
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
                    { new Guid("3448a641-44ca-41ff-bb63-965d03960cd2"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 12, 26, 22, 36, 15, 398, DateTimeKind.Local).AddTicks(3470), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mentor" },
                    { new Guid("6aa676a1-d06d-47ae-ab38-2aeed3234146"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 12, 26, 22, 36, 15, 398, DateTimeKind.Local).AddTicks(3490), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Guest" },
                    { new Guid("96f40fe8-818b-494b-9215-bb677a440823"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 12, 26, 22, 36, 15, 398, DateTimeKind.Local).AddTicks(3480), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin" },
                    { new Guid("cd70ee9e-af65-403a-b72b-83f644824525"), new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2022, 12, 26, 22, 36, 15, 398, DateTimeKind.Local).AddTicks(3460), true, false, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "User" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Mentors_Advertisements_AdvertisementId",
                table: "Mentors",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
