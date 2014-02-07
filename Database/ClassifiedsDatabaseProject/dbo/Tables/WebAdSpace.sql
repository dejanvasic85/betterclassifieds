CREATE TABLE [dbo].[WebAdSpace] (
    [WebAdSpaceId] INT            IDENTITY (1, 1) NOT NULL,
    [Title]        NVARCHAR (100) COLLATE Latin1_General_CI_AS NULL,
    [SettingID]    INT            NULL,
    [SortOrder]    INT            NULL,
    [AdLinkUrl]    NVARCHAR (255) COLLATE Latin1_General_CI_AS NULL,
    [AdTarget]     NVARCHAR (50)  COLLATE Latin1_General_CI_AS NULL,
    [SpaceType]    INT            NULL,
    [ImageUrl]     NVARCHAR (255) COLLATE Latin1_General_CI_AS NULL,
    [DisplayText]  NVARCHAR (100) COLLATE Latin1_General_CI_AS NULL,
    [ToolTipText]  NVARCHAR (200) COLLATE Latin1_General_CI_AS NULL,
    [Active]       BIT            NULL,
    CONSTRAINT [PK_WebAdSpace] PRIMARY KEY CLUSTERED ([WebAdSpaceId] ASC),
    CONSTRAINT [FK_WebAdSpace_WebAdSpaceSetting] FOREIGN KEY ([SettingID]) REFERENCES [dbo].[WebAdSpaceSetting] ([SettingId])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The number value of the order in which to display on the web.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebAdSpace', @level2type = N'COLUMN', @level2name = N'SortOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'The url that points to where the click event should take the user.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebAdSpace', @level2type = N'COLUMN', @level2name = N'AdLinkUrl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'_blank, _parent, _search, _self, _top', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebAdSpace', @level2type = N'COLUMN', @level2name = N'AdTarget';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'1=Image, 2=Text', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebAdSpace', @level2type = N'COLUMN', @level2name = N'SpaceType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'WebAdSpace', @level2type = N'COLUMN', @level2name = N'ImageUrl';

