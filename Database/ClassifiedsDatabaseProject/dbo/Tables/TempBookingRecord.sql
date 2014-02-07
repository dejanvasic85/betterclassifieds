CREATE TABLE [dbo].[TempBookingRecord] (
    [BookingRecordId] UNIQUEIDENTIFIER NOT NULL,
    [TotalCost]       MONEY            NOT NULL,
    [SessionID]       VARCHAR (25)     NOT NULL,
    [UserId]          VARCHAR (50)     NOT NULL,
    [DateTime]        DATETIME         NOT NULL,
    [AdReferenceId]   VARCHAR (20)     NOT NULL,
    CONSTRAINT [PK_TempBookingRecord] PRIMARY KEY CLUSTERED ([BookingRecordId] ASC)
);

