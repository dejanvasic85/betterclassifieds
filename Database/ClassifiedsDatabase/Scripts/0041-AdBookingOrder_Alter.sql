-- Drop the columns
ALTER TABLE AdBookingOrder
DROP COLUMN [BookReference]

ALTER TABLE AdBookingOrder
DROP COLUMN [OrderName]

-- Add the columns back with the proper name
ALTER TABLE AdBookingOrder
ADD [Reference] VARCHAR(20)

ALTER TABLE AdBookingOrder
ADD [Name] VARCHAR(20)

ALTER TABLE AdBookingOrder
ADD [CreateDateUtc] DATETIME