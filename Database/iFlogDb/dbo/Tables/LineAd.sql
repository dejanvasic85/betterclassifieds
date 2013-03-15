CREATE TABLE [dbo].[LineAd] (
    [LineAdId]                INT            IDENTITY (1, 1) NOT NULL,
    [AdDesignId]              INT            NOT NULL,
    [AdHeader]                NVARCHAR (255) NULL,
    [AdText]                  NVARCHAR (MAX) NOT NULL,
    [NumOfWords]              INT            NULL,
    [UsePhoto]                BIT            NULL,
    [UseBoldHeader]           BIT            NULL,
    [IsColourBoldHeading]     BIT            NULL,
    [IsColourBorder]          BIT            NULL,
    [IsColourBackground]      BIT            NULL,
    [IsSuperBoldHeading]      BIT            NULL,
    [IsFooterPhoto]           BIT            NULL,
    [BoldHeadingColourCode]   VARCHAR (10)   NULL,
    [BorderColourCode]        VARCHAR (10)   NULL,
    [BackgroundColourCode]    VARCHAR (10)   NULL,
    [FooterPhotoId]           VARCHAR (100)  NULL,
    [IsSuperHeadingPurchased] BIT            NULL,
    CONSTRAINT [PK_LineAd] PRIMARY KEY CLUSTERED ([LineAdId] ASC),
    CONSTRAINT [FK_LineAd_AdDesign] FOREIGN KEY ([AdDesignId]) REFERENCES [dbo].[AdDesign] ([AdDesignId])
);

