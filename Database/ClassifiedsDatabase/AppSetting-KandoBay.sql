
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
execute temp_createAppSetting @Key = 'MaxOnlineImages', @Setting = '5'
execute temp_createAppSetting @Key = 'SupportNotificationAccounts', @Setting = 'support@paramountit.com.au'
execute temp_createAppSetting @Key = 'AdDurationDays', @Setting = '60'
execute temp_createAppSetting @Key = 'ClientAddress', @Setting = 'Uche Njoku,PO Box 333,Richmond,VIC,3121,Australia', @Force = 1
execute temp_createAppSetting @Key = 'ClientAddressLatLong', @Setting = '-37.818635,145.001470', @Force = 1
execute temp_createAppSetting @Key = 'EnableTwoFactorAuth', @Setting = 'true', @Force = 1
execute temp_createAppSetting @Key = 'ClientName', @Setting = 'KandoBay', @Force = 1

drop procedure temp_createAppSetting


GO

/*
 *	Categories
 */
create procedure temp_createParentCategory
	@Title varchar(50),
	@ParentCategoryId int = null,
	@CategoryAdType varchar(50) = null,
	@IsOnlineOnly	bit	= null,
	@MainCategoryId	int = null output
as
begin
	if not exists (select top 1 * from MainCategory where Title = @Title)
	begin
		insert into MainCategory (Title, ParentId, CategoryAdType, IsOnlineOnly)
		values	(@Title, @ParentCategoryId, @CategoryAdType, @IsOnlineOnly)

		set @MainCategoryId = @@IDENTITY
	end
	else if @CategoryAdType IS NOT NULL
	begin
		update MainCategory
		set CategoryAdType = @CategoryAdType,
			IsOnlineOnly = @IsOnlineOnly
		where Title = @Title
	end

end
go
exec temp_createParentCategory @Title = 'Events', @CategoryAdType = 'Event', @IsOnlineOnly = 1;
exec temp_createParentCategory @Title = 'Concerts', @CategoryAdType = 'Event', @IsOnlineOnly = 1;
exec temp_createParentCategory @Title = 'Event Services', @CategoryAdType = 'Event', @IsOnlineOnly = 1;
exec temp_createParentCategory @Title = 'Sports', @CategoryAdType = 'Event', @IsOnlineOnly = 1;
go
drop procedure temp_createParentCategory
