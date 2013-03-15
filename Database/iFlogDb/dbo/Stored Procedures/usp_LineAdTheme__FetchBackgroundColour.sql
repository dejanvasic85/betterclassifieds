-- =============================================
-- Author:		Dejan Vasic
-- Create date: 27-FEB-2011
-- =============================================
CREATE PROCEDURE [dbo].[usp_LineAdTheme__FetchBackgroundColour]
	@HeaderColourCode		varchar(10) = NULL,
	@BorderColourCode	varchar(10) = NULL
AS
/*
	[usp_LineAdTheme__FetchBackgroundColour]
		@HeaderColourCode	= NULL,
		@BorderColourCode	= NULL
*/
BEGIN

	declare @true	 tinyint = 1;

	-- Fetch the possible colour coes first
	SELECT TOP 1
			lat.BackgroundColourCode, lat.BackgroundColourName
	FROM	LineAdTheme lat
	WHERE	lat.HeaderColourCode = ISNULL(@HeaderColourCode, lat.HeaderColourCode)
		AND	lat.BorderColourCode = ISNULL(@BorderColourCode, lat.BorderColourCode)
		AND	lat.IsActive = @true;
	
END


