CREATE TABLE [dbo].[PublicationType] (
    [PublicationTypeId] INT            NOT NULL,
    [Code]              NCHAR (10)     NULL,
    [Title]             NVARCHAR (50)  NULL,
    [Description]       NVARCHAR (255) NULL,
    CONSTRAINT [PK_PublicationType] PRIMARY KEY CLUSTERED ([PublicationTypeId] ASC)
);

