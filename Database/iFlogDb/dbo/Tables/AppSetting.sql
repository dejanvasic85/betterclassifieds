CREATE TABLE [dbo].[AppSetting] (
    [Module]       NVARCHAR (10)  NOT NULL,
    [AppKey]       NVARCHAR (100) NOT NULL,
    [DataType]     NVARCHAR (50)  NULL,
    [SettingValue] NVARCHAR (MAX) NULL,
    [Description]  NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_AppSetting_1] PRIMARY KEY CLUSTERED ([Module] ASC, [AppKey] ASC)
);

