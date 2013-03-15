CREATE TABLE [dbo].[EmailTemplate] (
    [EmailTemplateId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]            NVARCHAR (100) COLLATE Latin1_General_CI_AS NOT NULL,
    [Description]     NVARCHAR (150) COLLATE Latin1_General_CI_AS NULL,
    [EmailContent]    NVARCHAR (MAX) COLLATE Latin1_General_CI_AS NOT NULL,
    [Subject]         NVARCHAR (50)  COLLATE Latin1_General_CI_AS NOT NULL,
    [Sender]          NVARCHAR (100) COLLATE Latin1_General_CI_AS NOT NULL,
    [EntityCode]      NVARCHAR (10)  COLLATE Latin1_General_CI_AS NOT NULL,
    CONSTRAINT [PK_EmailTemplate] PRIMARY KEY CLUSTERED ([EmailTemplateId] ASC)
);

