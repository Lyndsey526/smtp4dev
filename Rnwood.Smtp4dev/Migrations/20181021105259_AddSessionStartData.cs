using Microsoft.EntityFrameworkCore.Migrations;

namespace Rnwood.Smtp4dev.Migrations
{
    public partial class AddSessionStartData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
    SELECT * INTO MessagesTemp FROM Messages;

    DROP TABLE Messages;

    CREATE TABLE Messages (
        Id UNIQUEIDENTIFIER NOT NULL,
        [From] NVARCHAR(MAX),
        [To] NVARCHAR(MAX),
        ReceivedDate DATETIME2 NOT NULL,
        Subject NVARCHAR(MAX),
        Data VARBINARY(MAX),
        MimeParseError NVARCHAR(MAX),
        SessionId UNIQUEIDENTIFIER,
        AttachmentCount INT NOT NULL DEFAULT 0,
        IsUnread BIT NOT NULL DEFAULT 0,
        RelayError NVARCHAR(MAX),
        ImapUid INT NOT NULL
    );

    INSERT INTO Messages (
        Id,
        [From],
        [To],
        ReceivedDate,
        Subject,
        Data,
        MimeParseError,
        SessionId,
        AttachmentCount,
        IsUnread,
        RelayError,
        ImapUid
    )
    SELECT 
        Id,
        [From],
        [To],
        ReceivedDate,
        Subject,
        Data,
        MimeParseError,
        SessionId,
        AttachmentCount,
        IsUnread,
        RelayError,
        ROW_NUMBER() OVER (ORDER BY ReceivedDate) AS ImapUid
    FROM MessagesTemp;

    DROP TABLE MessagesTemp;

    CREATE INDEX IX_Messages_SessionId ON Messages (SessionId);
");
        }
    }
}
