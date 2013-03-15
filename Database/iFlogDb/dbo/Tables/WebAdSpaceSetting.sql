CREATE TABLE [dbo].[WebAdSpaceSetting] (
    [SettingId]    INT            IDENTITY (1, 1) NOT NULL,
    [Title]        NVARCHAR (100) COLLATE Latin1_General_CI_AS NULL,
    [LocationCode] INT            NOT NULL,
    CONSTRAINT [PK_WebAdSpaceSetting] PRIMARY KEY CLUSTERED ([SettingId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1=Top Banner, 2=Footer, 3=Right Column', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebAdSpaceSetting', @level2type = N'COLUMN', @level2name = N'LocationCode';

