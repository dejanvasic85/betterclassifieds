CREATE TABLE [dbo].[AdBookingOrderSummary] (
    [AdBookingOrderSummaryId] INT           IDENTITY (1, 1) NOT NULL,
    [AdBookingId]             INT           NOT NULL,
    [BookingReference]        VARCHAR (50)  NOT NULL,
    [PaymentReference]        VARCHAR (50)  NULL,
    [Total]                   MONEY         NULL,
    [BusinessName]            VARCHAR (100) NULL,
    [BusinessAddress]         VARCHAR (400) NULL,
    [BusinessPhoneNumber]     VARCHAR (400) NULL,
    [RecipientName]           VARCHAR (100) NULL,
    [RecipientAddress]        VARCHAR (400) NULL,
    [RecipientPhoneNumber]    VARCHAR (20)  NULL,
    [CreatedDate]             DATETIME      NULL,
    [CreatedDateUtc]          DATETIME      NULL,
    [BookingStartDate]          DATETIME      NULL,
    CONSTRAINT [PK_AdBookingOrderSummary] PRIMARY KEY CLUSTERED ([AdBookingOrderSummaryId] ASC),
    CONSTRAINT [FK_AdBookingOrderSummary_AdBooking] FOREIGN KEY ([AdBookingId]) REFERENCES [dbo].[AdBooking] ([AdBookingId])
);

