-- =============================================
-- Author:		Dejan Vasic
-- Create date: 13-OCT-09
-- Modifications
-- Date			Author			Description
-- =============================================
create procedure [dbo].[spReportWeeklySales]
	@pubid int,					
	@ed datetime,
	@status int = null,			-- optional
	@subCategoryId int = null	-- optional
as 
begin
select ds.AdDesignId as iFlogID, 
		(select mc.Title from MainCategory mc where mc.MainCategoryId = sc.ParentId) AS Category,
		sc.Title as SubCategory, 
		bk.UserId, bk.BookReference, bk.BookingType,
		case bk.BookingStatus 
			when 1 then 'Booked'
			when 2 then 'Expired'
			when 3 then 'Cancelled'
			when 4 then 'Unpaid'
		end as [Status],
		ln.NumOfWords, 
		case ln.UseBoldHeader 
			when 0 then 0
			when 1 then 1
		end as BoldHeadings,
		case ln.UsePhoto	
			when 0 then 0
			when 1 then 1
		end as Photos,
		pb.Title as Publication,
		(en.EditionAdPrice * 0.9) as PriceExGST,
		(en.EditionAdPrice * 0.1) as GST,
		(en.EditionAdPrice) as TotalPrice
		
from AdBooking bk
	inner join BookEntry en ON en.AdBookingId = bk.AdBookingId
	inner join AdDesign ds on ds.AdId = bk.AdId
	inner join LineAd ln on ln.AdDesignId = ds.AdDesignId
	inner join MainCategory sc on sc.MainCategoryId = bk.MainCategoryId
	inner join Publication pb on pb.PublicationId = en.PublicationId
where 
	en.PublicationId = @pubId 
	and en.EditionDate = @ed
	and (@status is null or (@status is not null and bk.BookingStatus = @status))
	and (@subCategoryId is null or (@subCategoryId is not null and bk.MainCategoryId = @subCategoryId))
order by Category, SubCategory

end 

