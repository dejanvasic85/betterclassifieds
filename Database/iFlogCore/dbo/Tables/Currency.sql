CREATE TABLE [dbo].[Currency] (
    [CurrencyId]   UNIQUEIDENTIFIER NOT NULL,
    [CurrencyCode] NCHAR (5)        COLLATE Latin1_General_CI_AS NOT NULL,
    [CurrencyName] NVARCHAR (150)   COLLATE Latin1_General_CI_AS NOT NULL
);

