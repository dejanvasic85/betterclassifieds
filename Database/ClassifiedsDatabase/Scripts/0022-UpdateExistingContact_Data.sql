
UPDATE	OnlineAd
SET		ContactEmail = ContactValue
FROM	OnlineAd
WHERE	ContactType = 'Email' and ContactValue != '' and ContactValue is not null and ContactValue like '%@%'

UPDATE	OnlineAd
SET		ContactPhone = ContactValue
FROM	OnlineAd
WHERE	ContactType = 'Phone' and ContactValue != '' and ContactValue is not null and ContactValue not like '%@%'
