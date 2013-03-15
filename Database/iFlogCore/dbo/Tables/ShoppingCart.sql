CREATE TABLE [dbo].[ShoppingCart] (
    [ShoppingCartId]   UNIQUEIDENTIFIER NOT NULL,
    [UserId]           UNIQUEIDENTIFIER NOT NULL,
    [Status]           NVARCHAR (20)    COLLATE Latin1_General_CI_AS NOT NULL,
    [DateTimeCreated]  DATETIME         NOT NULL,
    [DateTimeModified] DATETIME         NOT NULL,
    [SessionId]        NVARCHAR (30)    COLLATE Latin1_General_CI_AS NOT NULL,
    [ClientCode]       NVARCHAR (11)    COLLATE Latin1_General_CI_AS NULL,
    CONSTRAINT [PK_ShoppingCart] PRIMARY KEY CLUSTERED ([ShoppingCartId] ASC)
);

