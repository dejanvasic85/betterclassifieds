CREATE TABLE [dbo].[PublicationCategory] (
    [PublicationCategoryId] INT            IDENTITY (1, 1) NOT NULL,
    [Title]                 NVARCHAR (50)  NULL,
    [Description]           NVARCHAR (MAX) NULL,
    [ImageUrl]              NVARCHAR (255) NULL,
    [ParentId]              INT            NULL,
    [MainCategoryId]        INT            NULL,
    [PublicationId]         INT            NULL,
    CONSTRAINT [PK_PublicationCategory] PRIMARY KEY CLUSTERED ([PublicationCategoryId] ASC),
    CONSTRAINT [FK_PublicationCategory_MainCategory] FOREIGN KEY ([MainCategoryId]) REFERENCES [dbo].[MainCategory] ([MainCategoryId]),
    CONSTRAINT [FK_PublicationCategory_Publication] FOREIGN KEY ([PublicationId]) REFERENCES [dbo].[Publication] ([PublicationId])
);

