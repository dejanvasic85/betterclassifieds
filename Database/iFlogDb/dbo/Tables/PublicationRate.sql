CREATE TABLE [dbo].[PublicationRate] (
    [PublicationRateId]     INT IDENTITY (1, 1) NOT NULL,
    [PublicationAdTypeId]   INT NULL,
    [PublicationCategoryId] INT NULL,
    [RatecardId]            INT NULL,
    CONSTRAINT [PK_PublicationRate] PRIMARY KEY CLUSTERED ([PublicationRateId] ASC),
    CONSTRAINT [FK_PublicationRate_PublicationAdType] FOREIGN KEY ([PublicationAdTypeId]) REFERENCES [dbo].[PublicationAdType] ([PublicationAdTypeId]),
    CONSTRAINT [FK_PublicationRate_PublicationCategory] FOREIGN KEY ([PublicationCategoryId]) REFERENCES [dbo].[PublicationCategory] ([PublicationCategoryId]),
    CONSTRAINT [FK_PublicationRate_Ratecard] FOREIGN KEY ([RatecardId]) REFERENCES [dbo].[Ratecard] ([RatecardId])
);

