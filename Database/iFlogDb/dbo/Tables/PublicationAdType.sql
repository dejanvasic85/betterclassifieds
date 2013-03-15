CREATE TABLE [dbo].[PublicationAdType] (
    [PublicationAdTypeId] INT IDENTITY (1, 1) NOT NULL,
    [PublicationId]       INT NULL,
    [AdTypeId]            INT NULL,
    CONSTRAINT [PK_PublicationAdType] PRIMARY KEY CLUSTERED ([PublicationAdTypeId] ASC),
    CONSTRAINT [FK_PublicationAdType_AdType] FOREIGN KEY ([AdTypeId]) REFERENCES [dbo].[AdType] ([AdTypeId]),
    CONSTRAINT [FK_PublicationAdType_Publication] FOREIGN KEY ([PublicationId]) REFERENCES [dbo].[Publication] ([PublicationId])
);

