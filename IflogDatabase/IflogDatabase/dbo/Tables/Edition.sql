CREATE TABLE [dbo].[Edition] (
    [EditionId]     INT      IDENTITY (1, 1) NOT NULL,
    [PublicationId] INT      NULL,
    [EditionDate]   DATETIME NULL,
    [Deadline]      DATETIME NULL,
    [Active]        BIT      NULL,
    CONSTRAINT [PK_Edition] PRIMARY KEY CLUSTERED ([EditionId] ASC),
    CONSTRAINT [FK_Edition_Publication] FOREIGN KEY ([PublicationId]) REFERENCES [dbo].[Publication] ([PublicationId])
);

