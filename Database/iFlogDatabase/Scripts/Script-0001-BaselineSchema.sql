SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BaseRate]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BaseRate](
	[BaseRateId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[ImageUrl] [nvarchar](255) NULL,
	[UpgradeBaseRateId] [int] NULL,
 CONSTRAINT [PK_BaseRate] PRIMARY KEY CLUSTERED 
(
	[BaseRateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AppSetting]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AppSetting](
	[Module] [nvarchar](10) NOT NULL,
	[AppKey] [nvarchar](100) NOT NULL,
	[DataType] [nvarchar](50) NULL,
	[SettingValue] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_AppSetting_1] PRIMARY KEY CLUSTERED 
(
	[Module] ASC,
	[AppKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdType](
	[AdTypeId] [int] NOT NULL,
	[Code] [nvarchar](50) NULL,
	[Title] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[PaperBased] [bit] NULL,
	[ImageUrl] [nvarchar](255) NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_AdType] PRIMARY KEY CLUSTERED 
(
	[AdTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ad]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Ad](
	[AdId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Comments] [nvarchar](255) NULL,
	[UseAsTemplate] [bit] NULL,
 CONSTRAINT [PK_Ad] PRIMARY KEY CLUSTERED 
(
	[AdId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdBookingExtension]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdBookingExtension](
	[AdBookingExtensionId] [int] IDENTITY(1,1) NOT NULL,
	[AdBookingId] [int] NOT NULL,
	[Insertions] [int] NULL,
	[ExtensionPrice] [money] NULL,
	[Status] [int] NOT NULL,
	[LastModifiedUserId] [varchar](50) NULL,
	[LastModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_AdBookingExtension] PRIMARY KEY CLUSTERED 
(
	[AdBookingExtensionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Location]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Location](
	[LocationId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[LocationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LineAdTheme]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[LineAdTheme](
	[LineAdThemeId] [int] IDENTITY(1,1) NOT NULL,
	[ThemeName] [varchar](100) NULL,
	[Description] [varchar](max) NULL,
	[DescriptionHtml] [nvarchar](max) NULL,
	[ImageUrl] [varchar](500) NULL,
	[HeaderColourCode] [varchar](10) NULL,
	[HeaderColourName] [varchar](50) NULL,
	[BorderColourCode] [varchar](10) NULL,
	[BorderColourName] [varchar](50) NULL,
	[BackgroundColourCode] [varchar](10) NULL,
	[BackgroundColourName] [varchar](50) NULL,
	[IsHeadingSuperBold] [bit] NULL,
	[IsActive] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedByUser] [varchar](100) NULL,
 CONSTRAINT [PK_LineAdTheme] PRIMARY KEY CLUSTERED 
(
	[LineAdThemeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LineAdColourCode]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[LineAdColourCode](
	[LineAdColourId] [int] IDENTITY(1,1) NOT NULL,
	[LineAdColourName] [varchar](50) NOT NULL,
	[ColourCode] [varchar](10) NOT NULL,
	[Cyan] [int] NULL,
	[Magenta] [int] NULL,
	[Yellow] [int] NULL,
	[KeyCode] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[SortOrder] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedByUser] [varchar](100) NULL,
 CONSTRAINT [PK_LineAdColourCode] PRIMARY KEY CLUSTERED 
(
	[LineAdColourId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EnquiryType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EnquiryType](
	[EnquiryTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NULL,
	[Active] [bit] NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_EnquiryType] PRIMARY KEY CLUSTERED 
(
	[EnquiryTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PublicationType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PublicationType](
	[PublicationTypeId] [int] NOT NULL,
	[Code] [nchar](10) NULL,
	[Title] [nvarchar](50) NULL,
	[Description] [nvarchar](255) NULL,
 CONSTRAINT [PK_PublicationType] PRIMARY KEY CLUSTERED 
(
	[PublicationTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ScriptLog]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ScriptLog](
	[ScriptName] [varchar](300) NOT NULL,
	[RunDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ScriptLog] PRIMARY KEY CLUSTERED 
(
	[ScriptName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MainCategory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MainCategory](
	[MainCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[ImageUrl] [nvarchar](255) NULL,
	[ParentId] [int] NULL,
 CONSTRAINT [PK_MainCategory] PRIMARY KEY CLUSTERED 
(
	[MainCategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Transaction]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Transaction](
	[TransactionId] [int] IDENTITY(1,1) NOT NULL,
	[TransactionType] [int] NULL,
	[UserId] [nvarchar](50) NULL,
	[Title] [nvarchar](50) NULL,
	[Description] [nvarchar](255) NULL,
	[Amount] [money] NULL,
	[TransactionDate] [datetime] NULL,
	[RowTimeStamp] [timestamp] NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Transaction', N'COLUMN',N'TransactionType'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Refers to the enumeration "TransactionType" in the booking system.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Transaction', @level2type=N'COLUMN',@level2name=N'TransactionType'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempBookingRecord]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TempBookingRecord](
	[BookingRecordId] [uniqueidentifier] NOT NULL,
	[TotalCost] [money] NOT NULL,
	[SessionID] [varchar](25) NOT NULL,
	[UserId] [varchar](50) NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[AdReferenceId] [varchar](20) NOT NULL,
 CONSTRAINT [PK_TempBookingRecord] PRIMARY KEY CLUSTERED 
(
	[BookingRecordId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SupportEnquiry]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SupportEnquiry](
	[SupportEnquiryId] [int] IDENTITY(1,1) NOT NULL,
	[EnquiryTypeName] [varchar](50) NULL,
	[FullName] [varchar](100) NULL,
	[Email] [varchar](100) NULL,
	[Phone] [varchar](15) NULL,
	[Subject] [varchar](100) NULL,
	[EnquiryText] [varchar](max) NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_SupportEnquiry] PRIMARY KEY CLUSTERED 
(
	[SupportEnquiryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebContent]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebContent](
	[WebContentId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NULL,
	[PageId] [varchar](50) NOT NULL,
	[WebContent] [nvarchar](max) NULL,
	[LastModifiedDate] [datetime] NULL,
	[LastModifiedUser] [varchar](50) NULL,
 CONSTRAINT [PK_WebContent] PRIMARY KEY CLUSTERED 
(
	[WebContentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebAdSpaceSetting]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebAdSpaceSetting](
	[SettingId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NULL,
	[LocationCode] [int] NOT NULL,
 CONSTRAINT [PK_WebAdSpaceSetting] PRIMARY KEY CLUSTERED 
(
	[SettingId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'WebAdSpaceSetting', N'COLUMN',N'LocationCode'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1=Top Banner, 2=Footer, 3=Right Column' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WebAdSpaceSetting', @level2type=N'COLUMN',@level2name=N'LocationCode'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WebAdSpace]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[WebAdSpace](
	[WebAdSpaceId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NULL,
	[SettingID] [int] NULL,
	[SortOrder] [int] NULL,
	[AdLinkUrl] [nvarchar](255) NULL,
	[AdTarget] [nvarchar](50) NULL,
	[SpaceType] [int] NULL,
	[ImageUrl] [nvarchar](255) NULL,
	[DisplayText] [nvarchar](100) NULL,
	[ToolTipText] [nvarchar](200) NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_WebAdSpace] PRIMARY KEY CLUSTERED 
(
	[WebAdSpaceId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'WebAdSpace', N'COLUMN',N'SortOrder'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The number value of the order in which to display on the web.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WebAdSpace', @level2type=N'COLUMN',@level2name=N'SortOrder'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'WebAdSpace', N'COLUMN',N'AdLinkUrl'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The url that points to where the click event should take the user.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WebAdSpace', @level2type=N'COLUMN',@level2name=N'AdLinkUrl'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'WebAdSpace', N'COLUMN',N'AdTarget'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'_blank, _parent, _search, _self, _top' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WebAdSpace', @level2type=N'COLUMN',@level2name=N'AdTarget'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'WebAdSpace', N'COLUMN',N'SpaceType'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1=Image, 2=Text' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WebAdSpace', @level2type=N'COLUMN',@level2name=N'SpaceType'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'WebAdSpace', N'COLUMN',N'ImageUrl'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WebAdSpace', @level2type=N'COLUMN',@level2name=N'ImageUrl'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpecialRate]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SpecialRate](
	[SpecialRateId] [int] IDENTITY(1,1) NOT NULL,
	[BaseRateId] [int] NULL,
	[NumOfInsertions] [int] NULL,
	[MaximumWords] [int] NULL,
	[SetPrice] [money] NULL,
	[Discount] [numeric](18, 2) NULL,
	[NumOfAds] [int] NULL,
	[LineAdBoldHeader] [bit] NULL,
	[LineAdImage] [bit] NULL,
	[NumberOfImages] [int] NULL,
 CONSTRAINT [PK_SpecialRate] PRIMARY KEY CLUSTERED 
(
	[SpecialRateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LocationArea]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[LocationArea](
	[LocationAreaId] [int] IDENTITY(1,1) NOT NULL,
	[LocationId] [int] NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Description] [nvarchar](255) NULL,
 CONSTRAINT [PK_LocationArea] PRIMARY KEY CLUSTERED 
(
	[LocationAreaId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Publication]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Publication](
	[PublicationId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[PublicationTypeId] [int] NULL,
	[ImageUrl] [nvarchar](255) NULL,
	[FrequencyType] [nvarchar](20) NULL,
	[FrequencyValue] [nvarchar](20) NULL,
	[Active] [bit] NULL,
	[SortOrder] [int] NULL,
 CONSTRAINT [PK_Publication] PRIMARY KEY CLUSTERED 
(
	[PublicationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Publication', N'COLUMN',N'FrequencyType'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'A string representing the frequency (weekly, daily, monthyl etc)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Publication', @level2type=N'COLUMN',@level2name=N'FrequencyType'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'Publication', N'COLUMN',N'FrequencyValue'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The days of the week separated by ";" (for e.g. 1 - Monday 2-Tuesday) and 1;2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Publication', @level2type=N'COLUMN',@level2name=N'FrequencyValue'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Ratecard]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Ratecard](
	[RatecardId] [int] IDENTITY(1,1) NOT NULL,
	[BaseRateId] [int] NULL,
	[MinCharge] [money] NULL,
	[MaxCharge] [money] NULL,
	[RatePerMeasureUnit] [money] NULL,
	[MeasureUnitLimit] [int] NULL,
	[PhotoCharge] [money] NULL,
	[BoldHeading] [money] NULL,
	[OnlineEditionBundle] [money] NULL,
	[LineAdSuperBoldHeading] [money] NULL,
	[LineAdColourHeading] [money] NULL,
	[LineAdColourBorder] [money] NULL,
	[LineAdColourBackground] [money] NULL,
	[LineAdExtraImage] [money] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedByUser] [varchar](50) NULL,
 CONSTRAINT [PK_Ratecard] PRIMARY KEY CLUSTERED 
(
	[RatecardId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdDesign]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdDesign](
	[AdDesignId] [int] IDENTITY(1,1) NOT NULL,
	[AdId] [int] NULL,
	[AdTypeId] [int] NULL,
	[Status] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[Version] [int] NULL,
	[FirstAdDesignId] [int] NULL,
 CONSTRAINT [PK_AdDesign] PRIMARY KEY CLUSTERED 
(
	[AdDesignId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'AdDesign', N'COLUMN',N'Status'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1=Pending, 2=Approved, 3=Cancelled' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AdDesign', @level2type=N'COLUMN',@level2name=N'Status'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdBooking]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdBooking](
	[AdBookingId] [int] IDENTITY(1,1) NOT NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[TotalPrice] [money] NULL,
	[BookReference] [nchar](10) NULL,
	[AdId] [int] NULL,
	[UserId] [nvarchar](50) NULL,
	[BookingStatus] [int] NULL,
	[MainCategoryId] [int] NULL,
	[BookingType] [nvarchar](20) NULL,
	[BookingDate] [datetime] NULL,
	[Insertions] [int] NULL,
 CONSTRAINT [PK_AdBooking] PRIMARY KEY CLUSTERED 
(
	[AdBookingId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'AdBooking', N'COLUMN',N'BookingStatus'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1=Booked, 2=Expired, 3=Cancelled, 4=Unpaid' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AdBooking', @level2type=N'COLUMN',@level2name=N'BookingStatus'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdGraphic]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AdGraphic](
	[AdGraphicId] [int] IDENTITY(1,1) NOT NULL,
	[AdDesignId] [int] NULL,
	[DocumentID] [nvarchar](100) NULL,
	[Filename] [nvarchar](100) NULL,
	[ImageType] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_AdGraphic] PRIMARY KEY CLUSTERED 
(
	[AdGraphicId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Edition]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Edition](
	[EditionId] [int] IDENTITY(1,1) NOT NULL,
	[PublicationId] [int] NULL,
	[EditionDate] [datetime] NULL,
	[Deadline] [datetime] NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_Edition] PRIMARY KEY CLUSTERED 
(
	[EditionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BookEntry]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BookEntry](
	[BookEntryId] [int] IDENTITY(1,1) NOT NULL,
	[EditionDate] [datetime] NULL,
	[AdBookingId] [int] NULL,
	[PublicationId] [int] NULL,
	[EditionAdPrice] [money] NULL,
	[PublicationPrice] [money] NULL,
	[BaseRateId] [int] NULL,
	[RateType] [nvarchar](20) NULL,
 CONSTRAINT [PK_BookEntry] PRIMARY KEY CLUSTERED 
(
	[BookEntryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LineAd]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[LineAd](
	[LineAdId] [int] IDENTITY(1,1) NOT NULL,
	[AdDesignId] [int] NOT NULL,
	[AdHeader] [nvarchar](255) NULL,
	[AdText] [nvarchar](max) NOT NULL,
	[NumOfWords] [int] NULL,
	[UsePhoto] [bit] NULL,
	[UseBoldHeader] [bit] NULL,
	[IsColourBoldHeading] [bit] NULL,
	[IsColourBorder] [bit] NULL,
	[IsColourBackground] [bit] NULL,
	[IsSuperBoldHeading] [bit] NULL,
	[IsFooterPhoto] [bit] NULL,
	[BoldHeadingColourCode] [varchar](10) NULL,
	[BorderColourCode] [varchar](10) NULL,
	[BackgroundColourCode] [varchar](10) NULL,
	[FooterPhotoId] [varchar](100) NULL,
	[IsSuperHeadingPurchased] [bit] NULL,
 CONSTRAINT [PK_LineAd] PRIMARY KEY CLUSTERED 
(
	[LineAdId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PublicationCategory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PublicationCategory](
	[PublicationCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[ImageUrl] [nvarchar](255) NULL,
	[ParentId] [int] NULL,
	[MainCategoryId] [int] NULL,
	[PublicationId] [int] NULL,
 CONSTRAINT [PK_PublicationCategory] PRIMARY KEY CLUSTERED 
(
	[PublicationCategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PublicationAdType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PublicationAdType](
	[PublicationAdTypeId] [int] IDENTITY(1,1) NOT NULL,
	[PublicationId] [int] NULL,
	[AdTypeId] [int] NULL,
 CONSTRAINT [PK_PublicationAdType] PRIMARY KEY CLUSTERED 
(
	[PublicationAdTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OnlineAd]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OnlineAd](
	[OnlineAdId] [int] IDENTITY(1,1) NOT NULL,
	[AdDesignId] [int] NULL,
	[Heading] [nvarchar](255) NULL,
	[Description] [nvarchar](max) NULL,
	[HtmlText] [nvarchar](max) NULL,
	[Price] [money] NULL,
	[LocationId] [int] NULL,
	[LocationAreaId] [int] NULL,
	[ContactName] [nvarchar](200) NULL,
	[ContactType] [nvarchar](20) NULL,
	[ContactValue] [nvarchar](100) NULL,
	[NumOfViews] [int] NULL,
 CONSTRAINT [PK_OnlineAd] PRIMARY KEY CLUSTERED 
(
	[OnlineAdId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NonPublicationDate]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[NonPublicationDate](
	[NonPublicationDateId] [int] IDENTITY(1,1) NOT NULL,
	[PublicationId] [int] NOT NULL,
	[EditionDate] [datetime] NOT NULL,
 CONSTRAINT [PK_NonPublicationDate] PRIMARY KEY CLUSTERED 
(
	[NonPublicationDateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[RssFeed]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[RssFeed]
AS
SELECT     TOP 100  dbo.OnlineAd.OnlineAdId, dbo.AdBooking.StartDate, dbo.AdBooking.EndDate, dbo.AdBooking.BookReference, dbo.AdBooking.UserId, 
                      dbo.AdBooking.MainCategoryId, dbo.Ad.AdId, dbo.Ad.Title, dbo.OnlineAd.Heading, dbo.OnlineAd.HtmlText, dbo.OnlineAd.Description, dbo.OnlineAd.Price, 
                      dbo.OnlineAd.ContactName, dbo.MainCategory.ParentId, dbo.AdBooking.BookingDate, dbo.MainCategory.Title as CategoryTitle
FROM         dbo.OnlineAd INNER JOIN
                      dbo.AdDesign ON dbo.OnlineAd.AdDesignId = dbo.AdDesign.AdDesignId INNER JOIN
                      dbo.Ad ON dbo.AdDesign.AdId = dbo.Ad.AdId INNER JOIN
                      dbo.AdBooking ON dbo.Ad.AdId = dbo.AdBooking.AdId INNER JOIN
                      dbo.MainCategory ON dbo.AdBooking.MainCategoryId = dbo.MainCategory.MainCategoryId
WHERE     (dbo.AdBooking.BookingStatus = 1)
ORDER BY dbo.AdBooking.BookingDate DESC
'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PublicationSpecialRate]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PublicationSpecialRate](
	[PublicationSpecialRateId] [int] IDENTITY(1,1) NOT NULL,
	[SpecialRateId] [int] NULL,
	[PublicationAdTypeId] [int] NULL,
	[PublicationCategoryId] [int] NULL,
 CONSTRAINT [PK_PublicationSpecialRate] PRIMARY KEY CLUSTERED 
(
	[PublicationSpecialRateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PublicationRate]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PublicationRate](
	[PublicationRateId] [int] IDENTITY(1,1) NOT NULL,
	[PublicationAdTypeId] [int] NULL,
	[PublicationCategoryId] [int] NULL,
	[RatecardId] [int] NULL,
 CONSTRAINT [PK_PublicationRate] PRIMARY KEY CLUSTERED 
(
	[PublicationRateId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[OnlineAds]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[OnlineAds]
AS
SELECT        TOP (100) PERCENT dbo.OnlineAd.OnlineAdId, dbo.OnlineAd.Heading, dbo.OnlineAd.Description, dbo.OnlineAd.Price, dbo.Location.Title AS Location, 
                         dbo.OnlineAd.LocationId, dbo.LocationArea.Title AS Area, dbo.OnlineAd.ContactName, dbo.OnlineAd.LocationAreaId, dbo.OnlineAd.ContactType, 
                         dbo.OnlineAd.ContactValue, dbo.OnlineAd.NumOfViews, MainCategory_1.Title AS Category, MainCategory_1.ParentId AS ParentCategoryId,
                             (SELECT        Title
                               FROM            dbo.MainCategory
                               WHERE        (MainCategoryId = MainCategory_1.ParentId)) AS ParentCategoryTitle, dbo.AdBooking.UserId, dbo.AdBooking.BookingDate, 
                         dbo.AdBooking.BookingStatus, dbo.AdDesign.AdId, dbo.AdDesign.AdDesignId, dbo.AdType.Code AS AdType, MainCategory_1.MainCategoryId
FROM            dbo.MainCategory AS MainCategory_1 INNER JOIN
                         dbo.AdBooking ON MainCategory_1.MainCategoryId = dbo.AdBooking.MainCategoryId INNER JOIN
                         dbo.OnlineAd INNER JOIN
                         dbo.AdDesign ON dbo.OnlineAd.AdDesignId = dbo.AdDesign.AdDesignId INNER JOIN
                         dbo.Location ON dbo.OnlineAd.LocationId = dbo.Location.LocationId INNER JOIN
                         dbo.LocationArea ON dbo.OnlineAd.LocationAreaId = dbo.LocationArea.LocationAreaId AND dbo.Location.LocationId = dbo.LocationArea.LocationId ON 
                         dbo.AdBooking.AdId = dbo.AdDesign.AdId INNER JOIN
                         dbo.AdType ON dbo.AdDesign.AdTypeId = dbo.AdType.AdTypeId
WHERE        (dbo.AdType.Code = ''ONLINE'') AND (dbo.AdBooking.BookingStatus = 1)
'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane1' , N'SCHEMA',N'dbo', N'VIEW',N'OnlineAds', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[56] 4[16] 2[24] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "MainCategory_1"
            Begin Extent = 
               Top = 174
               Left = 34
               Bottom = 303
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AdBooking"
            Begin Extent = 
               Top = 307
               Left = 459
               Bottom = 436
               Right = 633
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "OnlineAd"
            Begin Extent = 
               Top = 45
               Left = 535
               Bottom = 174
               Right = 705
            End
            DisplayFlags = 280
            TopColumn = 8
         End
         Begin Table = "AdDesign"
            Begin Extent = 
               Top = 12
               Left = 232
               Bottom = 141
               Right = 404
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "Location"
            Begin Extent = 
               Top = 0
               Left = 780
               Bottom = 112
               Right = 950
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "LocationArea"
            Begin Extent = 
               Top = 304
               Left = 702
               Bottom = 433
               Right = 872
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "AdType"
            Begin Extent = 
               Top = 8
               Left = 0
               Bottom = 137
               Right = 170
            End
            Displ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'OnlineAds'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane2' , N'SCHEMA',N'dbo', N'VIEW',N'OnlineAds', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'ayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 3225
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'OnlineAds'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPaneCount' , N'SCHEMA',N'dbo', N'VIEW',N'OnlineAds', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'OnlineAds'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[OnlineAdEnquiry]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[OnlineAdEnquiry](
	[OnlineAdEnquiryId] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[OnlineAdId] [int] NOT NULL,
	[EnquiryTypeId] [int] NOT NULL,
	[Email] [nvarchar](100) NULL,
	[Phone] [nvarchar](12) NULL,
	[EnquiryText] [nvarchar](max) NULL,
	[OpenDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_OnlineAdEnquiry] PRIMARY KEY CLUSTERED 
(
	[OnlineAdEnquiryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[Invoice]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[Invoice]
AS
SELECT     dbo.AdBooking.StartDate, dbo.AdBooking.EndDate, dbo.AdBooking.TotalPrice, dbo.MainCategory.Title AS Category, dbo.AdBooking.BookReference, 
                      dbo.AdBooking.UserId, dbo.Publication.Title AS Publication, dbo.BookEntry.EditionDate, dbo.[Transaction].TransactionDate
FROM         dbo.AdBooking INNER JOIN
                      dbo.MainCategory ON dbo.AdBooking.MainCategoryId = dbo.MainCategory.MainCategoryId INNER JOIN
                      dbo.BookEntry ON dbo.AdBooking.AdBookingId = dbo.BookEntry.AdBookingId INNER JOIN
                      dbo.Publication ON dbo.BookEntry.PublicationId = dbo.Publication.PublicationId INNER JOIN
                      dbo.[Transaction] ON dbo.AdBooking.BookReference = dbo.[Transaction].Title

'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EnquiryDocument]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EnquiryDocument](
	[EnquiryDocumentId] [int] IDENTITY(1,1) NOT NULL,
	[OnlineAdEnquiryId] [int] NOT NULL,
	[DocumentId] [int] NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_EnquiryDocument] PRIMARY KEY CLUSTERED 
(
	[EnquiryDocumentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ScriptLog_RunDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[ScriptLog]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ScriptLog_RunDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ScriptLog] ADD  CONSTRAINT [DF_ScriptLog_RunDate]  DEFAULT (getdate()) FOR [RunDate]
END


End
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AdBooking_Ad]') AND parent_object_id = OBJECT_ID(N'[dbo].[AdBooking]'))
ALTER TABLE [dbo].[AdBooking]  WITH CHECK ADD  CONSTRAINT [FK_AdBooking_Ad] FOREIGN KEY([AdId])
REFERENCES [dbo].[Ad] ([AdId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AdBooking_Ad]') AND parent_object_id = OBJECT_ID(N'[dbo].[AdBooking]'))
ALTER TABLE [dbo].[AdBooking] CHECK CONSTRAINT [FK_AdBooking_Ad]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AdBooking_MainCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[AdBooking]'))
ALTER TABLE [dbo].[AdBooking]  WITH CHECK ADD  CONSTRAINT [FK_AdBooking_MainCategory] FOREIGN KEY([MainCategoryId])
REFERENCES [dbo].[MainCategory] ([MainCategoryId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AdBooking_MainCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[AdBooking]'))
ALTER TABLE [dbo].[AdBooking] CHECK CONSTRAINT [FK_AdBooking_MainCategory]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AdDesign_Ad]') AND parent_object_id = OBJECT_ID(N'[dbo].[AdDesign]'))
ALTER TABLE [dbo].[AdDesign]  WITH CHECK ADD  CONSTRAINT [FK_AdDesign_Ad] FOREIGN KEY([AdId])
REFERENCES [dbo].[Ad] ([AdId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AdDesign_Ad]') AND parent_object_id = OBJECT_ID(N'[dbo].[AdDesign]'))
ALTER TABLE [dbo].[AdDesign] CHECK CONSTRAINT [FK_AdDesign_Ad]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AdDesign_AdType]') AND parent_object_id = OBJECT_ID(N'[dbo].[AdDesign]'))
ALTER TABLE [dbo].[AdDesign]  WITH CHECK ADD  CONSTRAINT [FK_AdDesign_AdType] FOREIGN KEY([AdTypeId])
REFERENCES [dbo].[AdType] ([AdTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AdDesign_AdType]') AND parent_object_id = OBJECT_ID(N'[dbo].[AdDesign]'))
ALTER TABLE [dbo].[AdDesign] CHECK CONSTRAINT [FK_AdDesign_AdType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AdGraphic_AdDesign]') AND parent_object_id = OBJECT_ID(N'[dbo].[AdGraphic]'))
ALTER TABLE [dbo].[AdGraphic]  WITH CHECK ADD  CONSTRAINT [FK_AdGraphic_AdDesign] FOREIGN KEY([AdDesignId])
REFERENCES [dbo].[AdDesign] ([AdDesignId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AdGraphic_AdDesign]') AND parent_object_id = OBJECT_ID(N'[dbo].[AdGraphic]'))
ALTER TABLE [dbo].[AdGraphic] CHECK CONSTRAINT [FK_AdGraphic_AdDesign]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BookEntry_AdBooking]') AND parent_object_id = OBJECT_ID(N'[dbo].[BookEntry]'))
ALTER TABLE [dbo].[BookEntry]  WITH CHECK ADD  CONSTRAINT [FK_BookEntry_AdBooking] FOREIGN KEY([AdBookingId])
REFERENCES [dbo].[AdBooking] ([AdBookingId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BookEntry_AdBooking]') AND parent_object_id = OBJECT_ID(N'[dbo].[BookEntry]'))
ALTER TABLE [dbo].[BookEntry] CHECK CONSTRAINT [FK_BookEntry_AdBooking]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BookEntry_Publication]') AND parent_object_id = OBJECT_ID(N'[dbo].[BookEntry]'))
ALTER TABLE [dbo].[BookEntry]  WITH CHECK ADD  CONSTRAINT [FK_BookEntry_Publication] FOREIGN KEY([PublicationId])
REFERENCES [dbo].[Publication] ([PublicationId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BookEntry_Publication]') AND parent_object_id = OBJECT_ID(N'[dbo].[BookEntry]'))
ALTER TABLE [dbo].[BookEntry] CHECK CONSTRAINT [FK_BookEntry_Publication]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Edition_Publication]') AND parent_object_id = OBJECT_ID(N'[dbo].[Edition]'))
ALTER TABLE [dbo].[Edition]  WITH CHECK ADD  CONSTRAINT [FK_Edition_Publication] FOREIGN KEY([PublicationId])
REFERENCES [dbo].[Publication] ([PublicationId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Edition_Publication]') AND parent_object_id = OBJECT_ID(N'[dbo].[Edition]'))
ALTER TABLE [dbo].[Edition] CHECK CONSTRAINT [FK_Edition_Publication]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EnquiryDocument_OnlineAdEnquiry]') AND parent_object_id = OBJECT_ID(N'[dbo].[EnquiryDocument]'))
ALTER TABLE [dbo].[EnquiryDocument]  WITH CHECK ADD  CONSTRAINT [FK_EnquiryDocument_OnlineAdEnquiry] FOREIGN KEY([OnlineAdEnquiryId])
REFERENCES [dbo].[OnlineAdEnquiry] ([OnlineAdEnquiryId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EnquiryDocument_OnlineAdEnquiry]') AND parent_object_id = OBJECT_ID(N'[dbo].[EnquiryDocument]'))
ALTER TABLE [dbo].[EnquiryDocument] CHECK CONSTRAINT [FK_EnquiryDocument_OnlineAdEnquiry]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_LineAd_AdDesign]') AND parent_object_id = OBJECT_ID(N'[dbo].[LineAd]'))
ALTER TABLE [dbo].[LineAd]  WITH CHECK ADD  CONSTRAINT [FK_LineAd_AdDesign] FOREIGN KEY([AdDesignId])
REFERENCES [dbo].[AdDesign] ([AdDesignId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_LineAd_AdDesign]') AND parent_object_id = OBJECT_ID(N'[dbo].[LineAd]'))
ALTER TABLE [dbo].[LineAd] CHECK CONSTRAINT [FK_LineAd_AdDesign]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_LocationArea_Location]') AND parent_object_id = OBJECT_ID(N'[dbo].[LocationArea]'))
ALTER TABLE [dbo].[LocationArea]  WITH CHECK ADD  CONSTRAINT [FK_LocationArea_Location] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([LocationId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_LocationArea_Location]') AND parent_object_id = OBJECT_ID(N'[dbo].[LocationArea]'))
ALTER TABLE [dbo].[LocationArea] CHECK CONSTRAINT [FK_LocationArea_Location]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_NonPublicationDate_Publication]') AND parent_object_id = OBJECT_ID(N'[dbo].[NonPublicationDate]'))
ALTER TABLE [dbo].[NonPublicationDate]  WITH CHECK ADD  CONSTRAINT [FK_NonPublicationDate_Publication] FOREIGN KEY([PublicationId])
REFERENCES [dbo].[Publication] ([PublicationId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_NonPublicationDate_Publication]') AND parent_object_id = OBJECT_ID(N'[dbo].[NonPublicationDate]'))
ALTER TABLE [dbo].[NonPublicationDate] CHECK CONSTRAINT [FK_NonPublicationDate_Publication]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OnlineAd_AdDesign]') AND parent_object_id = OBJECT_ID(N'[dbo].[OnlineAd]'))
ALTER TABLE [dbo].[OnlineAd]  WITH CHECK ADD  CONSTRAINT [FK_OnlineAd_AdDesign] FOREIGN KEY([AdDesignId])
REFERENCES [dbo].[AdDesign] ([AdDesignId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OnlineAd_AdDesign]') AND parent_object_id = OBJECT_ID(N'[dbo].[OnlineAd]'))
ALTER TABLE [dbo].[OnlineAd] CHECK CONSTRAINT [FK_OnlineAd_AdDesign]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OnlineAd_Location]') AND parent_object_id = OBJECT_ID(N'[dbo].[OnlineAd]'))
ALTER TABLE [dbo].[OnlineAd]  WITH CHECK ADD  CONSTRAINT [FK_OnlineAd_Location] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([LocationId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OnlineAd_Location]') AND parent_object_id = OBJECT_ID(N'[dbo].[OnlineAd]'))
ALTER TABLE [dbo].[OnlineAd] CHECK CONSTRAINT [FK_OnlineAd_Location]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OnlineAd_LocationArea]') AND parent_object_id = OBJECT_ID(N'[dbo].[OnlineAd]'))
ALTER TABLE [dbo].[OnlineAd]  WITH CHECK ADD  CONSTRAINT [FK_OnlineAd_LocationArea] FOREIGN KEY([LocationAreaId])
REFERENCES [dbo].[LocationArea] ([LocationAreaId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OnlineAd_LocationArea]') AND parent_object_id = OBJECT_ID(N'[dbo].[OnlineAd]'))
ALTER TABLE [dbo].[OnlineAd] CHECK CONSTRAINT [FK_OnlineAd_LocationArea]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OnlineAdEnquiry_EnquiryType]') AND parent_object_id = OBJECT_ID(N'[dbo].[OnlineAdEnquiry]'))
ALTER TABLE [dbo].[OnlineAdEnquiry]  WITH CHECK ADD  CONSTRAINT [FK_OnlineAdEnquiry_EnquiryType] FOREIGN KEY([EnquiryTypeId])
REFERENCES [dbo].[EnquiryType] ([EnquiryTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OnlineAdEnquiry_EnquiryType]') AND parent_object_id = OBJECT_ID(N'[dbo].[OnlineAdEnquiry]'))
ALTER TABLE [dbo].[OnlineAdEnquiry] CHECK CONSTRAINT [FK_OnlineAdEnquiry_EnquiryType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OnlineAdEnquiry_OnlineAd]') AND parent_object_id = OBJECT_ID(N'[dbo].[OnlineAdEnquiry]'))
ALTER TABLE [dbo].[OnlineAdEnquiry]  WITH CHECK ADD  CONSTRAINT [FK_OnlineAdEnquiry_OnlineAd] FOREIGN KEY([OnlineAdId])
REFERENCES [dbo].[OnlineAd] ([OnlineAdId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_OnlineAdEnquiry_OnlineAd]') AND parent_object_id = OBJECT_ID(N'[dbo].[OnlineAdEnquiry]'))
ALTER TABLE [dbo].[OnlineAdEnquiry] CHECK CONSTRAINT [FK_OnlineAdEnquiry_OnlineAd]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Publication_PublicationType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Publication]'))
ALTER TABLE [dbo].[Publication]  WITH CHECK ADD  CONSTRAINT [FK_Publication_PublicationType] FOREIGN KEY([PublicationTypeId])
REFERENCES [dbo].[PublicationType] ([PublicationTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Publication_PublicationType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Publication]'))
ALTER TABLE [dbo].[Publication] CHECK CONSTRAINT [FK_Publication_PublicationType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PublicationAdType_AdType]') AND parent_object_id = OBJECT_ID(N'[dbo].[PublicationAdType]'))
ALTER TABLE [dbo].[PublicationAdType]  WITH CHECK ADD  CONSTRAINT [FK_PublicationAdType_AdType] FOREIGN KEY([AdTypeId])
REFERENCES [dbo].[AdType] ([AdTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PublicationAdType_AdType]') AND parent_object_id = OBJECT_ID(N'[dbo].[PublicationAdType]'))
ALTER TABLE [dbo].[PublicationAdType] CHECK CONSTRAINT [FK_PublicationAdType_AdType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PublicationAdType_Publication]') AND parent_object_id = OBJECT_ID(N'[dbo].[PublicationAdType]'))
ALTER TABLE [dbo].[PublicationAdType]  WITH CHECK ADD  CONSTRAINT [FK_PublicationAdType_Publication] FOREIGN KEY([PublicationId])
REFERENCES [dbo].[Publication] ([PublicationId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PublicationAdType_Publication]') AND parent_object_id = OBJECT_ID(N'[dbo].[PublicationAdType]'))
ALTER TABLE [dbo].[PublicationAdType] CHECK CONSTRAINT [FK_PublicationAdType_Publication]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PublicationCategory_MainCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[PublicationCategory]'))
ALTER TABLE [dbo].[PublicationCategory]  WITH CHECK ADD  CONSTRAINT [FK_PublicationCategory_MainCategory] FOREIGN KEY([MainCategoryId])
REFERENCES [dbo].[MainCategory] ([MainCategoryId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PublicationCategory_MainCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[PublicationCategory]'))
ALTER TABLE [dbo].[PublicationCategory] CHECK CONSTRAINT [FK_PublicationCategory_MainCategory]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PublicationCategory_Publication]') AND parent_object_id = OBJECT_ID(N'[dbo].[PublicationCategory]'))
ALTER TABLE [dbo].[PublicationCategory]  WITH CHECK ADD  CONSTRAINT [FK_PublicationCategory_Publication] FOREIGN KEY([PublicationId])
REFERENCES [dbo].[Publication] ([PublicationId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PublicationCategory_Publication]') AND parent_object_id = OBJECT_ID(N'[dbo].[PublicationCategory]'))
ALTER TABLE [dbo].[PublicationCategory] CHECK CONSTRAINT [FK_PublicationCategory_Publication]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PublicationRate_PublicationAdType]') AND parent_object_id = OBJECT_ID(N'[dbo].[PublicationRate]'))
ALTER TABLE [dbo].[PublicationRate]  WITH CHECK ADD  CONSTRAINT [FK_PublicationRate_PublicationAdType] FOREIGN KEY([PublicationAdTypeId])
REFERENCES [dbo].[PublicationAdType] ([PublicationAdTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PublicationRate_PublicationAdType]') AND parent_object_id = OBJECT_ID(N'[dbo].[PublicationRate]'))
ALTER TABLE [dbo].[PublicationRate] CHECK CONSTRAINT [FK_PublicationRate_PublicationAdType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PublicationRate_PublicationCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[PublicationRate]'))
ALTER TABLE [dbo].[PublicationRate]  WITH CHECK ADD  CONSTRAINT [FK_PublicationRate_PublicationCategory] FOREIGN KEY([PublicationCategoryId])
REFERENCES [dbo].[PublicationCategory] ([PublicationCategoryId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PublicationRate_PublicationCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[PublicationRate]'))
ALTER TABLE [dbo].[PublicationRate] CHECK CONSTRAINT [FK_PublicationRate_PublicationCategory]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PublicationRate_Ratecard]') AND parent_object_id = OBJECT_ID(N'[dbo].[PublicationRate]'))
ALTER TABLE [dbo].[PublicationRate]  WITH CHECK ADD  CONSTRAINT [FK_PublicationRate_Ratecard] FOREIGN KEY([RatecardId])
REFERENCES [dbo].[Ratecard] ([RatecardId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PublicationRate_Ratecard]') AND parent_object_id = OBJECT_ID(N'[dbo].[PublicationRate]'))
ALTER TABLE [dbo].[PublicationRate] CHECK CONSTRAINT [FK_PublicationRate_Ratecard]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PublicationSpecialRate_PublicationAdType]') AND parent_object_id = OBJECT_ID(N'[dbo].[PublicationSpecialRate]'))
ALTER TABLE [dbo].[PublicationSpecialRate]  WITH CHECK ADD  CONSTRAINT [FK_PublicationSpecialRate_PublicationAdType] FOREIGN KEY([PublicationAdTypeId])
REFERENCES [dbo].[PublicationAdType] ([PublicationAdTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PublicationSpecialRate_PublicationAdType]') AND parent_object_id = OBJECT_ID(N'[dbo].[PublicationSpecialRate]'))
ALTER TABLE [dbo].[PublicationSpecialRate] CHECK CONSTRAINT [FK_PublicationSpecialRate_PublicationAdType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PublicationSpecialRate_PublicationCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[PublicationSpecialRate]'))
ALTER TABLE [dbo].[PublicationSpecialRate]  WITH CHECK ADD  CONSTRAINT [FK_PublicationSpecialRate_PublicationCategory] FOREIGN KEY([PublicationCategoryId])
REFERENCES [dbo].[PublicationCategory] ([PublicationCategoryId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PublicationSpecialRate_PublicationCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[PublicationSpecialRate]'))
ALTER TABLE [dbo].[PublicationSpecialRate] CHECK CONSTRAINT [FK_PublicationSpecialRate_PublicationCategory]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PublicationSpecialRate_SpecialRate]') AND parent_object_id = OBJECT_ID(N'[dbo].[PublicationSpecialRate]'))
ALTER TABLE [dbo].[PublicationSpecialRate]  WITH CHECK ADD  CONSTRAINT [FK_PublicationSpecialRate_SpecialRate] FOREIGN KEY([SpecialRateId])
REFERENCES [dbo].[SpecialRate] ([SpecialRateId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PublicationSpecialRate_SpecialRate]') AND parent_object_id = OBJECT_ID(N'[dbo].[PublicationSpecialRate]'))
ALTER TABLE [dbo].[PublicationSpecialRate] CHECK CONSTRAINT [FK_PublicationSpecialRate_SpecialRate]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Ratecard_BaseRate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Ratecard]'))
ALTER TABLE [dbo].[Ratecard]  WITH CHECK ADD  CONSTRAINT [FK_Ratecard_BaseRate] FOREIGN KEY([BaseRateId])
REFERENCES [dbo].[BaseRate] ([BaseRateId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Ratecard_BaseRate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Ratecard]'))
ALTER TABLE [dbo].[Ratecard] CHECK CONSTRAINT [FK_Ratecard_BaseRate]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SpecialRate_BaseRate]') AND parent_object_id = OBJECT_ID(N'[dbo].[SpecialRate]'))
ALTER TABLE [dbo].[SpecialRate]  WITH CHECK ADD  CONSTRAINT [FK_SpecialRate_BaseRate] FOREIGN KEY([BaseRateId])
REFERENCES [dbo].[BaseRate] ([BaseRateId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SpecialRate_BaseRate]') AND parent_object_id = OBJECT_ID(N'[dbo].[SpecialRate]'))
ALTER TABLE [dbo].[SpecialRate] CHECK CONSTRAINT [FK_SpecialRate_BaseRate]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WebAdSpace_WebAdSpaceSetting]') AND parent_object_id = OBJECT_ID(N'[dbo].[WebAdSpace]'))
ALTER TABLE [dbo].[WebAdSpace]  WITH CHECK ADD  CONSTRAINT [FK_WebAdSpace_WebAdSpaceSetting] FOREIGN KEY([SettingID])
REFERENCES [dbo].[WebAdSpaceSetting] ([SettingId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_WebAdSpace_WebAdSpaceSetting]') AND parent_object_id = OBJECT_ID(N'[dbo].[WebAdSpace]'))
ALTER TABLE [dbo].[WebAdSpace] CHECK CONSTRAINT [FK_WebAdSpace_WebAdSpaceSetting]
GO
