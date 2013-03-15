CREATE TABLE [dbo].[NonPublicationDate] (
    [NonPublicationDateId] INT      IDENTITY (1, 1) NOT NULL,
    [PublicationId]        INT      NOT NULL,
    [EditionDate]          DATETIME NOT NULL,
    CONSTRAINT [PK_NonPublicationDate] PRIMARY KEY CLUSTERED ([NonPublicationDateId] ASC),
    CONSTRAINT [FK_NonPublicationDate_Publication] FOREIGN KEY ([PublicationId]) REFERENCES [dbo].[Publication] ([PublicationId])
);

