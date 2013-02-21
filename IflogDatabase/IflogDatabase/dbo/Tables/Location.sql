CREATE TABLE [dbo].[Location] (
    [LocationId]  INT           IDENTITY (1, 1) NOT NULL,
    [Title]       NVARCHAR (50) NULL,
    [Description] NVARCHAR (50) NULL,
    CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED ([LocationId] ASC)
);

