CREATE TABLE [dbo].[BaseRate] (
    [BaseRateId]        INT            IDENTITY (1, 1) NOT NULL,
    [Title]             NVARCHAR (50)  NULL,
    [Description]       NVARCHAR (MAX) NULL,
    [StartDate]         DATETIME       NULL,
    [EndDate]           DATETIME       NULL,
    [ImageUrl]          NVARCHAR (255) NULL,
    [UpgradeBaseRateId] INT            NULL,
    CONSTRAINT [PK_BaseRate] PRIMARY KEY CLUSTERED ([BaseRateId] ASC)
);

