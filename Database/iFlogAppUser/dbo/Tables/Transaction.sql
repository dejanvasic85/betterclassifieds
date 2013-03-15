CREATE TABLE [dbo].[Transaction] (
    [TransactionId]    UNIQUEIDENTIFIER CONSTRAINT [DF_Transaction_TransactionId] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [Title]            NVARCHAR (50)    NOT NULL,
    [Description]      NVARCHAR (MAX)   NULL,
    [TransactionType]  NVARCHAR (20)    NULL,
    [ApplicationId]    UNIQUEIDENTIFIER NOT NULL,
    [AppUserProfileId] UNIQUEIDENTIFIER NOT NULL,
    [Date]             DATETIME         NOT NULL,
    [RowTimeStamp]     ROWVERSION       NOT NULL,
    CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED ([TransactionId] ASC),
    CONSTRAINT [FK_Transaction_aspnet_Applications] FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].[aspnet_Applications] ([ApplicationId]),
    CONSTRAINT [FK_Transaction_aspnet_Users] FOREIGN KEY ([AppUserProfileId]) REFERENCES [dbo].[aspnet_Users] ([UserId])
);

