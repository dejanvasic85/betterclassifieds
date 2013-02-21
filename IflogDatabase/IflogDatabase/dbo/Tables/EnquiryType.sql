CREATE TABLE [dbo].[EnquiryType] (
    [EnquiryTypeId] INT            IDENTITY (1, 1) NOT NULL,
    [Title]         NVARCHAR (100) NULL,
    [Active]        BIT            NULL,
    [CreatedDate]   DATETIME       NULL,
    CONSTRAINT [PK_EnquiryType] PRIMARY KEY CLUSTERED ([EnquiryTypeId] ASC)
);

