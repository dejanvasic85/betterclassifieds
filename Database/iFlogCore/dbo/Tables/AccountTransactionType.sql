CREATE TABLE [dbo].[AccountTransactionType] (
    [TransactionTypeId] INT          IDENTITY (1, 1) NOT NULL,
    [Title]             VARCHAR (50) COLLATE Latin1_General_CI_AS NOT NULL,
    [ModuleId]          INT          NULL,
    CONSTRAINT [PK_AccountTransactionType] PRIMARY KEY CLUSTERED ([TransactionTypeId] ASC),
    CONSTRAINT [FK_AccountTransactionType_Module] FOREIGN KEY ([ModuleId]) REFERENCES [dbo].[Module] ([ModuleId])
);

