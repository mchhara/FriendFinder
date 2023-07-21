using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class InvideEntityAdded2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invides_Users_SourceUserId",
                table: "Invides");

            migrationBuilder.DropForeignKey(
                name: "FK_Invides_Users_TargetUserId",
                table: "Invides");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invides",
                table: "Invides");

            migrationBuilder.RenameTable(
                name: "Invides",
                newName: "Invitations");

            migrationBuilder.RenameIndex(
                name: "IX_Invides_TargetUserId",
                table: "Invitations",
                newName: "IX_Invitations_TargetUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invitations",
                table: "Invitations",
                columns: new[] { "SourceUserId", "TargetUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_Users_SourceUserId",
                table: "Invitations",
                column: "SourceUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invitations_Users_TargetUserId",
                table: "Invitations",
                column: "TargetUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_Users_SourceUserId",
                table: "Invitations");

            migrationBuilder.DropForeignKey(
                name: "FK_Invitations_Users_TargetUserId",
                table: "Invitations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invitations",
                table: "Invitations");

            migrationBuilder.RenameTable(
                name: "Invitations",
                newName: "Invides");

            migrationBuilder.RenameIndex(
                name: "IX_Invitations_TargetUserId",
                table: "Invides",
                newName: "IX_Invides_TargetUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invides",
                table: "Invides",
                columns: new[] { "SourceUserId", "TargetUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Invides_Users_SourceUserId",
                table: "Invides",
                column: "SourceUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invides_Users_TargetUserId",
                table: "Invides",
                column: "TargetUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
