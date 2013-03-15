CREATE TABLE [dbo].[ContentResource] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [EntityGroup] NVARCHAR (256) NOT NULL,
    [CultureCode] NVARCHAR (10)  NOT NULL,
    [EntityKey]   NVARCHAR (128) NOT NULL,
    [EntityType]  NVARCHAR (128) NOT NULL,
    [EntityValue] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_WebContent] PRIMARY KEY CLUSTERED ([Id] ASC)
);

