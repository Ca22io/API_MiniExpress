using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniExpress.Migrations
{
    /// <inheritdoc />
    public partial class version71 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Principal",
                table: "Enderecos",
                type: "TEXT",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "INTEGER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Principal",
                table: "Enderecos",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 10,
                oldNullable: true);
        }
    }
}
