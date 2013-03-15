CREATE TABLE [dbo].[aspnet_Function] (
    [FunctionId]          UNIQUEIDENTIFIER NOT NULL,
    [FunctionName]        NVARCHAR (256)   NOT NULL,
    [LoweredFunctionName] NVARCHAR (256)   NOT NULL,
    [Description]         NVARCHAR (256)   NULL,
    [RoleId]              UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_aspnet_Function] PRIMARY KEY CLUSTERED ([FunctionId] ASC),
    CONSTRAINT [aspnet_Function_aspnet_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[aspnet_Roles] ([RoleId])
);

