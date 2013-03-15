CREATE TABLE [dbo].[BillingSettings] (
    [ClientCode]            VARCHAR (10)     COLLATE Latin1_General_CI_AS NOT NULL,
    [BankId]                UNIQUEIDENTIFIER NOT NULL,
    [BankName]              NVARCHAR (50)    COLLATE Latin1_General_CI_AS NULL,
    [Description]           NVARCHAR (250)   COLLATE Latin1_General_CI_AS NULL,
    [GatewayUrl]            NVARCHAR (500)   COLLATE Latin1_General_CI_AS NULL,
    [ReturnLinkText]        NVARCHAR (200)   COLLATE Latin1_General_CI_AS NULL,
    [GSTRate]               DECIMAL (18, 2)  NULL,
    [RefundPolicyUrl]       NVARCHAR (500)   COLLATE Latin1_General_CI_AS NULL,
    [ReturnLinkUrl]         NVARCHAR (500)   COLLATE Latin1_General_CI_AS NULL,
    [CollectAddressDetails] BIT              NOT NULL,
    [InvoiceBannerImageId]  UNIQUEIDENTIFIER NOT NULL,
    [ReferencePrefix]       NCHAR (5)        COLLATE Latin1_General_CI_AS NOT NULL,
    [PP_BusinessEmail]      NVARCHAR (300)   COLLATE Latin1_General_CI_AS NOT NULL,
    [PP_CurrencyCode]       UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_BillingSettings] PRIMARY KEY CLUSTERED ([ClientCode] ASC, [BankId] ASC),
    CONSTRAINT [FK_BillingSettings_Billing_Bank] FOREIGN KEY ([BankId]) REFERENCES [dbo].[Billing_Bank] ([BankId])
);

