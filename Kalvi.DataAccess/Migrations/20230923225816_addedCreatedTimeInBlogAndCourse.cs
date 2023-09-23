using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kalvi.DataAccess.Migrations
{
    public partial class addedCreatedTimeInBlogAndCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseDetailId",
                table: "Courses");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Blogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Blogs");

            migrationBuilder.AddColumn<int>(
                name: "CourseDetailId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
