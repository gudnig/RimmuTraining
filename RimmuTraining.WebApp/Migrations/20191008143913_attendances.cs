using Microsoft.EntityFrameworkCore.Migrations;

namespace RimmuTraining.WebApp.Migrations
{
    public partial class attendances : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Members_MemberId",
                table: "Attendance");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Practices_PracticeId",
                table: "Attendance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attendance",
                table: "Attendance");

            migrationBuilder.RenameTable(
                name: "Attendance",
                newName: "Attendances");

            migrationBuilder.RenameIndex(
                name: "IX_Attendance_PracticeId",
                table: "Attendances",
                newName: "IX_Attendances_PracticeId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendance_MemberId",
                table: "Attendances",
                newName: "IX_Attendances_MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attendances",
                table: "Attendances",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Members_MemberId",
                table: "Attendances",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_Practices_PracticeId",
                table: "Attendances",
                column: "PracticeId",
                principalTable: "Practices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Members_MemberId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_Practices_PracticeId",
                table: "Attendances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attendances",
                table: "Attendances");

            migrationBuilder.RenameTable(
                name: "Attendances",
                newName: "Attendance");

            migrationBuilder.RenameIndex(
                name: "IX_Attendances_PracticeId",
                table: "Attendance",
                newName: "IX_Attendance_PracticeId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendances_MemberId",
                table: "Attendance",
                newName: "IX_Attendance_MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attendance",
                table: "Attendance",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Members_MemberId",
                table: "Attendance",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Practices_PracticeId",
                table: "Attendance",
                column: "PracticeId",
                principalTable: "Practices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
