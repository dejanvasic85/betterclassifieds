CREATE TABLE [dbo].[Industry] (
    [IndustryId]  INT            IDENTITY (1, 1) NOT NULL,
    [Title]       NVARCHAR (50)  NOT NULL,
    [Description] NVARCHAR (250) NULL,
    CONSTRAINT [PK_Industry_1] PRIMARY KEY CLUSTERED ([IndustryId] ASC)
);

