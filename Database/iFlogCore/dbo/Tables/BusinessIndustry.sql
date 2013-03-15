CREATE TABLE [dbo].[BusinessIndustry] (
    [BusinessIndustryId] INT           IDENTITY (1, 1) NOT NULL,
    [Title]              NVARCHAR (50) COLLATE Latin1_General_CI_AS NULL,
    [Description]        NVARCHAR (50) COLLATE Latin1_General_CI_AS NULL,
    CONSTRAINT [PK_BusinessIndustry] PRIMARY KEY CLUSTERED ([BusinessIndustryId] ASC)
);

