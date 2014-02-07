CREATE TABLE [dbo].[AdBookingExtension] (
    [AdBookingExtensionId] INT          IDENTITY (1, 1) NOT NULL,
    [AdBookingId]          INT          NOT NULL,
    [Insertions]           INT          NULL,
    [ExtensionPrice]       MONEY        NULL,
    [Status]               INT          NOT NULL,
    [LastModifiedUserId]   VARCHAR (50) NULL,
    [LastModifiedDate]     DATETIME     NULL,
    CONSTRAINT [PK_AdBookingExtension] PRIMARY KEY CLUSTERED ([AdBookingExtensionId] ASC)
);

