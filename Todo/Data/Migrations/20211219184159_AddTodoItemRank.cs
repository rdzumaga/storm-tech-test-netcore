﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Todo.Data.Migrations
{
    public partial class AddTodoItemRank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "TodoItems",
                type: "INTEGER",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rank",
                table: "TodoItems");
        }
    }
}
