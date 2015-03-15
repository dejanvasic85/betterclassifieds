-- Drop the columns
ALTER TABLE AdBookingOrderItem
DROP COLUMN [ItemName]

ALTER TABLE AdBookingOrderItem
DROP COLUMN [Amount]

-- Add the columns back with the proper name
ALTER TABLE AdBookingOrderItem
ADD [Name] VARCHAR(20)

ALTER TABLE AdBookingOrderItem
ADD [Price] MONEY



-- New column
ALTER TABLE AdBookingOrderItem
ADD [Editions] INT