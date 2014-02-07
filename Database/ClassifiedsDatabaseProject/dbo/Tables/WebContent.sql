CREATE TABLE [dbo].[WebContent] (
    [WebContentId]     INT            IDENTITY (1, 1) NOT NULL,
    [Title]            VARCHAR (50)   NULL,
    [PageId]           VARCHAR (50)   NOT NULL,
    [WebContent]       NVARCHAR (MAX) NULL,
    [LastModifiedDate] DATETIME       NULL,
    [LastModifiedUser] VARCHAR (50)   NULL,
    CONSTRAINT [PK_WebContent] PRIMARY KEY CLUSTERED ([WebContentId] ASC)
);

