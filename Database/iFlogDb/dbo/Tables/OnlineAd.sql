CREATE TABLE [dbo].[OnlineAd] (
    [OnlineAdId]     INT            IDENTITY (1, 1) NOT NULL,
    [AdDesignId]     INT            NULL,
    [Heading]        NVARCHAR (255) NULL,
    [Description]    NVARCHAR (MAX) NULL,
    [HtmlText]       NVARCHAR (MAX) NULL,
    [Price]          MONEY          NULL,
    [LocationId]     INT            NULL,
    [LocationAreaId] INT            NULL,
    [ContactName]    NVARCHAR (200) NULL,
    [ContactType]    NVARCHAR (20)  NULL,
    [ContactValue]   NVARCHAR (100) NULL,
    [NumOfViews]     INT            NULL,
    CONSTRAINT [PK_OnlineAd] PRIMARY KEY CLUSTERED ([OnlineAdId] ASC),
    CONSTRAINT [FK_OnlineAd_AdDesign] FOREIGN KEY ([AdDesignId]) REFERENCES [dbo].[AdDesign] ([AdDesignId]),
    CONSTRAINT [FK_OnlineAd_Location] FOREIGN KEY ([LocationId]) REFERENCES [dbo].[Location] ([LocationId]),
    CONSTRAINT [FK_OnlineAd_LocationArea] FOREIGN KEY ([LocationAreaId]) REFERENCES [dbo].[LocationArea] ([LocationAreaId])
);

