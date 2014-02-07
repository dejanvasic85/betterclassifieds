CREATE TABLE [dbo].[TutorAd] (
    [TutorAdId]      INT            IDENTITY (1, 1) NOT NULL,
    [OnlineAdId]     INT            NOT NULL,
    [Subjects]       NVARCHAR (500) NULL,
    [AgeGroupMin]    INT            NULL,
    [AgeGroupMax]    INT            NULL,
    [ExpertiseLevel] NVARCHAR (100) NULL,
    [TravelOption]   NVARCHAR (50)  NULL,
    [PricingOption]  NVARCHAR (100) NULL,
    [WhatToBring]    NVARCHAR (100) NULL,
    [Objective]      NVARCHAR (200) NULL,
    CONSTRAINT [PK_TutorAd] PRIMARY KEY CLUSTERED ([TutorAdId] ASC),
    CONSTRAINT [FK_TutorAd_OnlineAd] FOREIGN KEY ([OnlineAdId]) REFERENCES [dbo].[OnlineAd] ([OnlineAdId])
);

