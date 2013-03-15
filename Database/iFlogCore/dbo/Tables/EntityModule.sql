CREATE TABLE [dbo].[EntityModule] (
    [EntityModuleId] INT           IDENTITY (1, 1) NOT NULL,
    [EntityCode]     NVARCHAR (10) COLLATE Latin1_General_CI_AS NOT NULL,
    [ModuleId]       INT           NOT NULL,
    [Active]         BIT           NOT NULL,
    [StartDate]      DATETIME      NULL,
    [EndDate]        DATETIME      NULL,
    CONSTRAINT [PK_Subscription] PRIMARY KEY CLUSTERED ([EntityModuleId] ASC),
    CONSTRAINT [FK_Subscription_Entity] FOREIGN KEY ([EntityCode]) REFERENCES [dbo].[Entity] ([EntityCode]),
    CONSTRAINT [FK_Subscription_Module] FOREIGN KEY ([ModuleId]) REFERENCES [dbo].[Module] ([ModuleId])
);

