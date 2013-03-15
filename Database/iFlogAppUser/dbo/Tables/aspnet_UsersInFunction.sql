CREATE TABLE [dbo].[aspnet_UsersInFunction] (
    [UserId]     UNIQUEIDENTIFIER NOT NULL,
    [FunctionId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_aspnet_UsersInFunction] PRIMARY KEY CLUSTERED ([UserId] ASC, [FunctionId] ASC),
    CONSTRAINT [FK_aspnet_UsersInFunction_aspnet_Function] FOREIGN KEY ([FunctionId]) REFERENCES [dbo].[aspnet_Function] ([FunctionId]),
    CONSTRAINT [FK_aspnet_UsersInFunction_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId])
);

