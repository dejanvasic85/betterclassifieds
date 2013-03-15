CREATE TABLE [dbo].[AdDesign] (
    [AdDesignId]      INT      IDENTITY (1, 1) NOT NULL,
    [AdId]            INT      NULL,
    [AdTypeId]        INT      NULL,
    [Status]          INT      NULL,
    [CreatedDate]     DATETIME NULL,
    [Version]         INT      NULL,
    [FirstAdDesignId] INT      NULL,
    CONSTRAINT [PK_AdDesign] PRIMARY KEY CLUSTERED ([AdDesignId] ASC),
    CONSTRAINT [FK_AdDesign_Ad] FOREIGN KEY ([AdId]) REFERENCES [dbo].[Ad] ([AdId]),
    CONSTRAINT [FK_AdDesign_AdType] FOREIGN KEY ([AdTypeId]) REFERENCES [dbo].[AdType] ([AdTypeId])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1=Pending, 2=Approved, 3=Cancelled', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AdDesign', @level2type = N'COLUMN', @level2name = N'Status';

