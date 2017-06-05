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
execute temp_createAppSetting @Key = 'SupportNotificationAccounts', @Setting = 'support@paramountit.com.au'
execute temp_createAppSetting @Key = 'AdDurationDays', @Setting = '30'
execute temp_createAppSetting @Key = 'ClientAddress', @Setting = 'Level 1 221,Kerr Street,Fitzroy,VIC,3068,Australia', @Force = 1
execute temp_createAppSetting @Key = 'ClientAddressLatLong', @Setting = '-37.796973, 144.984072', @Force = 1
execute temp_createAppSetting @Key = 'ClientPhoneNumber', @Setting = '61 3 9421 4499', @Force = 1
execute temp_createAppSetting @Key = 'PublisherHomeUrl', @Setting = 'http://beat.com.au'
execute temp_createAppSetting @Key = 'FacebookAppId', @Setting = '143183162513319'
execute temp_createAppSetting @Key = 'Security.EnableRegistrationEmailVerification', @Setting = 'false', @Force = 1
execute temp_createAppSetting @Key = 'ClientName', @Setting = 'Beat', @Force = 1
execute temp_createAppSetting @Key = 'EventTicketReservationExpiryMinutes', @Setting = '10'
execute temp_createAppSetting @Key = 'EventMaxTicketsPerBooking', @Setting = '10'
execute temp_createAppSetting @Key = 'EventTicketFee', @Setting = '3.4', @Force = 1
execute temp_createAppSetting @Key = 'EventTicketFeeCents', @Setting = '30', @Force = 1
execute temp_createAppSetting @Key = 'IsPrintEnabled', @Setting = 'False', @Force = 1
execute temp_createAppSetting @Key = 'Events.EnablePayPalPayments', @Setting = 'True', @Force = 1
execute temp_createAppSetting @Key = 'Events.EnableCreditCardPayments', @Setting = 'True', @Force = 1
execute temp_createAppSetting @Key = 'Email.FromAddress', @Setting = 'Beat <no-reply@beat.com.au>', @Force=1

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



-- Category : Music
exec temp_createCategory @Title = 'Music', @FontIcon = 'music', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Concert', @ParentCategory = 'Music', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Opera', @ParentCategory = 'Music', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Tour', @ParentCategory = 'Music', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Party', @ParentCategory = 'Music', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Festival', @ParentCategory = 'Music', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Class', @ParentCategory = 'Music', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Convention', @ParentCategory = 'Music', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Expo', @ParentCategory = 'Music', @IsOnlineOnly = 1, @CategoryAdType = 'Event';

-- Category : Film & Media
exec temp_createCategory @Title = 'Film & Media', @FontIcon = 'film', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Convention', @ParentCategory = 'Film & Media', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Seminar', @ParentCategory = 'Film & Media', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Screening', @ParentCategory = 'Film & Media', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Appearance', @ParentCategory = 'Film & Media', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Networking', @ParentCategory = 'Film & Media', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Party', @ParentCategory = 'Film & Media', @IsOnlineOnly = 1, @CategoryAdType = 'Event';

-- Category : Business
exec temp_createCategory @Title = 'Business', @FontIcon = 'suitcase', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Conference', @ParentCategory = 'Business', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Convention', @ParentCategory = 'Business', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Seminar', @ParentCategory = 'Business', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Training', @ParentCategory = 'Business', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Party', @ParentCategory = 'Business', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Meetup', @ParentCategory = 'Business', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Expo', @ParentCategory = 'Business', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Networking', @ParentCategory = 'Business', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Other', @ParentCategory = 'Business', @IsOnlineOnly = 1, @CategoryAdType = 'Event';


-- Category : Food & Drink
exec temp_createCategory @Title = 'Food & Drink', @FontIcon = 'cutlery', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Festival', @ParentCategory = 'Food & Drink', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Networking', @ParentCategory = 'Food & Drink', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Gala', @ParentCategory = 'Food & Drink', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Seminar', @ParentCategory = 'Food & Drink', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Class', @ParentCategory = 'Food & Drink', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Party', @ParentCategory = 'Food & Drink', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Other', @ParentCategory = 'Food & Drink', @IsOnlineOnly = 1, @CategoryAdType = 'Event';

-- Category : Community & Culture
exec temp_createCategory @Title = 'Community & Culture', @FontIcon = 'users', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Festival', @ParentCategory = 'Community & Culture', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Party', @ParentCategory = 'Community & Culture', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'National Day', @ParentCategory = 'Community & Culture', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Fund Raiser', @ParentCategory = 'Community & Culture', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Class', @ParentCategory = 'Community & Culture', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Performance', @ParentCategory = 'Community & Culture', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Dinner', @ParentCategory = 'Community & Culture', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Other', @ParentCategory = 'Community & Culture', @IsOnlineOnly = 1, @CategoryAdType = 'Event';

-- Category: Sports & Fitness
exec temp_createCategory @Title = 'Sports & Fitness', @FontIcon = 'futbol-o', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Class', @ParentCategory = 'Sports & Fitness', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Race', @ParentCategory = 'Sports & Fitness', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Game', @ParentCategory = 'Sports & Fitness', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Match', @ParentCategory = 'Sports & Fitness', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Tournament', @ParentCategory = 'Sports & Fitness', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Training', @ParentCategory = 'Sports & Fitness', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Networking', @ParentCategory = 'Sports & Fitness', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Other', @ParentCategory = 'Sports & Fitness', @IsOnlineOnly = 1, @CategoryAdType = 'Event';

-- Category: Education
exec temp_createCategory @Title = 'Education', @FontIcon = 'book', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Party', @ParentCategory = 'Education', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Ball', @ParentCategory = 'Education', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Orientation Week', @ParentCategory = 'Education', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Expo', @ParentCategory = 'Education', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Conference', @ParentCategory = 'Education', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Seminar', @ParentCategory = 'Education', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Tutoring', @ParentCategory = 'Education', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Tour', @ParentCategory = 'Education', @IsOnlineOnly = 1, @CategoryAdType = 'Event';
exec temp_createCategory @Title = 'Other', @ParentCategory = 'Education', @IsOnlineOnly = 1, @CategoryAdType = 'Event';


drop procedure temp_createCategory