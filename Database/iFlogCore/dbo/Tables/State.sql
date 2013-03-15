CREATE TABLE [dbo].[State] (
    [StateId]     INT            IDENTITY (1, 1) NOT NULL,
    [StateCode]   NVARCHAR (5)   COLLATE Latin1_General_CI_AS NOT NULL,
    [Title]       NVARCHAR (50)  COLLATE Latin1_General_CI_AS NOT NULL,
    [Description] NVARCHAR (100) COLLATE Latin1_General_CI_AS NULL,
    CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED ([StateCode] ASC)
);

