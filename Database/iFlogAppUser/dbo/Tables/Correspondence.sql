CREATE TABLE [dbo].[Correspondence] (
    [Id]               INT              IDENTITY (1, 1) NOT NULL,
    [AppUserProfileId] UNIQUEIDENTIFIER NOT NULL,
    [Title]            NVARCHAR (50)    NOT NULL,
    [Description]      NVARCHAR (250)   NULL,
    [Date]             DATETIME         NOT NULL,
    [Type]             INT              NULL,
    CONSTRAINT [PK_Correspondence] PRIMARY KEY CLUSTERED ([Id] ASC)
);

