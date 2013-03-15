
CREATE procedure [dbo].[psp_Betterclassified_GetLineAdBookingByLastEdition]
	@EditionDate dateTime 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	select  
	UserId, 
	ab.BookReference, 
		(
		select AdDesignId from dbo.AdDesign ad2
		inner join dbo.AdType at2
			on ad2.AdTypeId = at2.AdTypeId
		and ad2.AdId = dg.AdId
		and at2.Code='ONLINE'
	) as AdDesignId,
	g.LastEditionDate, 
	ab.MainCategoryId,
	ab.AdBookingId, 
	ab.BookingDate, 
	ab.StartDate, 
	ab.EndDate, 
	ab.TotalPrice 

	from  dbo.AdBooking ab
	inner join dbo.AdDesign dg
		on dg.AdId = ab.AdId 
	inner join dbo.AdType at
		on at.AdTypeId = dg.AdTypeId
		and at.Code = 'LINE'
	inner join 
	(
		select MAX (e1.EditionDate) as LastEditionDate, e1.AdBookingId 
		from dbo.BookEntry e1			
	 group by e1.AdBookingId
	) g
	on g.AdBookingId = ab.AdBookingId 
	and  g.LastEditionDate = @EditionDate
END
