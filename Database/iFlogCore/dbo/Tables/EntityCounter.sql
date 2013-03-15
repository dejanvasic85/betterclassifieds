CREATE TABLE [dbo].[EntityCounter] (
    [CounterId]       INT      IDENTITY (1, 1) NOT NULL,
    [DateTimeCreated] DATETIME NOT NULL,
    CONSTRAINT [PK_EntityCounter] PRIMARY KEY CLUSTERED ([CounterId] ASC)
);

