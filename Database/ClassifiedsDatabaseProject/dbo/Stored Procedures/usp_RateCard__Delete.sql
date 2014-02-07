-- =============================================
-- Author:		Dejan Vasic
-- Create date: 01-JAN-2010
-- =============================================
CREATE PROCEDURE [dbo].[usp_RateCard__Delete]
	 @RateCardId		INT
	,@IsCascade			BIT = 1
AS
BEGIN
	BEGIN TRANSACTION
	
	declare @baseRateId int;
	SELECT	@baseRateId = r.BaseRateId 
	FROM	Ratecard r 
	WHERE	r.RatecardId = @RateCardId;
	
	IF @IsCascade = 1
	begin
		DELETE PublicationRate WHERE RatecardId = @RateCardId;
	end
	
	DELETE Ratecard WHERE RatecardId = @RateCardId;
	DELETE BaseRate WHERE BaseRateId = @baseRateId;
	
	COMMIT TRANSACTION
END


