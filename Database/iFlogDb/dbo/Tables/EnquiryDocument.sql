CREATE TABLE [dbo].[EnquiryDocument] (
    [EnquiryDocumentId] INT      IDENTITY (1, 1) NOT NULL,
    [OnlineAdEnquiryId] INT      NOT NULL,
    [DocumentId]        INT      NULL,
    [CreatedDate]       DATETIME NULL,
    CONSTRAINT [PK_EnquiryDocument] PRIMARY KEY CLUSTERED ([EnquiryDocumentId] ASC),
    CONSTRAINT [FK_EnquiryDocument_OnlineAdEnquiry] FOREIGN KEY ([OnlineAdEnquiryId]) REFERENCES [dbo].[OnlineAdEnquiry] ([OnlineAdEnquiryId])
);

