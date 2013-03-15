CREATE TABLE [dbo].[EmailBroadcastEntry] (
    [EmailBroadcastEntryId] INT              IDENTITY (1, 1) NOT NULL,
    [BroadcastId]           UNIQUEIDENTIFIER NOT NULL,
    [Email]                 NVARCHAR (100)   COLLATE Latin1_General_CI_AS NOT NULL,
    [EmailContent]          NVARCHAR (MAX)   COLLATE Latin1_General_CI_AS NOT NULL,
    [LastRetryDateTime]     DATETIME         NULL,
    [SentDateTime]          DATETIME         NULL,
    [RetryNo]               INT              NOT NULL,
    [Subject]               VARCHAR (50)     COLLATE Latin1_General_CI_AS NOT NULL,
    [Sender]                NVARCHAR (50)    COLLATE Latin1_General_CI_AS NOT NULL,
    [IsBodyHtml]            BIT              NOT NULL,
    [Priority]              INT              NOT NULL,
    [CreateDateTime]        DATETIME         CONSTRAINT [EmailBroadcastEntry_CreatedDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_EmailBroadcastEntry] PRIMARY KEY CLUSTERED ([EmailBroadcastEntryId] ASC)
);

