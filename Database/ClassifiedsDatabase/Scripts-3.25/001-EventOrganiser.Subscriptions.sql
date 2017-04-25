GO 

ALTER TABLE EventOrganiser
ADD SubscribeToPurchaseNotifications BIT NULL;

ALTER TABLE EventOrganiser
ADD SubscribeToDailyNotifications BIT NULL;

GO

UPDATE dbo.EventOrganiser
SET SubscribeToPurchaseNotifications = 1,
	SubscribeToDailyNotifications = 1;