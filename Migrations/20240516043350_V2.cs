using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yota_backend.Migrations
{
    /// <inheritdoc />
    public partial class V2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageBlob",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "ImageBlob",
                table: "Albums");

            migrationBuilder.RenameColumn(
                name: "AES",
                table: "Tracks",
                newName: "FilePath");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Playlists",
                newName: "Title");

            migrationBuilder.AlterColumn<long>(
                name: "Bytes",
                table: "Tracks",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Playlists",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Playlists",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Playlists",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Albums",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Playlists");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Albums");

            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Tracks",
                newName: "AES");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Playlists",
                newName: "Name");

            migrationBuilder.AlterColumn<int>(
                name: "Bytes",
                table: "Tracks",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Playlists",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageBlob",
                table: "Playlists",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageBlob",
                table: "Albums",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
