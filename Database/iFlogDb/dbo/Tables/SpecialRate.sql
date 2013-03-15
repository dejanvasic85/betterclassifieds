CREATE TABLE [dbo].[SpecialRate] (
    [SpecialRateId]    INT             IDENTITY (1, 1) NOT NULL,
    [BaseRateId]       INT             NULL,
    [NumOfInsertions]  INT             NULL,
    [MaximumWords]     INT             NULL,
    [SetPrice]         MONEY           NULL,
    [Discount]         NUMERIC (18, 2) NULL,
    [NumOfAds]         INT             NULL,
    [LineAdBoldHeader] BIT             NULL,
    [LineAdImage]      BIT             NULL,
    [NumberOfImages]   INT             NULL,
    CONSTRAINT [PK_SpecialRate] PRIMARY KEY CLUSTERED ([SpecialRateId] ASC),
    CONSTRAINT [FK_SpecialRate_BaseRate] FOREIGN KEY ([BaseRateId]) REFERENCES [dbo].[BaseRate] ([BaseRateId])
);

