-- =============================================
-- Author:		Dejan Vasic
-- Create date: 27-FEB-2011
-- =============================================
CREATE PROCEDURE [dbo].[usp_LineAdTheme__FetchHeaderColour]
	@BorderColourCode		varchar(10) = NULL,
	@BackgroundColourCode	varchar(10) = NULL
AS
/*
	usp_LineAdTheme__FetchHeaderColour
		@BorderColourCode		= NULL,
		@BackgroundColourCode	= NULL
*/
BEGIN

	declare @true	 tinyint = 1;

	-- Fetch the possible colour coes first
	SELECT TOP 1
			lat.HeaderColourCode, lat.HeaderColourName
	FROM	LineAdTheme lat
	WHERE	lat.BorderColourCode = ISNULL(@BorderColourCode, lat.BorderColourCode)
		AND	lat.BackgroundColourCode = ISNULL(@BackgroundColourCode, lat.BackgroundColourCode)
		AND	lat.IsActive = @true;
	
END


