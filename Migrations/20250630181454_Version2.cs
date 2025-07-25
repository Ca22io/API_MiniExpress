using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniExpress.Migrations
{
    /// <inheritdoc />
    public partial class Version2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Perfils_IdPerfil",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Perfils",
                table: "Perfils");

            migrationBuilder.RenameTable(
                name: "Perfils",
                newName: "Perfis");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Perfis",
                table: "Perfis",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Perfis_IdPerfil",
                table: "Usuarios",
                column: "IdPerfil",
                principalTable: "Perfis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Perfis_IdPerfil",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Perfis",
                table: "Perfis");

            migrationBuilder.RenameTable(
                name: "Perfis",
                newName: "Perfils");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Perfils",
                table: "Perfils",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Perfils_IdPerfil",
                table: "Usuarios",
                column: "IdPerfil",
                principalTable: "Perfils",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
