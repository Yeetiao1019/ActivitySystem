using Microsoft.EntityFrameworkCore.Migrations;

namespace ActivitySystem.Migrations
{
    public partial class ActivityOneToOneActivityImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityImages_Activities_ActivityImageId",
                table: "ActivityImages");

            migrationBuilder.DropColumn(
                name: "ActivityImageId",
                table: "Activities");

            //migrationBuilder.AlterColumn<int>(
            //    name: "ActivityImageId",
            //    table: "ActivityImages",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityImages_ActivityId",
                table: "ActivityImages",
                column: "ActivityId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityImages_Activities_ActivityId",
                table: "ActivityImages",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "ActivityId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityImages_Activities_ActivityId",
                table: "ActivityImages");

            migrationBuilder.DropIndex(
                name: "IX_ActivityImages_ActivityId",
                table: "ActivityImages");

            migrationBuilder.AlterColumn<int>(
                name: "ActivityImageId",
                table: "ActivityImages",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ActivityImageId",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityImages_Activities_ActivityImageId",
                table: "ActivityImages",
                column: "ActivityImageId",
                principalTable: "Activities",
                principalColumn: "ActivityId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
