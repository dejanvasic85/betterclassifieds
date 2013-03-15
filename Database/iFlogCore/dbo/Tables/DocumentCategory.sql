CREATE TABLE [dbo].[DocumentCategory] (
    [DocumentCategoryId] INT           IDENTITY (1, 1) NOT NULL,
    [Title]              VARCHAR (50)  COLLATE Latin1_General_CI_AS NULL,
    [Code]               INT           NOT NULL,
    [ExpiryPurgeDays]    INT           NULL,
    [AcceptedFileTypes]  VARCHAR (255) COLLATE Latin1_General_CI_AS NULL,
    [MaximumFileSize]    INT           NULL,
    CONSTRAINT [PK_DocumentCategory] PRIMARY KEY CLUSTERED ([DocumentCategoryId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Refer to DslDocumentCategory within the Paramount Business Objects', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DocumentCategory', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Number of days images are kept until purged. NULL value indicates never.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DocumentCategory', @level2type = N'COLUMN', @level2name = N'ExpiryPurgeDays';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Semi Colon separated list of accepted file extensions', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DocumentCategory', @level2type = N'COLUMN', @level2name = N'AcceptedFileTypes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Maximum size of each file in bytes allowed for this category. 1 megabytes = 1 048 576 bytes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'DocumentCategory', @level2type = N'COLUMN', @level2name = N'MaximumFileSize';

