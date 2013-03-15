CREATE TYPE [dbo].[NameValueCollectionType] AS TABLE (
    [Name]  VARCHAR (50)  COLLATE Latin1_General_CI_AS NOT NULL,
    [Value] VARCHAR (255) COLLATE Latin1_General_CI_AS NULL);

