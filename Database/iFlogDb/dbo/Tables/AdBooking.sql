CREATE TABLE [dbo].[AdBooking] (
    [AdBookingId]    INT           IDENTITY (1, 1) NOT NULL,
    [StartDate]      DATETIME      NULL,
    [EndDate]        DATETIME      NULL,
    [TotalPrice]     MONEY         NULL,
    [BookReference]  NCHAR (10)    NULL,
    [AdId]           INT           NULL,
    [UserId]         NVARCHAR (50) NULL,
    [BookingStatus]  INT           NULL,
    [MainCategoryId] INT           NULL,
    [BookingType]    NVARCHAR (20) NULL,
    [BookingDate]    DATETIME      NULL,
    [Insertions]     INT           NULL,
    CONSTRAINT [PK_AdBooking] PRIMARY KEY CLUSTERED ([AdBookingId] ASC),
    CONSTRAINT [FK_AdBooking_Ad] FOREIGN KEY ([AdId]) REFERENCES [dbo].[Ad] ([AdId]),
    CONSTRAINT [FK_AdBooking_MainCategory] FOREIGN KEY ([MainCategoryId]) REFERENCES [dbo].[MainCategory] ([MainCategoryId])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1=Booked, 2=Expired, 3=Cancelled, 4=Unpaid', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AdBooking', @level2type = N'COLUMN', @level2name = N'BookingStatus';

