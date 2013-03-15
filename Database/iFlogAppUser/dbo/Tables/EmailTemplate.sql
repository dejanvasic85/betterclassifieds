CREATE TABLE [dbo].[EmailTemplate] (
    [EmailTemplateId] INT            IDENTITY (1, 1) NOT NULL,
    [EmailHeader]     NVARCHAR (MAX) NOT NULL,
    [EmailBody]       NVARCHAR (MAX) NULL,
    [Description]     NVARCHAR (250) NULL,
    CONSTRAINT [PK_EmailTemplate] PRIMARY KEY CLUSTERED ([EmailTemplateId] ASC)
);

