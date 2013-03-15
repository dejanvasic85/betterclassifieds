CREATE TABLE [dbo].[BannerAudit] (
    [BannerAuditId]   INT              IDENTITY (1, 1) NOT NULL,
    [BannerId]        UNIQUEIDENTIFIER NOT NULL,
    [ActionTypeName]  VARCHAR (20)     COLLATE Latin1_General_CI_AS CONSTRAINT [DF_BannerAudit_ActionTypeName] DEFAULT ('View') NULL,
    [IPAddress]       VARCHAR (50)     COLLATE Latin1_General_CI_AS NULL,
    [ClientCode]      VARCHAR (10)     COLLATE Latin1_General_CI_AS NOT NULL,
    [ApplicationName] VARCHAR (50)     COLLATE Latin1_General_CI_AS NULL,
    [PageUrl]         VARCHAR (500)    COLLATE Latin1_General_CI_AS NULL,
    [Location]        VARCHAR (20)     COLLATE Latin1_General_CI_AS NULL,
    [UserGroup]       VARCHAR (100)    COLLATE Latin1_General_CI_AS NULL,
    [Gender]          VARCHAR (10)     COLLATE Latin1_General_CI_AS NULL,
    [Username]        VARCHAR (50)     COLLATE Latin1_General_CI_AS NULL,
    [UserId]          UNIQUEIDENTIFIER NULL,
    [PostCode]        VARCHAR (10)     COLLATE Latin1_General_CI_AS NULL,
    [CreatedDateTime] DATETIME         NOT NULL,
    [BannerTargetUrl] NVARCHAR (500)   COLLATE Latin1_General_CI_AS NULL,
    CONSTRAINT [PK_BannerAudit] PRIMARY KEY CLUSTERED ([BannerAuditId] ASC),
    CONSTRAINT [FK_BannerAudit_Banner] FOREIGN KEY ([BannerId]) REFERENCES [dbo].[Banner] ([BannerId])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Available options: Click, View', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BannerAudit', @level2type = N'COLUMN', @level2name = N'ActionTypeName';

