CREATE TABLE [dbo].[Entity] (
    [EntityCode]         NVARCHAR (10) COLLATE Latin1_General_CI_AS NOT NULL,
    [EntityName]         NVARCHAR (50) COLLATE Latin1_General_CI_AS NOT NULL,
    [Password]           NVARCHAR (20) COLLATE Latin1_General_CI_AS NULL,
    [PrimaryContactId]   INT           NULL,
    [CreatedDate]        DATETIME      NULL,
    [TimeZone]           INT           NULL,
    [ABN]                NVARCHAR (12) COLLATE Latin1_General_CI_AS NULL,
    [BusinessIndustryId] INT           NULL,
    [Active]             BIT           NULL,
    [TimeZoneRef]        VARCHAR (100) COLLATE Latin1_General_CI_AS NULL,
    [CultureRef]         VARCHAR (10)  COLLATE Latin1_General_CI_AS NULL,
    CONSTRAINT [PK_Entity] PRIMARY KEY CLUSTERED ([EntityCode] ASC),
    CONSTRAINT [FK_Entity_BusinessIndustry] FOREIGN KEY ([BusinessIndustryId]) REFERENCES [dbo].[BusinessIndustry] ([BusinessIndustryId]),
    CONSTRAINT [FK_Entity_Contact] FOREIGN KEY ([PrimaryContactId]) REFERENCES [dbo].[Contact] ([ContactId])
);

