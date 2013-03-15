CREATE TABLE [dbo].[Ratecard] (
    [RatecardId]             INT          IDENTITY (1, 1) NOT NULL,
    [BaseRateId]             INT          NULL,
    [MinCharge]              MONEY        NULL,
    [MaxCharge]              MONEY        NULL,
    [RatePerMeasureUnit]     MONEY        NULL,
    [MeasureUnitLimit]       INT          NULL,
    [PhotoCharge]            MONEY        NULL,
    [BoldHeading]            MONEY        NULL,
    [OnlineEditionBundle]    MONEY        NULL,
    [LineAdSuperBoldHeading] MONEY        NULL,
    [LineAdColourHeading]    MONEY        NULL,
    [LineAdColourBorder]     MONEY        NULL,
    [LineAdColourBackground] MONEY        NULL,
    [LineAdExtraImage]       MONEY        NULL,
    [CreatedDate]            DATETIME     NULL,
    [CreatedByUser]          VARCHAR (50) NULL,
    CONSTRAINT [PK_Ratecard] PRIMARY KEY CLUSTERED ([RatecardId] ASC),
    CONSTRAINT [FK_Ratecard_BaseRate] FOREIGN KEY ([BaseRateId]) REFERENCES [dbo].[BaseRate] ([BaseRateId])
);

