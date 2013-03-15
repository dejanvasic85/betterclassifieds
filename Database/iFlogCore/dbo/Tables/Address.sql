CREATE TABLE [dbo].[Address] (
    [AddressId]   INT            NOT NULL,
    [Street]      NVARCHAR (100) COLLATE Latin1_General_CI_AS NULL,
    [Suburb]      NVARCHAR (100) COLLATE Latin1_General_CI_AS NULL,
    [City]        NVARCHAR (100) COLLATE Latin1_General_CI_AS NULL,
    [Province]    NVARCHAR (100) COLLATE Latin1_General_CI_AS NULL,
    [PostCode]    NVARCHAR (5)   COLLATE Latin1_General_CI_AS NULL,
    [RegionId]    INT            NULL,
    [CountryCode] NVARCHAR (4)   COLLATE Latin1_General_CI_AS NULL,
    CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED ([AddressId] ASC)
);

