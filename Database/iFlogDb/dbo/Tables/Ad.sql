CREATE TABLE [dbo].[Ad] (
    [AdId]          INT            IDENTITY (1, 1) NOT NULL,
    [Title]         NVARCHAR (50)  NULL,
    [Comments]      NVARCHAR (255) NULL,
    [UseAsTemplate] BIT            NULL,
    CONSTRAINT [PK_Ad] PRIMARY KEY CLUSTERED ([AdId] ASC)
);

