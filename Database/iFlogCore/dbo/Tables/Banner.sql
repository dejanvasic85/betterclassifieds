CREATE TABLE [dbo].[Banner] (
    [BannerId]      UNIQUEIDENTIFIER NOT NULL,
    [ClientCode]    VARCHAR (10)     COLLATE Latin1_General_CI_AS NOT NULL,
    [BannerGroupId] UNIQUEIDENTIFIER NOT NULL,
    [Title]         VARCHAR (100)    COLLATE Latin1_General_CI_AS NOT NULL,
    [DocumentID]    UNIQUEIDENTIFIER NOT NULL,
    [NavigateUrl]   VARCHAR (150)    COLLATE Latin1_General_CI_AS NOT NULL,
    [RequestCount]  INT              CONSTRAINT [DF_Banner_RequestCount] DEFAULT ((0)) NOT NULL,
    [ClickCount]    INT              CONSTRAINT [DF_Banner_ClickCount] DEFAULT ((0)) NOT NULL,
    [StartDateTime] DATETIME         NOT NULL,
    [EndDateTime]   DATETIME         NOT NULL,
    [IsDeleted]     BIT              CONSTRAINT [DF_Banner_IsDeleted] DEFAULT ((0)) NOT NULL,
    [CreatedBy]     VARCHAR (50)     COLLATE Latin1_General_CI_AS NULL,
    [CreatedDate]   DATETIME         CONSTRAINT [DF_Banner_CreatedDate] DEFAULT (getdate()) NOT NULL,
    [ModifiedDate]  DATETIME         NULL,
    CONSTRAINT [PK_Banner] PRIMARY KEY CLUSTERED ([BannerId] ASC),
    CONSTRAINT [Banner_BannerGroup_FK] FOREIGN KEY ([BannerGroupId]) REFERENCES [dbo].[BannerGroup] ([BannerGroupId]),
    CONSTRAINT [FK_Banner_DocumentStorage] FOREIGN KEY ([DocumentID]) REFERENCES [dbo].[DocumentStorage] ([DocumentID])
);

