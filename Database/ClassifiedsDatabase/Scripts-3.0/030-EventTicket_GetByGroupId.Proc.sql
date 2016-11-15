
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EventTicket_GetByEventGroupId]
       @eventGroupId	INT
AS
BEGIN
	SELECT	t.*
	FROM	EventGroupTicket gt
	JOIN	EventTicket t
		ON	t.EventTicketId = gt.EventTicketId
	WHERE	gt.EventGroupId = @eventGroupId
END