CREATE TABLE [dbo].[BannerReferenceType] (
    [BannerReferenceTypeId] INT           IDENTITY (1, 1) NOT NULL,
    [Title]                 VARCHAR (100) COLLATE Latin1_General_CI_AS NULL,
    CONSTRAINT [PK_BannerReferenceType] PRIMARY KEY CLUSTERED ([BannerReferenceTypeId] ASC)
);

