-- =============================================
-- Author:			Uche Njoku
-- Create date:		14-Jly-2009
-- Modifications
-- =============================================
Create procedure [dbo].[psp_Betterclassified_GetAdBookingByEndDate]
	@endDate dateTime
	
as
begin
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	set nocount on
	select b.* from dbo.AdBooking b
	where b.EndDate =@endDate
end
