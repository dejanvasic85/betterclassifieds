CREATE VIEW [dbo].[Invoice]
AS
SELECT     dbo.AdBooking.StartDate, dbo.AdBooking.EndDate, dbo.AdBooking.TotalPrice, dbo.MainCategory.Title AS Category, dbo.AdBooking.BookReference, 
                      dbo.AdBooking.UserId, dbo.Publication.Title AS Publication, dbo.BookEntry.EditionDate, dbo.[Transaction].TransactionDate
FROM         dbo.AdBooking INNER JOIN
                      dbo.MainCategory ON dbo.AdBooking.MainCategoryId = dbo.MainCategory.MainCategoryId INNER JOIN
                      dbo.BookEntry ON dbo.AdBooking.AdBookingId = dbo.BookEntry.AdBookingId INNER JOIN
                      dbo.Publication ON dbo.BookEntry.PublicationId = dbo.Publication.PublicationId INNER JOIN
                      dbo.[Transaction] ON dbo.AdBooking.BookReference = dbo.[Transaction].Title

