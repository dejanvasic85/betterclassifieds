create procedure temp_createAppSetting
	@Key		varchar(50),
	@Setting	varchar(max)
AS
BEGIN
	IF NOT EXISTS ( SELECT * FROM AppSetting WHERE AppKey = @Key)
	begin
		INSERT INTO AppSetting (Module, AppKey, DataType, SettingValue)
		VALUES	('System', @Key, 'string', @Setting);
	end
END
GO

execute temp_createAppSetting @Key = 'NumberOfDaysAfterLastEdition', @Setting = '6'
execute temp_createAppSetting @Key = 'SearchResultsPerPage', @Setting = '10'
execute temp_createAppSetting @Key = 'SearchMaxPagedRequests', @Setting = '100'

drop procedure temp_createAppSetting