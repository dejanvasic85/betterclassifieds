CREATE TABLE [dbo].[Module] (
    [ModuleId]    INT            IDENTITY (1, 1) NOT NULL,
    [Title]       NVARCHAR (50)  COLLATE Latin1_General_CI_AS NULL,
    [Description] NVARCHAR (MAX) COLLATE Latin1_General_CI_AS NULL,
    CONSTRAINT [PK_Module] PRIMARY KEY CLUSTERED ([ModuleId] ASC)
);

