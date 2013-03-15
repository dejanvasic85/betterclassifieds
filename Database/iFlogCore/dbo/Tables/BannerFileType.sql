CREATE TABLE [dbo].[BannerFileType] (
    [BannerFileTypeId] INT           IDENTITY (1, 1) NOT NULL,
    [Code]             NVARCHAR (5)  COLLATE Latin1_General_CI_AS NOT NULL,
    [Title]            NVARCHAR (50) COLLATE Latin1_General_CI_AS NOT NULL,
    CONSTRAINT [PK_BannerFileType] PRIMARY KEY CLUSTERED ([Code] ASC)
);

