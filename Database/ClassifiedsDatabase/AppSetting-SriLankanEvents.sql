
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
execute temp_createAppSetting @Key = 'SupportNotificationAccounts', @Setting = 'support@paramountit.com.au'
execute temp_createAppSetting @Key = 'AdDurationDays', @Setting = '60'
execute temp_createAppSetting @Key = 'ClientAddress', @Setting = 'PO Box 333, Lennox St,Richmond,VIC,3121,Australia', @Force = 1
execute temp_createAppSetting @Key = 'ClientAddressLatLong', @Setting = '-37.818635,145.001470', @Force = 1
execute temp_createAppSetting @Key = 'EnableTwoFactorAuth', @Setting = 'true', @Force = 1
execute temp_createAppSetting @Key = 'ClientName', @Setting = 'Sri Lankan Events', @Force = 1
execute temp_createAppSetting @Key = 'EventTicketReservationExpiryMinutes', @Setting = '5'
execute temp_createAppSetting @Key = 'EventMaxTicketsPerBooking', @Setting = '5'
execute temp_createAppSetting @Key = 'EventTicketFee', @Setting = '2.9', @Force = 1
execute temp_createAppSetting @Key = 'FacebookAppId', @Setting = '1277927518889183', @Force = 1


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
			IsOnlineOnly = @IsOnlineOnly
		where Title = @Title
	end

end
go

-- Setup the new icons for the categories
-- exec temp_createCategory @Title = 'For Sale', @FontIcon = 'shopping-cart';
go
drop procedure temp_createCategory
