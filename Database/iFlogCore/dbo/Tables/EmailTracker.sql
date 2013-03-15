CREATE TABLE [dbo].[EmailTracker] (
    [EmailTrackerId]        INT              IDENTITY (1, 1) NOT NULL,
    [EmailBroadcastEntryId] UNIQUEIDENTIFIER NOT NULL,
    [Page]                  NVARCHAR (MAX)   COLLATE Latin1_General_CI_AS NULL,
    [IpAddress]             NVARCHAR (50)    COLLATE Latin1_General_CI_AS NULL,
    [Browser]               NVARCHAR (50)    COLLATE Latin1_General_CI_AS NULL,
    [DateTime]              DATETIME         NOT NULL,
    [Country]               NVARCHAR (150)   COLLATE Latin1_General_CI_AS NULL,
    [Region]                NVARCHAR (150)   COLLATE Latin1_General_CI_AS NULL,
    [City]                  NVARCHAR (150)   COLLATE Latin1_General_CI_AS NULL,
    [Postcode]              NVARCHAR (50)    COLLATE Latin1_General_CI_AS NULL,
    [Latitude]              NVARCHAR (50)    COLLATE Latin1_General_CI_AS NULL,
    [Longitude]             NVARCHAR (50)    COLLATE Latin1_General_CI_AS NULL,
    [TimeZone]              NCHAR (10)       COLLATE Latin1_General_CI_AS NULL,
    CONSTRAINT [PK_EmailTracker] PRIMARY KEY CLUSTERED ([EmailTrackerId] ASC)
);

