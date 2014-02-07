-- =============================================
-- Author:		Dejan Vasic
-- Create date: 19th Nov 2008
-- Description:	Returns all publications that are 
--				able to print or contain the specified
--				ad type. If showOnline parameter is true,
--				it will also include online publications.
-- Date			Author			Description
-- 28-NOV-09	Dejan Vasic		Adding sort order logic
-- =============================================
CREATE PROCEDURE [dbo].[spGetPublicationsByAdType]
	-- Add the parameters for the stored procedure here
	@adTypeId int,
	@showOnline bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

IF @showOnline = 1 
	
	-- If we want to list all publications including online. Execute this.
	SELECT pub.PublicationId, pub.Title, pub.Description, pub.ImageUrl
	FROM Publication pub
	INNER JOIN PublicationAdType typ ON pub.PublicationId = typ.PublicationId
	WHERE typ.AdTypeId = @adTypeId AND pub.Active = 1
	ORDER BY pub.SortOrder
	
ELSE
	SELECT pub.PublicationId, pub.Title, pub.Description, pub.ImageUrl
	FROM Publication pub
	INNER JOIN PublicationType ptp ON pub.PublicationTypeId = ptp.PublicationTypeId
	INNER JOIN PublicationAdType typ ON pub.PublicationId = typ.PublicationId
	WHERE 
		typ.AdTypeId = @adTypeId AND 
		(ptp.Code = 'NEWS' OR ptp.Code = 'MAG') AND
		pub.ACTIVE = 1
	ORDER BY pub.SortOrder
END


