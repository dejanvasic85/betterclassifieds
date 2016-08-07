-- DROP THE EnquiryType and the Foreign key

ALTER TABLE OnlineAdEnquiry 
DROP CONSTRAINT FK_OnlineAdEnquiry_EnquiryType;

GO

ALTER TABLE OnlineAdEnquiry
DROP COLUMN EnquiryTypeId;

GO

DROP TABLE EnquiryType

GO

-- ALTER Procedure

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Dejan Vasic
-- Create date: 22-SEP-2015
-- Description:	Creates a new entry for the online ad enquiry based on the ad id (Booking id)
-- 
-- =============================================
IF EXISTS ( SELECT  *
            FROM    sys.objects
            WHERE   object_id = OBJECT_ID(N'OnlineAdEnquiry_Create')
                    AND type IN ( N'P', N'PC' ) ) 
DROP PROC dbo.OnlineAdEnquiry_Create

GO


CREATE PROCEDURE OnlineAdEnquiry_Create
		@adId			int,
		@fullName		varchar(100),
		@email			varchar(100),
		@enquiryText	varchar(max),
		@phone			varchar(12) = null,
		@enquiryId		int	output
AS
BEGIN

	SET NOCOUNT ON 

	DECLARE @onlineAdId		INT,
			@openDate		DATETIME = NULL,
			@createdDate	DATETIME = GETDATE(),
			@active			BIT = 1;

	-- Fetch the online ad ID for the FK
	SELECT	@onlineAdId = OnlineAdId
	FROM	OnlineAd o
	JOIN	AdDesign ds
		ON	ds.AdDesignId = o.AdDesignId
	JOIN	AdBooking bk
		ON	bk.AdId = ds.AdId
		AND bk.AdBookingId = @adId;
			

	INSERT INTO [dbo].[OnlineAdEnquiry]
			   ([FullName]
			   ,[OnlineAdId]
			   ,[Email]
			   ,[Phone]
			   ,[EnquiryText]
			   ,[OpenDate]
			   ,[CreatedDate]
			   ,[Active])
		 VALUES
			   (@fullName
			   ,@onlineAdId
			   ,@email
			   ,@phone
			   ,@enquiryText
			   ,@openDate
			   ,@createdDate
			   ,@active)

	SELECT	@enquiryId = @@IDENTITY;

END
GO
