using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RimmuTraining.WebApp.Migrations
{
    public partial class practicestartingmember : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StartingMemberId",
                table: "Practices",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Practices_StartingMemberId",
                table: "Practices",
                column: "StartingMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Practices_Members_StartingMemberId",
                table: "Practices",
                column: "StartingMemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Practices_Members_StartingMemberId",
                table: "Practices");

            migrationBuilder.DropIndex(
                name: "IX_Practices_StartingMemberId",
                table: "Practices");

            migrationBuilder.DropColumn(
                name: "StartingMemberId",
                table: "Practices");
        }
    }
}
