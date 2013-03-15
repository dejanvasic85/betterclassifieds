CREATE TABLE [dbo].[Region] (
    [RegionId]  INT            IDENTITY (1, 1) NOT NULL,
    [Title]     NVARCHAR (30)  COLLATE Latin1_General_CI_AS NULL,
    [StateCode] NVARCHAR (5)   COLLATE Latin1_General_CI_AS CONSTRAINT [DF_Region_StateCode] DEFAULT (N'Other') NOT NULL,
    [TimeZone]  NVARCHAR (200) COLLATE Latin1_General_CI_AS NULL,
    CONSTRAINT [PK_Region_1] PRIMARY KEY CLUSTERED ([RegionId] ASC)
);

