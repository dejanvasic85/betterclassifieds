CREATE TABLE [dbo].[InvoiceItem] (
    [InvoiceItemId] UNIQUEIDENTIFIER NOT NULL,
    [Title]         NVARCHAR (100)   COLLATE Latin1_General_CI_AS NOT NULL,
    [Summary]       NVARCHAR (500)   COLLATE Latin1_General_CI_AS NULL,
    [Quantity]      DECIMAL (18, 2)  NOT NULL,
    [Price]         MONEY            NOT NULL,
    [InvoiceId]     UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_InvoiceItem] PRIMARY KEY CLUSTERED ([InvoiceItemId] ASC),
    CONSTRAINT [InvoiceItem_Invoice_FK] FOREIGN KEY ([InvoiceId]) REFERENCES [dbo].[Invoice] ([InvoiceId])
);

