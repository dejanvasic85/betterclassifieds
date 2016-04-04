
/*
 * App Settings
 */ 
create procedure temp_createAppSetting
	@Key		varchar(50),
	@Setting	varchar(max),
	@Force	bit = 0
AS
BEGIN
	IF NOT EXISTS ( SELECT * FROM AppSetting WHERE AppKey = @Key)
	begin
		INSERT INTO AppSetting (Module, AppKey, DataType, SettingValue)
		VALUES	('System', @Key, 'string', @Setting);
	end
	ELSE IF @Force = 1
	begin
		UPDATE	AppSetting
		SET		SettingValue = @Setting
		WHERE	AppKey = @Key
	end
END
GO

execute temp_createAppSetting @Key = 'SearchResultsPerPage', @Setting = '10'
execute temp_createAppSetting @Key = 'SearchMaxPagedRequests', @Setting = '100'
execute temp_createAppSetting @Key = 'MaxOnlineImages', @Setting = '10'
execute temp_createAppSetting @Key = 'SupportNotificationAccounts', @Setting = 'support@srilankanevents.com.au'
execute temp_createAppSetting @Key = 'AdDurationDays', @Setting = '730'
execute temp_createAppSetting @Key = 'ClientAddress', @Setting = 'Melbourne,VIC,3000,Australia', @Force = 1
execute temp_createAppSetting @Key = 'ClientAddressLatLong', @Setting = '37.8136,144.9631', @Force = 1
execute temp_createAppSetting @Key = 'EnableTwoFactorAuth', @Setting = 'true', @Force = 1
execute temp_createAppSetting @Key = 'ClientName', @Setting = 'Sri Lankan Events', @Force = 1
execute temp_createAppSetting @Key = 'EventTicketReservationExpiryMinutes', @Setting = '10'
execute temp_createAppSetting @Key = 'EventMaxTicketsPerBooking', @Setting = '5'
execute temp_createAppSetting @Key = 'EventTicketFee', @Setting = '3.6', @Force = 1
execute temp_createAppSetting @Key = 'FacebookAppId', @Setting = '950300948385613', @Force = 1


drop procedure temp_createAppSetting


GO

/*
 *	Categories
 */
create procedure temp_createCategory
	@Title varchar(50),
	@ParentCategory varchar(50) = null,
	@CategoryAdType varchar(50) = null,
	@IsOnlineOnly	bit	= null,
	@FontIcon varchar(30) = null,
	@MainCategoryId	int = null output
as
begin
	declare @ParentCategoryId int = null
	if @ParentCategory IS NOT NULL
	begin
		select @ParentCategoryId = MainCategoryId
		from MainCategory
		where Title = @ParentCategory
	end

	if not exists (select top 1 * from MainCategory where Title = @Title)
	begin
		insert into MainCategory (Title, ParentId, CategoryAdType, IsOnlineOnly)
		values	(@Title, @ParentCategoryId, @CategoryAdType, @IsOnlineOnly)

		set @MainCategoryId = @@IDENTITY
	end
	else
	begin
		update MainCategory
		set CategoryAdType = @CategoryAdType,
			IsOnlineOnly = @IsOnlineOnly,
			FontIcon = @FontIcon
		where Title = @Title
	end

end
go

-- Setup the new icons for the categories
exec temp_createCategory @Title = 'Concerts', @FontIcon = 'music', @CategoryAdType = 'Event', @IsOnlineOnly = 1;
exec temp_createCategory @ParentCategory = 'Concerts', @Title = 'DJ', @CategoryAdType = 'Event', @IsOnlineOnly = 1;
exec temp_createCategory @ParentCategory = 'Concerts', @Title = 'Traditional', @CategoryAdType = 'Event', @IsOnlineOnly = 1;
exec temp_createCategory @ParentCategory = 'Concerts', @Title = 'Contemporary', @CategoryAdType = 'Event', @IsOnlineOnly = 1;
exec temp_createCategory @ParentCategory = 'Concerts', @Title = 'Modern', @CategoryAdType = 'Event', @IsOnlineOnly = 1;
exec temp_createCategory @ParentCategory = 'Concerts', @Title = 'Baila', @CategoryAdType = 'Event', @IsOnlineOnly = 1;

exec temp_createCategory @Title = 'Dinner Dance', @FontIcon = 'cutlery', @CategoryAdType = 'Event', @IsOnlineOnly = 1;
exec temp_createCategory @ParentCategory = 'Dinner Dance', @Title = 'Hoppers Night', @CategoryAdType = 'Event', @IsOnlineOnly = 1;
exec temp_createCategory @ParentCategory = 'Dinner Dance', @Title = 'Kotthu Night', @CategoryAdType = 'Event', @IsOnlineOnly = 1;

exec temp_createCategory @Title = 'Expo', @FontIcon = 'suitcase', @CategoryAdType = 'Event', @IsOnlineOnly = 1;
exec temp_createCategory @ParentCategory = 'Expo', @Title = 'Business', @CategoryAdType = 'Event', @IsOnlineOnly = 1;
exec temp_createCategory @ParentCategory = 'Expo', @Title = 'Fashion', @CategoryAdType = 'Event', @IsOnlineOnly = 1;
exec temp_createCategory @ParentCategory = 'Expo', @Title = 'Education', @CategoryAdType = 'Event', @IsOnlineOnly = 1;
exec temp_createCategory @ParentCategory = 'Expo', @Title = 'Music', @CategoryAdType = 'Event', @IsOnlineOnly = 1;
exec temp_createCategory @ParentCategory = 'Expo', @Title = 'Food', @CategoryAdType = 'Event', @IsOnlineOnly = 1;
exec temp_createCategory @ParentCategory = 'Expo', @Title = 'Wedding', @CategoryAdType = 'Event', @IsOnlineOnly = 1;

exec temp_createCategory @Title = 'Shows', @FontIcon = 'video-camera', @CategoryAdType = 'Event', @IsOnlineOnly = 1;
exec temp_createCategory @ParentCategory = 'Shows', @Title = 'Drama', @CategoryAdType = 'Event', @IsOnlineOnly = 1;
exec temp_createCategory @ParentCategory = 'Shows', @Title = 'Movie', @CategoryAdType = 'Event', @IsOnlineOnly = 1;
exec temp_createCategory @ParentCategory = 'Shows', @Title = 'Talk Show', @CategoryAdType = 'Event', @IsOnlineOnly = 1;
exec temp_createCategory @ParentCategory = 'Shows', @Title = 'Dance', @CategoryAdType = 'Event', @IsOnlineOnly = 1;


go
drop procedure temp_createCategory
