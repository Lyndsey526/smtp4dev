using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Rnwood.Smtp4dev.Migrations
{
    public partial class AddSessionToMessageReln : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"SELECT * INTO Messages_temp FROM Messages");

            migrationBuilder.DropTable(
              name: "Messages");

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    From = table.Column<string>(nullable: true),
                    To = table.Column<string>(nullable: true),
                    ReceivedDate = table.Column<DateTime>(nullable: false),
                    Subject = table.Column<string>(nullable: true),
                    Data = table.Column<byte[]>(nullable: true),
                    MimeParseError = table.Column<string>(nullable: true),
                    SessionId = table.Column<Guid>(nullable: true)
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SessionId",
                table: "Messages",
                column: "SessionId");

            migrationBuilder.Sql(@"
                INSERT INTO Messages (Id, [From], [To], ReceivedDate, Subject, Data, MimeParseError, SessionId)
                SELECT Id, [From], [To], ReceivedDate, Subject, Data, MimeParseError, NULL
                FROM Messages_temp");

            migrationBuilder.DropTable(
              name: "Messages_temp");
        }

    }
}
