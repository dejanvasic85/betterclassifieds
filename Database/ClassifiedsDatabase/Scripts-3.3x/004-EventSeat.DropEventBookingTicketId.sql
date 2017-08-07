  ALTER TABLE EventSeat
  DROP CONSTRAINT FK_EventSeatBooking_EventBookingTicket
  GO
  ALTER TABLE EventSeat
  DROP COLUMN EventBookingTicketId