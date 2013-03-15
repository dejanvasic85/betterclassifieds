CREATE TYPE [dbo].[BannerRefrenceType] AS TABLE (
    [BannerReferenceTypeId] INT              NULL,
    [BannerId]              UNIQUEIDENTIFIER NULL,
    [Value]                 VARCHAR (255)    COLLATE Latin1_General_CI_AS NULL);

