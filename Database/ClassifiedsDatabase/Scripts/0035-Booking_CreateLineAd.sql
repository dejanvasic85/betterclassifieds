GO
/****** Object:  StoredProcedure [dbo].[Booking_Create]    Script Date: 22/02/2015 7:04:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LineAd_Create]
	@AdBookingId		INT,
	@AdHeader			NVARCHAR(255),
	@AdText				NVARCHAR(MAX),
	@NumOfWords			INT,
	@UsePhoto			BIT,
	@UseBoldHeader		BIT,
	@IsColourBoldHeading BIT,
	@IsColourBorder		BIT,
	@IsColourBackground BIT,
	@IsSuperBoldHeading BIT,
    @BoldHeadingColourCode varchar(10),
	@BorderColourCode varchar(10),
    @BackgroundColourCode varchar(10),
    @IsSuperHeadingPurchased BIT,
	@lineAdId			INT = NULL OUTPUT
AS
BEGIN TRANSACTION 
	
	declare	@adId INT;
	declare @lineAdTypeId INT;
	declare @lineAdDesignId INT;
	declare @bookedStatusId INT = 1; -- Booked/Completed
	declare @regularBookingType VARCHAR(20) = 'Regular';

	SELECT	@adId = bk.AdId
	FROM	AdBooking bk
	WHERE	bk.AdBookingId = @AdBookingId

	SELECT  @lineAdTypeId = at.AdTypeId FROM AdType at WHERE at.Code = 'LINE';
	
	INSERT INTO [dbo].[AdDesign] ([AdId] ,[AdTypeId])
	VALUES (@adId,@lineAdTypeId)

	set	@lineAdDesignId = @@IDENTITY;
	
	-- Create the line ad
	INSERT INTO [dbo].[LineAd]
           ([AdDesignId]
           ,[AdHeader]
           ,[AdText]
           ,[NumOfWords]
           ,[UsePhoto]
           ,[UseBoldHeader]
           ,[IsColourBoldHeading]
           ,[IsColourBorder]
           ,[IsColourBackground]
           ,[IsSuperBoldHeading]
           ,[BoldHeadingColourCode]
           ,[BorderColourCode]
           ,[BackgroundColourCode]
           ,[IsSuperHeadingPurchased])
     VALUES
           (@lineAdDesignId
           ,@AdHeader
           ,@AdText
           ,@NumOfWords
           ,@UsePhoto
           ,@UseBoldHeader
           ,@IsColourBoldHeading
           ,@IsColourBorder
           ,@IsColourBackground
           ,@IsSuperBoldHeading
           ,@BoldHeadingColourCode
           ,@BorderColourCode
           ,@BackgroundColourCode
           ,@IsSuperHeadingPurchased)



	set	@lineAdId = @@IDENTITY;

COMMIT