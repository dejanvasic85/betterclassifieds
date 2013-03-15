CREATE TABLE [dbo].[LocationArea] (
    [LocationAreaId] INT            IDENTITY (1, 1) NOT NULL,
    [LocationId]     INT            NOT NULL,
    [Title]          NVARCHAR (50)  NULL,
    [Description]    NVARCHAR (255) NULL,
    CONSTRAINT [PK_LocationArea] PRIMARY KEY CLUSTERED ([LocationAreaId] ASC),
    CONSTRAINT [FK_LocationArea_Location] FOREIGN KEY ([LocationId]) REFERENCES [dbo].[Location] ([LocationId])
);

