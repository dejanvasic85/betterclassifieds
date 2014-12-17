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
execute temp_createAppSetting @Key = 'SupportNotificationAccounts', @Setting = 'support@paramountit.com.au;'
execute temp_createAppSetting @Key = 'AdDurationDays', @Setting = '60'
execute temp_createAppSetting @Key = 'ClientAddress', @Setting = 'Uche Njoku,PO Box 333,Richmond,VIC,3121,Australia', @Force = 1
execute temp_createAppSetting @Key = 'ClientAddressLatLong', @Setting = '-37.818635,145.001470', @Force = 1
execute temp_createAppSetting @Key = 'EnableTwoFactorAuth', @Setting = 'true', @Force = 1

drop procedure temp_createAppSetting