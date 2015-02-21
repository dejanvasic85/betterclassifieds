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
execute temp_createAppSetting @Key = 'ClientAddress', @Setting = 'Street Press Australia Pty Ltd,Level 1, 221 Kerr Street,Fitzroy,VIC,3068,Australia', @Force = 1
execute temp_createAppSetting @Key = 'ClientAddressLatLong', @Setting = '-37.796973, 144.984072', @Force = 1
execute temp_createAppSetting @Key = 'ClientPhoneNumber', @Setting = '61 3 9421 4499', @Force = 1
execute temp_createAppSetting @Key = 'PublisherHomeUrl', @Setting = 'http://themusic.com.au'
execute temp_createAppSetting @Key = 'FacebookAppId', @Setting = '143183162513319'
execute temp_createAppSetting @Key = 'EnableTwoFactorAuth', @Setting = 'true', @Force = 1
execute temp_createAppSetting @Key = 'PrintImagePixelsHeight', @Setting = '330'
execute temp_createAppSetting @Key = 'PrintImagePixelsWidth', @Setting = '354'
execute temp_createAppSetting @Key = 'PrintImageResolution', @Setting = '300'


drop procedure temp_createAppSetting