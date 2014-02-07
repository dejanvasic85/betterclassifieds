-- =============================================
-- Author:		Dejan Vasic
-- Create date: 23rd June 2010
-- Modifications
-- Date			Author			Description
-- =============================================
CREATE PROCEDURE [dbo].[spSpecialRateDelete]
	-- Add the parameters for the stored procedure here
	 @specialRateId		INT
	,@isCascade			BIT = 0
AS
BEGIN
	BEGIN TRANSACTION
	
	DECLARE @baseRateId INT;
	SELECT @baseRateId = sr.BaseRateId FROM SpecialRate sr WHERE sr.SpecialRateId = @specialRateId;
	
	IF @isCascade = 1
	begin
		DELETE PublicationSpecialRate WHERE SpecialRateId = @specialRateId;
	end
	
	DELETE SpecialRate WHERE SpecialRateId = @specialRateId;
	DELETE BaseRate WHERE BaseRateId = @baseRateId;
	
	COMMIT TRANSACTION
END


