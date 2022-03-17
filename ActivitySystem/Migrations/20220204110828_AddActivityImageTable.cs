using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ActivitySystem.Migrations
{
    public partial class AddActivityImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Organizers_OrganizerId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_OrganizerId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "OrganizerId",
                table: "Activities");

            migrationBuilder.CreateTable(
                name: "ActivityImages",
                columns: table => new
                {
                    ActivityImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActivityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityImages", x => x.ActivityImageId);
                    table.ForeignKey(
                        name: "FK_ActivityImages_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityImages_ActivityId",
                table: "ActivityImages",
                column: "ActivityId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityImages");

            migrationBuilder.AddColumn<int>(
                name: "OrganizerId",
                table: "Activities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_OrganizerId",
                table: "Activities",
                column: "OrganizerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Organizers_OrganizerId",
                table: "Activities",
                column: "OrganizerId",
                principalTable: "Organizers",
                principalColumn: "OrganizerId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
