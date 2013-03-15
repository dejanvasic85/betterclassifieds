CREATE TABLE [dbo].[OnlineAdEnquiry] (
    [OnlineAdEnquiryId] INT            IDENTITY (1, 1) NOT NULL,
    [FullName]          NVARCHAR (100) NOT NULL,
    [OnlineAdId]        INT            NOT NULL,
    [EnquiryTypeId]     INT            NOT NULL,
    [Email]             NVARCHAR (100) NULL,
    [Phone]             NVARCHAR (12)  NULL,
    [EnquiryText]       NVARCHAR (MAX) NULL,
    [OpenDate]          DATETIME       NULL,
    [CreatedDate]       DATETIME       NULL,
    [Active]            BIT            NULL,
    CONSTRAINT [PK_OnlineAdEnquiry] PRIMARY KEY CLUSTERED ([OnlineAdEnquiryId] ASC),
    CONSTRAINT [FK_OnlineAdEnquiry_EnquiryType] FOREIGN KEY ([EnquiryTypeId]) REFERENCES [dbo].[EnquiryType] ([EnquiryTypeId]),
    CONSTRAINT [FK_OnlineAdEnquiry_OnlineAd] FOREIGN KEY ([OnlineAdId]) REFERENCES [dbo].[OnlineAd] ([OnlineAdId])
);

