CREATE TABLE [dbo].[LineAdColourCode] (
    [LineAdColourId]   INT           IDENTITY (1, 1) NOT NULL,
    [LineAdColourName] VARCHAR (50)  NOT NULL,
    [ColourCode]       VARCHAR (10)  NOT NULL,
    [Cyan]             INT           NULL,
    [Magenta]          INT           NULL,
    [Yellow]           INT           NULL,
    [KeyCode]          INT           NULL,
    [IsActive]         BIT           NOT NULL,
    [SortOrder]        INT           NULL,
    [CreatedDate]      DATETIME      NULL,
    [CreatedByUser]    VARCHAR (100) NULL,
    CONSTRAINT [PK_LineAdColourCode] PRIMARY KEY CLUSTERED ([LineAdColourId] ASC)
);

