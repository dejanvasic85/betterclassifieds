CREATE TABLE [dbo].[Billing_Bank] (
    [BankId]          UNIQUEIDENTIFIER NOT NULL,
    [BankName]        NVARCHAR (50)    COLLATE Latin1_General_CI_AS NOT NULL,
    [Description]     NVARCHAR (250)   COLLATE Latin1_General_CI_AS NOT NULL,
    [GatewayUrl]      NVARCHAR (500)   COLLATE Latin1_General_CI_AS NOT NULL,
    [ReturnLinkText]  NVARCHAR (200)   COLLATE Latin1_General_CI_AS NOT NULL,
    [GSTRate]         DECIMAL (18, 2)  NOT NULL,
    [RefundPolicyUrl] NVARCHAR (500)   COLLATE Latin1_General_CI_AS NOT NULL,
    [ReturnLinkUrl]   NVARCHAR (500)   COLLATE Latin1_General_CI_AS NOT NULL,
    [AllowOverride]   BIT              NOT NULL,
    CONSTRAINT [PK_Billing_Bank] PRIMARY KEY CLUSTERED ([BankId] ASC)
);

