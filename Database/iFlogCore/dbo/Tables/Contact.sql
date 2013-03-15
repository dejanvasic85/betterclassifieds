CREATE TABLE [dbo].[Contact] (
    [ContactId] INT           IDENTITY (1, 1) NOT NULL,
    [Firstname] NVARCHAR (50) COLLATE Latin1_General_CI_AS NULL,
    [Lastname]  NVARCHAR (50) COLLATE Latin1_General_CI_AS NULL,
    [Prefix]    NVARCHAR (10) COLLATE Latin1_General_CI_AS NULL,
    [Email]     NVARCHAR (50) COLLATE Latin1_General_CI_AS NULL,
    [Phone]     NVARCHAR (12) COLLATE Latin1_General_CI_AS NULL,
    [Mobile]    NVARCHAR (12) COLLATE Latin1_General_CI_AS NULL,
    CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED ([ContactId] ASC)
);

