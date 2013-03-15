CREATE TABLE [dbo].[BannerGroup] (
    [BannerGroupId]    UNIQUEIDENTIFIER NOT NULL,
    [ClientCode]       VARCHAR (10)     COLLATE Latin1_General_CI_AS NOT NULL,
    [Title]            VARCHAR (50)     COLLATE Latin1_General_CI_AS NULL,
    [Height]           INT              NULL,
    [Width]            INT              NULL,
    [AcceptedFileType] NVARCHAR (5)     COLLATE Latin1_General_CI_AS CONSTRAINT [DF_BannerGroup_AcceptedFileType] DEFAULT (N'OTHER') NULL,
    [IsTimerEnabled]   BIT              NOT NULL,
    [IsActive]         BIT              CONSTRAINT [DF_BannerGroup_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_BannerGroup] PRIMARY KEY CLUSTERED ([BannerGroupId] ASC),
    CONSTRAINT [BannerGroup_BannerFileType_FK] FOREIGN KEY ([AcceptedFileType]) REFERENCES [dbo].[BannerFileType] ([Code])
);

