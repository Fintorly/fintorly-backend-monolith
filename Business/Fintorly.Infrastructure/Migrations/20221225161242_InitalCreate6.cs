using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fintorly.Infrastructure.Migrations
{
    public partial class InitalCreate6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mentors_ProfilePictures_ProfilePictureId",
                table: "Mentors");

            migrationBuilder.AddForeignKey(
                name: "FK_Mentors_ProfilePictures_ProfilePictureId",
                table: "Mentors",
                column: "ProfilePictureId",
                principalTable: "ProfilePictures",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mentors_ProfilePictures_ProfilePictureId",
                table: "Mentors");

            migrationBuilder.AddForeignKey(
                name: "FK_Mentors_ProfilePictures_ProfilePictureId",
                table: "Mentors",
                column: "ProfilePictureId",
                principalTable: "ProfilePictures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
