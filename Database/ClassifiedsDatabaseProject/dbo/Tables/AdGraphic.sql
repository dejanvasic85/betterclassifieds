CREATE TABLE [dbo].[AdGraphic] (
    [AdGraphicId]  INT            IDENTITY (1, 1) NOT NULL,
    [AdDesignId]   INT            NULL,
    [DocumentID]   NVARCHAR (100) NULL,
    [Filename]     NVARCHAR (100) NULL,
    [ImageType]    NVARCHAR (50)  NULL,
    [ModifiedDate] DATETIME       NULL,
    CONSTRAINT [PK_AdGraphic] PRIMARY KEY CLUSTERED ([AdGraphicId] ASC),
    CONSTRAINT [FK_AdGraphic_AdDesign] FOREIGN KEY ([AdDesignId]) REFERENCES [dbo].[AdDesign] ([AdDesignId])
);

