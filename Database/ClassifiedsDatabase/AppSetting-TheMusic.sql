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
execute temp_createAppSetting @Key = 'AdDurationDays', @Setting = '60'


drop procedure temp_createAppSetting