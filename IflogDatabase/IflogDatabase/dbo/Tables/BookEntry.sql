CREATE TABLE [dbo].[BookEntry] (
    [BookEntryId]      INT           IDENTITY (1, 1) NOT NULL,
    [EditionDate]      DATETIME      NULL,
    [AdBookingId]      INT           NULL,
    [PublicationId]    INT           NULL,
    [EditionAdPrice]   MONEY         NULL,
    [PublicationPrice] MONEY         NULL,
    [BaseRateId]       INT           NULL,
    [RateType]         NVARCHAR (20) NULL,
    CONSTRAINT [PK_BookEntry] PRIMARY KEY CLUSTERED ([BookEntryId] ASC),
    CONSTRAINT [FK_BookEntry_AdBooking] FOREIGN KEY ([AdBookingId]) REFERENCES [dbo].[AdBooking] ([AdBookingId]),
    CONSTRAINT [FK_BookEntry_Publication] FOREIGN KEY ([PublicationId]) REFERENCES [dbo].[Publication] ([PublicationId])
);

