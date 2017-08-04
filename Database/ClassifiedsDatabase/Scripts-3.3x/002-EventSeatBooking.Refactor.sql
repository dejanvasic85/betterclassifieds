exec sp_rename 'EventSeatBooking', 'EventSeat';

GO

exec sp_rename '[EventSeat].[EventSeatBookingId]', 'EventSeatId', 'COLUMN';

GO