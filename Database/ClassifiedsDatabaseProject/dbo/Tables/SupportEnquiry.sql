CREATE TABLE [dbo].[SupportEnquiry] (
    [SupportEnquiryId] INT           IDENTITY (1, 1) NOT NULL,
    [EnquiryTypeName]  VARCHAR (50)  NULL,
    [FullName]         VARCHAR (100) NULL,
    [Email]            VARCHAR (100) NULL,
    [Phone]            VARCHAR (15)  NULL,
    [Subject]          VARCHAR (100) NULL,
    [EnquiryText]      VARCHAR (MAX) NULL,
    [CreatedDate]      DATETIME      NULL,
    CONSTRAINT [PK_SupportEnquiry] PRIMARY KEY CLUSTERED ([SupportEnquiryId] ASC)
);

