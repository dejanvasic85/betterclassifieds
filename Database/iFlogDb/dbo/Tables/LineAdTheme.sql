CREATE TABLE [dbo].[LineAdTheme] (
    [LineAdThemeId]        INT            IDENTITY (1, 1) NOT NULL,
    [ThemeName]            VARCHAR (100)  NULL,
    [Description]          VARCHAR (MAX)  NULL,
    [DescriptionHtml]      NVARCHAR (MAX) NULL,
    [ImageUrl]             VARCHAR (500)  NULL,
    [HeaderColourCode]     VARCHAR (10)   NULL,
    [HeaderColourName]     VARCHAR (50)   NULL,
    [BorderColourCode]     VARCHAR (10)   NULL,
    [BorderColourName]     VARCHAR (50)   NULL,
    [BackgroundColourCode] VARCHAR (10)   NULL,
    [BackgroundColourName] VARCHAR (50)   NULL,
    [IsHeadingSuperBold]   BIT            NULL,
    [IsActive]             BIT            NULL,
    [CreatedDate]          DATETIME       NULL,
    [CreatedByUser]        VARCHAR (100)  NULL,
    CONSTRAINT [PK_LineAdTheme] PRIMARY KEY CLUSTERED ([LineAdThemeId] ASC)
);

