CREATE TABLE [dbo].[ContactAddress] (
    [ContactAddressId] INT IDENTITY (1, 1) NOT NULL,
    [ContactId]        INT NOT NULL,
    [AddressId]        INT NOT NULL,
    CONSTRAINT [PK_ContactAddress] PRIMARY KEY CLUSTERED ([ContactAddressId] ASC),
    CONSTRAINT [FK_ContactAddress_Address] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[Address] ([AddressId]),
    CONSTRAINT [FK_ContactAddress_Contact] FOREIGN KEY ([ContactId]) REFERENCES [dbo].[Contact] ([ContactId])
);

