CREATE TABLE [dbo].[BannerReference] (
    [BannerReferenceId]     INT              IDENTITY (1, 1) NOT NULL,
    [BannerReferenceTypeId] INT              NULL,
    [BannerId]              UNIQUEIDENTIFIER NULL,
    [Value]                 VARCHAR (255)    COLLATE Latin1_General_CI_AS NULL,
    CONSTRAINT [PK_BannerReference] PRIMARY KEY CLUSTERED ([BannerReferenceId] ASC),
    CONSTRAINT [FK_BannerReference_Banner] FOREIGN KEY ([BannerId]) REFERENCES [dbo].[Banner] ([BannerId]),
    CONSTRAINT [FK_BannerReference_BannerReferenceType] FOREIGN KEY ([BannerReferenceTypeId]) REFERENCES [dbo].[BannerReferenceType] ([BannerReferenceTypeId])
);

