CREATE TABLE [dbo].[EntityContact] (
    [EntityContactId] INT           IDENTITY (1, 1) NOT NULL,
    [EntityCode]      NVARCHAR (10) COLLATE Latin1_General_CI_AS NULL,
    [ContactId]       INT           NULL,
    CONSTRAINT [PK_EntityContact] PRIMARY KEY CLUSTERED ([EntityContactId] ASC),
    CONSTRAINT [FK_EntityContact_Contact] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[Contact] ([ContactId]),
    CONSTRAINT [FK_EntityContact_Entity] FOREIGN KEY ([EntityCode]) REFERENCES [dbo].[Entity] ([EntityCode])
);

