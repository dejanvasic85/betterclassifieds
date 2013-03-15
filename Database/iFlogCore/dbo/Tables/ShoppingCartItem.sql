CREATE TABLE [dbo].[ShoppingCartItem] (
    [ShoppingCartItemId] UNIQUEIDENTIFIER NOT NULL,
    [ProductId]          UNIQUEIDENTIFIER NOT NULL,
    [Quantity]           DECIMAL (18, 2)  NOT NULL,
    [Price]              MONEY            NOT NULL,
    [ProductType]        NVARCHAR (20)    COLLATE Latin1_General_CI_AS NOT NULL,
    [Title]              NVARCHAR (100)   COLLATE Latin1_General_CI_AS NOT NULL,
    [Summary]            NVARCHAR (MAX)   COLLATE Latin1_General_CI_AS NULL,
    [ShoppingCartId]     UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_ShoppingCartItem] PRIMARY KEY CLUSTERED ([ShoppingCartItemId] ASC),
    CONSTRAINT [ShoppingCartItem_ShoppingCart_FK] FOREIGN KEY ([ShoppingCartId]) REFERENCES [dbo].[ShoppingCart] ([ShoppingCartId])
);

