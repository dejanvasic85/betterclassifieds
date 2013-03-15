CREATE TABLE [dbo].[AccountTransaction] (
    [AccountTransactionId] INT           IDENTITY (1, 1) NOT NULL,
    [ClientCode]           VARCHAR (10)  COLLATE Latin1_General_CI_AS NOT NULL,
    [Description]          VARCHAR (MAX) COLLATE Latin1_General_CI_AS NULL,
    [TransactionDate]      DATETIME      NOT NULL,
    [TransactionTypeId]    INT           NOT NULL,
    CONSTRAINT [PK_AccountTransaction] PRIMARY KEY CLUSTERED ([AccountTransactionId] ASC),
    CONSTRAINT [FK_AccountTransaction_AccountTransactionType] FOREIGN KEY ([TransactionTypeId]) REFERENCES [dbo].[AccountTransactionType] ([TransactionTypeId])
);

