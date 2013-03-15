CREATE TABLE [dbo].[AdType] (
    [AdTypeId]    INT            NOT NULL,
    [Code]        NVARCHAR (50)  NULL,
    [Title]       NVARCHAR (50)  NULL,
    [Description] NVARCHAR (MAX) NULL,
    [PaperBased]  BIT            NULL,
    [ImageUrl]    NVARCHAR (255) NULL,
    [Active]      BIT            NULL,
    CONSTRAINT [PK_AdType] PRIMARY KEY CLUSTERED ([AdTypeId] ASC)
);

