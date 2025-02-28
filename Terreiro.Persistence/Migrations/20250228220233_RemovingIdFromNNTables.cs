using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Terreiro.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemovingIdFromNNTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_user_roles",
                table: "user_roles");

            migrationBuilder.DropIndex(
                name: "IX_user_roles_role_id",
                table: "user_roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_events",
                table: "user_events");

            migrationBuilder.DropIndex(
                name: "IX_user_events_user_id",
                table: "user_events");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_event_items",
                table: "user_event_items");

            migrationBuilder.DropIndex(
                name: "IX_user_event_items_user_id",
                table: "user_event_items");

            migrationBuilder.DropColumn(
                name: "id",
                table: "user_roles");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "user_roles");

            migrationBuilder.DropColumn(
                name: "deleted_at",
                table: "user_roles");

            migrationBuilder.DropColumn(
                name: "id",
                table: "user_events");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "user_events");

            migrationBuilder.DropColumn(
                name: "deleted_at",
                table: "user_events");

            migrationBuilder.DropColumn(
                name: "id",
                table: "user_event_items");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "user_event_items");

            migrationBuilder.DropColumn(
                name: "deleted_at",
                table: "user_event_items");

            migrationBuilder.DropSequence(
                name: "UserEventItemSequence");

            migrationBuilder.DropSequence(
                name: "UserEventSequence");

            migrationBuilder.DropSequence(
                name: "UserRoleSequence");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_roles",
                table: "user_roles",
                columns: new[] { "role_id", "user_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_events",
                table: "user_events",
                columns: new[] { "user_id", "event_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_event_items",
                table: "user_event_items",
                columns: new[] { "user_id", "event_item_id" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_user_roles",
                table: "user_roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_events",
                table: "user_events");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_event_items",
                table: "user_event_items");

            migrationBuilder.CreateSequence(
                name: "UserEventItemSequence");

            migrationBuilder.CreateSequence(
                name: "UserEventSequence");

            migrationBuilder.CreateSequence(
                name: "UserRoleSequence");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "user_roles",
                type: "integer",
                nullable: false,
                defaultValueSql: "nextval('\"UserRoleSequence\"')");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "user_roles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "deleted_at",
                table: "user_roles",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "user_events",
                type: "integer",
                nullable: false,
                defaultValueSql: "nextval('\"UserEventSequence\"')");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "user_events",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "deleted_at",
                table: "user_events",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "user_event_items",
                type: "integer",
                nullable: false,
                defaultValueSql: "nextval('\"UserEventItemSequence\"')");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "user_event_items",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "deleted_at",
                table: "user_event_items",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_roles",
                table: "user_roles",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_events",
                table: "user_events",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_event_items",
                table: "user_event_items",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_user_roles_role_id",
                table: "user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_events_user_id",
                table: "user_events",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_event_items_user_id",
                table: "user_event_items",
                column: "user_id");
        }
    }
}
