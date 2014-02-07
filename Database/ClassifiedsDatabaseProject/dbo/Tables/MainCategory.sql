CREATE TABLE [dbo].[MainCategory] (
    [MainCategoryId] INT            IDENTITY (1, 1) NOT NULL,
    [Title]          NVARCHAR (50)  NULL,
    [Description]    NVARCHAR (MAX) NULL,
    [ImageUrl]       NVARCHAR (255) NULL,
    [ParentId]       INT            NULL,
    [OnlineAdTag]    VARCHAR (50)   NULL,
    CONSTRAINT [PK_MainCategory] PRIMARY KEY CLUSTERED ([MainCategoryId] ASC)
);

