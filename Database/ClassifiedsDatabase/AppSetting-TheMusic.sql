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

-- These are production ready settings. Use force parameter if you need to ensure it overrides any existing setting
-- Please update the sanitisation script if required 
execute temp_createAppSetting @Key = 'NumberOfDaysAfterLastEdition', @Setting = '6'
execute temp_createAppSetting @Key = 'SearchResultsPerPage', @Setting = '10'
execute temp_createAppSetting @Key = 'SearchMaxPagedRequests', @Setting = '100'
execute temp_createAppSetting @Key = 'MaxOnlineImages', @Setting = '2'
execute temp_createAppSetting @Key = 'SupportNotificationAccounts', @Setting = 'leanne@timeoff.com.au;incoming@iflog.com.au'
execute temp_createAppSetting @Key = 'AdDurationDays', @Setting = '30'
execute temp_createAppSetting @Key = 'ClientAddress', @Setting = 'Level 1 221,Kerr Street,Fitzroy,VIC,3068,Australia', @Force = 1
execute temp_createAppSetting @Key = 'ClientAddressLatLong', @Setting = '-37.796973, 144.984072', @Force = 1
execute temp_createAppSetting @Key = 'ClientPhoneNumber', @Setting = '61 3 9421 4499', @Force = 1
execute temp_createAppSetting @Key = 'PublisherHomeUrl', @Setting = 'http://themusic.com.au'
execute temp_createAppSetting @Key = 'FacebookAppId', @Setting = '143183162513319'
execute temp_createAppSetting @Key = 'EnableTwoFactorAuth', @Setting = 'true', @Force = 1
execute temp_createAppSetting @Key = 'PrintImagePixelsHeight', @Setting = '330'
execute temp_createAppSetting @Key = 'PrintImagePixelsWidth', @Setting = '354'
execute temp_createAppSetting @Key = 'PrintImageResolution', @Setting = '300'
execute temp_createAppSetting @Key = 'ClientName', @Setting = 'TheMusic', @Force = 1
execute temp_createAppSetting @Key = 'EventTicketReservationExpiryMinutes', @Setting = '10'
execute temp_createAppSetting @Key = 'EventMaxTicketsPerBooking', @Setting = '5'
execute temp_createAppSetting @Key = 'EventTicketFee', @Setting = '2.1', @Force = 1
execute temp_createAppSetting @Key = 'EventTicketFeeCents', @Setting = '30', @Force = 1
execute temp_createAppSetting @Key = 'IsPrintEnabled', @Setting = 'False', @Force = 1
execute temp_createAppSetting @Key = 'Events.EnablePayPalPayments', @Setting = 'True', @Force = 1
execute temp_createAppSetting @Key = 'Events.EnableCreditCardPayments', @Setting = 'True', @Force = 1


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

exec temp_createCategory @Title = 'Employment', @FontIcon = 'cubes';
exec temp_createCategory @Title = 'Music Services', @FontIcon = 'music';
exec temp_createCategory @Title = 'Musicians Wanted', @FontIcon = 'microphone';
exec temp_createCategory @Title = 'Musicians Available', @FontIcon = 'users';
exec temp_createCategory @Title = 'For Sale', @FontIcon = 'shopping-cart';
exec temp_createCategory @Title = 'Wanted', @FontIcon = 'search';
exec temp_createCategory @Title = 'Services', @FontIcon = 'support';
exec temp_createCategory @Title = 'Share Accommodation', @FontIcon = 'home';
exec temp_createCategory @Title = 'Film & Stage', @FontIcon = 'film';
exec temp_createCategory @Title = 'Tuition', @FontIcon = 'mortar-board';

drop procedure temp_createCategory