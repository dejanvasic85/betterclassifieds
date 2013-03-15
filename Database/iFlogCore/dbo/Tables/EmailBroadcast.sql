CREATE TABLE [dbo].[EmailBroadcast] (
    [EmailBroadcastId] INT              IDENTITY (1, 1) NOT NULL,
    [TemplateName]     NVARCHAR (100)   COLLATE Latin1_General_CI_AS NOT NULL,
    [BroadcastId]      UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_EmailBroadcast] PRIMARY KEY CLUSTERED ([EmailBroadcastId] ASC)
);

