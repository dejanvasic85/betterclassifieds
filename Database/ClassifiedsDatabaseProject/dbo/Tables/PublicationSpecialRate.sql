CREATE TABLE [dbo].[PublicationSpecialRate] (
    [PublicationSpecialRateId] INT IDENTITY (1, 1) NOT NULL,
    [SpecialRateId]            INT NULL,
    [PublicationAdTypeId]      INT NULL,
    [PublicationCategoryId]    INT NULL,
    CONSTRAINT [PK_PublicationSpecialRate] PRIMARY KEY CLUSTERED ([PublicationSpecialRateId] ASC),
    CONSTRAINT [FK_PublicationSpecialRate_PublicationAdType] FOREIGN KEY ([PublicationAdTypeId]) REFERENCES [dbo].[PublicationAdType] ([PublicationAdTypeId]),
    CONSTRAINT [FK_PublicationSpecialRate_PublicationCategory] FOREIGN KEY ([PublicationCategoryId]) REFERENCES [dbo].[PublicationCategory] ([PublicationCategoryId]),
    CONSTRAINT [FK_PublicationSpecialRate_SpecialRate] FOREIGN KEY ([SpecialRateId]) REFERENCES [dbo].[SpecialRate] ([SpecialRateId])
);

