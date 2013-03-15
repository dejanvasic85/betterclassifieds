CREATE TABLE [dbo].[EmailFrom] (
    [EmailFromID]  INT            IDENTITY (1, 1) NOT NULL,
    [EmailAddress] NVARCHAR (256) NULL,
    [Description]  NVARCHAR (256) NULL,
    CONSTRAINT [PK_EmailFrom] PRIMARY KEY CLUSTERED ([EmailFromID] ASC)
);

