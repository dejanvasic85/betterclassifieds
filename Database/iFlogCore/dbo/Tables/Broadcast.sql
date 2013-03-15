CREATE TABLE [dbo].[Broadcast] (
    [BroadcastId]     UNIQUEIDENTIFIER NOT NULL,
    [EntityCode]      NVARCHAR (10)    COLLATE Latin1_General_CI_AS NOT NULL,
    [ApplicationName] NVARCHAR (50)    COLLATE Latin1_General_CI_AS NULL,
    [Type]            NCHAR (10)       COLLATE Latin1_General_CI_AS NOT NULL,
    [CreatedDateTime] DATETIME         NOT NULL,
    CONSTRAINT [PK_Broadcast] PRIMARY KEY CLUSTERED ([BroadcastId] ASC)
);

