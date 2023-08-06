using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatRooms.Migrations
{
    /// <inheritdoc />
    public partial class InitialV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Chatrooms_ChatroomId",
                table: "Messages");

            migrationBuilder.AlterColumn<int>(
                name: "ChatroomId",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Chatrooms_ChatroomId",
                table: "Messages",
                column: "ChatroomId",
                principalTable: "Chatrooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Chatrooms_ChatroomId",
                table: "Messages");

            migrationBuilder.AlterColumn<int>(
                name: "ChatroomId",
                table: "Messages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Chatrooms_ChatroomId",
                table: "Messages",
                column: "ChatroomId",
                principalTable: "Chatrooms",
                principalColumn: "Id");
        }
    }
}
