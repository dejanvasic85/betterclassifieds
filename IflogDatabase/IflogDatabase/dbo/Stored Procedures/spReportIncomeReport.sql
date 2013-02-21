-- =============================================
-- Author:		Dejan Vasic
-- Create date: 13-OCT-09
-- Modifications
-- Date			Author			Description
-- =============================================
create procedure [dbo].[spReportIncomeReport]
	@startDate datetime,
	@endDate datetime
as 
begin

select	tr.TransactionDate, tr.UserId, tr.Title as BookReference, 
		bk.BookingType, 
		case tr.TransactionType 
			when 1 then 'Credit Card'
			when 2 then 'PayPal'
		end as [TransactionType],
		(tr.Amount * 0.9) as PriceExGST,(tr.Amount * 0.1) as GST, tr.Amount as TotalPrice
from [Transaction] tr
	inner join AdBooking bk on bk.BookReference = tr.Title 
	where tr.TransactionDate >= @startDate and tr.TransactionDate <= @endDate
end

