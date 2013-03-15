CREATE TABLE [dbo].[BusinessCategory] (
    [BusinessCategoryId] INT            IDENTITY (1, 1) NOT NULL,
    [IndustryId]         INT            NOT NULL,
    [Title]              NVARCHAR (50)  NOT NULL,
    [Description]        NVARCHAR (250) NULL,
    CONSTRAINT [PK_BusinessCategory] PRIMARY KEY CLUSTERED ([BusinessCategoryId] ASC),
    CONSTRAINT [FK_BusinessCategory_Industry] FOREIGN KEY ([IndustryId]) REFERENCES [dbo].[Industry] ([IndustryId])
);

