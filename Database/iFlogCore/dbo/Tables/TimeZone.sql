CREATE TABLE [dbo].[TimeZone] (
    [TimeZoneCode]        NVARCHAR (5)    COLLATE Latin1_General_CI_AS NOT NULL,
    [TimeZoneDescription] NVARCHAR (50)   COLLATE Latin1_General_CI_AS NOT NULL,
    [CountryCode]         CHAR (2)        COLLATE Latin1_General_CI_AS NOT NULL,
    [TimeZoneUtc]         DECIMAL (18, 1) NOT NULL,
    CONSTRAINT [PK_TimeZone_1] PRIMARY KEY CLUSTERED ([TimeZoneCode] ASC, [CountryCode] ASC)
);

