CREATE TABLE [dbo].[DocumentStorage] (
    [DocumentID]         UNIQUEIDENTIFIER NOT NULL,
    [ApplicationCode]    NVARCHAR (30)    COLLATE Latin1_General_CI_AS NOT NULL,
    [EntityCode]         NVARCHAR (10)    COLLATE Latin1_General_CI_AS NOT NULL,
    [AccountID]          UNIQUEIDENTIFIER NULL,
    [DocumentCategoryId] INT              NOT NULL,
    [Username]           NVARCHAR (50)    COLLATE Latin1_General_CI_AS NULL,
    [FileType]           NVARCHAR (MAX)   COLLATE Latin1_General_CI_AS NOT NULL,
    [FileData]           VARBINARY (MAX)  NULL,
    [FileLength]         INT              NOT NULL,
    [NumberOfChunks]     NUMERIC (18)     NULL,
    [FileName]           NVARCHAR (100)   COLLATE Latin1_General_CI_AS NULL,
    [Reference]          NVARCHAR (100)   COLLATE Latin1_General_CI_AS NULL,
    [IsPrivate]          BIT              NULL,
    [StartDate]          DATETIME         NULL,
    [EndDate]            DATETIME         NULL,
    [CreatedDate]        DATETIME         NOT NULL,
    [UpdatedDate]        DATETIME         NULL,
    [IsFileComplete]     BIT              CONSTRAINT [DF_DocumentStorage_IsFileComplete] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_DocumentStorage] PRIMARY KEY CLUSTERED ([DocumentID] ASC),
    CONSTRAINT [FK_DocumentStorage_DocumentCategory] FOREIGN KEY ([DocumentCategoryId]) REFERENCES [dbo].[DocumentCategory] ([DocumentCategoryId]) ON DELETE CASCADE
);

