CREATE TABLE [dbo].[Transaction] (
    [TransactionId]   INT            IDENTITY (1, 1) NOT NULL,
    [TransactionType] INT            NULL,
    [UserId]          NVARCHAR (50)  NULL,
    [Title]           NVARCHAR (50)  NULL,
    [Description]     NVARCHAR (255) NULL,
    [Amount]          MONEY          NULL,
    [TransactionDate] DATETIME       NULL,
    [RowTimeStamp]    ROWVERSION     NULL,
    CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED ([TransactionId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Refers to the enumeration "TransactionType" in the booking system.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Transaction', @level2type = N'COLUMN', @level2name = N'TransactionType';

