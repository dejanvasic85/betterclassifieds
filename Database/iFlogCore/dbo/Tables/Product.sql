CREATE TABLE [dbo].[Product] (
    [ProductId]   UNIQUEIDENTIFIER NOT NULL,
    [Title]       NVARCHAR (50)    COLLATE Latin1_General_CI_AS NOT NULL,
    [Description] NVARCHAR (250)   COLLATE Latin1_General_CI_AS NULL,
    [ImageId]     UNIQUEIDENTIFIER NULL,
    [ClientId]    VARCHAR (11)     COLLATE Latin1_General_CI_AS NOT NULL,
    CONSTRAINT [PK_Product_1] PRIMARY KEY CLUSTERED ([ProductId] ASC)
);

