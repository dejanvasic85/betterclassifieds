CREATE TABLE [dbo].[Lookup] (
    [LookupId]    BIGINT         IDENTITY (1, 1) NOT NULL,
    [GroupName]   NVARCHAR (50)  NOT NULL,
    [LookupValue] NVARCHAR (200) NOT NULL,
    CONSTRAINT [PK_Lookup] PRIMARY KEY CLUSTERED ([LookupId] ASC)
);

