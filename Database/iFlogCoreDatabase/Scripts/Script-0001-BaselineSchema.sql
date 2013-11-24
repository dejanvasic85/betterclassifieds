SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntityCounter]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EntityCounter](
	[CounterId] [int] IDENTITY(1,1) NOT NULL,
	[DateTimeCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_EntityCounter] PRIMARY KEY CLUSTERED 
(
	[CounterId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BillingInvoice]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BillingInvoice](
	[InvoiceId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Status] [nvarchar](20) NOT NULL,
	[DateTimeCreated] [datetime] NOT NULL,
	[DateTimeUpdated] [datetime] NOT NULL,
	[SessionId] [nvarchar](30) NOT NULL,
	[ReferenceNumber] [bigint] NOT NULL,
	[BillingName] [nvarchar](150) NULL,
	[BillingAddress1] [nvarchar](250) NULL,
	[BillingAddress2] [nvarchar](250) NULL,
	[BillingPostcode] [nvarchar](12) NULL,
	[BillingCity] [nvarchar](150) NULL,
	[BillingState] [nvarchar](50) NULL,
	[BillingCountry] [nvarchar](250) NULL,
	[DeliveryName] [nvarchar](150) NULL,
	[DeliveryAddress1] [nvarchar](250) NULL,
	[DeliveryAddress2] [nvarchar](250) NULL,
	[DeliveryPostcode] [nvarchar](12) NULL,
	[DeliveryCity] [nvarchar](150) NULL,
	[DeliveryState] [nvarchar](50) NULL,
	[DeliveryCountry] [nvarchar](250) NULL,
	[PaymentType] [nvarchar](20) NULL,
	[PaymentReference] [nvarchar](50) NULL,
	[ClientCode] [nvarchar](11) NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[InvoiceId] ASC
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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TimeZone]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TimeZone](
	[TimeZoneCode] [nvarchar](5) NOT NULL,
	[TimeZoneDescription] [nvarchar](50) NOT NULL,
	[CountryCode] [char](2) NOT NULL,
	[TimeZoneUtc] [decimal](18, 1) NOT NULL,
 CONSTRAINT [PK_TimeZone_1] PRIMARY KEY CLUSTERED 
(
	[TimeZoneCode] ASC,
	[CountryCode] ASC
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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[State]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[State](
	[StateId] [int] IDENTITY(1,1) NOT NULL,
	[StateCode] [nvarchar](5) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](100) NULL,
 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED 
(
	[StateCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ShoppingCart]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ShoppingCart](
	[ShoppingCartId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Status] [nvarchar](20) NOT NULL,
	[DateTimeCreated] [datetime] NOT NULL,
	[DateTimeModified] [datetime] NOT NULL,
	[SessionId] [nvarchar](30) NOT NULL,
	[ClientCode] [nvarchar](11) NULL,
 CONSTRAINT [PK_ShoppingCart] PRIMARY KEY CLUSTERED 
(
	[ShoppingCartId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Region]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Region](
	[RegionId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](30) NULL,
	[StateCode] [nvarchar](5) NOT NULL,
	[TimeZone] [nvarchar](200) NULL,
 CONSTRAINT [PK_Region_1] PRIMARY KEY CLUSTERED 
(
	[RegionId] ASC
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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Product]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Product](
	[ProductId] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[ImageId] [uniqueidentifier] NULL,
	[ClientId] [varchar](11) NOT NULL,
 CONSTRAINT [PK_Product_1] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NameValueCollection]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[NameValueCollection](
	[Name] [nvarchar](50) NOT NULL,
	[Value] [nvarchar](150) NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Module]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Module](
	[ModuleId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Module] PRIMARY KEY CLUSTERED 
(
	[ModuleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BannerFileType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BannerFileType](
	[BannerFileTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](5) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_BannerFileType] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Address]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Address](
	[AddressId] [int] NOT NULL,
	[Street] [nvarchar](100) NULL,
	[Suburb] [nvarchar](100) NULL,
	[City] [nvarchar](100) NULL,
	[Province] [nvarchar](100) NULL,
	[PostCode] [nvarchar](5) NULL,
	[RegionId] [int] NULL,
	[CountryCode] [nvarchar](4) NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[AddressId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Contact]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Contact](
	[ContactId] [int] IDENTITY(1,1) NOT NULL,
	[Firstname] [nvarchar](50) NULL,
	[Lastname] [nvarchar](50) NULL,
	[Prefix] [nvarchar](10) NULL,
	[Email] [nvarchar](50) NULL,
	[Phone] [nvarchar](12) NULL,
	[Mobile] [nvarchar](12) NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[ContactId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BusinessIndustry]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BusinessIndustry](
	[BusinessIndustryId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_BusinessIndustry] PRIMARY KEY CLUSTERED 
(
	[BusinessIndustryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Broadcast]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Broadcast](
	[BroadcastId] [uniqueidentifier] NOT NULL,
	[EntityCode] [nvarchar](10) NOT NULL,
	[ApplicationName] [nvarchar](50) NULL,
	[Type] [nchar](10) NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_Broadcast] PRIMARY KEY CLUSTERED 
(
	[BroadcastId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Billing_Bank]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Billing_Bank](
	[BankId] [uniqueidentifier] NOT NULL,
	[BankName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](250) NOT NULL,
	[GatewayUrl] [nvarchar](500) NOT NULL,
	[ReturnLinkText] [nvarchar](200) NOT NULL,
	[GSTRate] [decimal](18, 2) NOT NULL,
	[RefundPolicyUrl] [nvarchar](500) NOT NULL,
	[ReturnLinkUrl] [nvarchar](500) NOT NULL,
	[AllowOverride] [bit] NOT NULL,
 CONSTRAINT [PK_Billing_Bank] PRIMARY KEY CLUSTERED 
(
	[BankId] ASC
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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BannerReferenceType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BannerReferenceType](
	[BannerReferenceTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](100) NULL,
 CONSTRAINT [PK_BannerReferenceType] PRIMARY KEY CLUSTERED 
(
	[BannerReferenceTypeId] ASC
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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocumentCategory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DocumentCategory](
	[DocumentCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NULL,
	[Code] [int] NOT NULL,
	[ExpiryPurgeDays] [int] NULL,
	[AcceptedFileTypes] [varchar](255) NULL,
	[MaximumFileSize] [int] NULL,
 CONSTRAINT [PK_DocumentCategory] PRIMARY KEY CLUSTERED 
(
	[DocumentCategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'DocumentCategory', N'COLUMN',N'Code'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Refer to DslDocumentCategory within the Paramount Business Objects' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DocumentCategory', @level2type=N'COLUMN',@level2name=N'Code'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'DocumentCategory', N'COLUMN',N'ExpiryPurgeDays'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Number of days images are kept until purged. NULL value indicates never.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DocumentCategory', @level2type=N'COLUMN',@level2name=N'ExpiryPurgeDays'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'DocumentCategory', N'COLUMN',N'AcceptedFileTypes'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Semi Colon separated list of accepted file extensions' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DocumentCategory', @level2type=N'COLUMN',@level2name=N'AcceptedFileTypes'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'DocumentCategory', N'COLUMN',N'MaximumFileSize'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Maximum size of each file in bytes allowed for this category. 1 megabytes = 1 048 576 bytes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DocumentCategory', @level2type=N'COLUMN',@level2name=N'MaximumFileSize'
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Currency]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Currency](
	[CurrencyId] [uniqueidentifier] NOT NULL,
	[CurrencyCode] [nchar](5) NOT NULL,
	[CurrencyName] [nvarchar](150) NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmailTracker]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EmailTracker](
	[EmailTrackerId] [int] IDENTITY(1,1) NOT NULL,
	[EmailBroadcastEntryId] [uniqueidentifier] NOT NULL,
	[Page] [nvarchar](max) NULL,
	[IpAddress] [nvarchar](50) NULL,
	[Browser] [nvarchar](50) NULL,
	[DateTime] [datetime] NOT NULL,
	[Country] [nvarchar](150) NULL,
	[Region] [nvarchar](150) NULL,
	[City] [nvarchar](150) NULL,
	[Postcode] [nvarchar](50) NULL,
	[Latitude] [nvarchar](50) NULL,
	[Longitude] [nvarchar](50) NULL,
	[TimeZone] [nchar](10) NULL,
 CONSTRAINT [PK_EmailTracker] PRIMARY KEY CLUSTERED 
(
	[EmailTrackerId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmailTemplate]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EmailTemplate](
	[EmailTemplateId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](150) NULL,
	[EmailContent] [nvarchar](max) NOT NULL,
	[Subject] [nvarchar](50) NOT NULL,
	[Sender] [nvarchar](100) NOT NULL,
	[EntityCode] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_EmailTemplate] PRIMARY KEY CLUSTERED 
(
	[EmailTemplateId] ASC
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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmailBroadcastEntry]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EmailBroadcastEntry](
	[EmailBroadcastEntryId] [int] IDENTITY(1,1) NOT NULL,
	[BroadcastId] [uniqueidentifier] NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[EmailContent] [nvarchar](max) NOT NULL,
	[LastRetryDateTime] [datetime] NULL,
	[SentDateTime] [datetime] NULL,
	[RetryNo] [int] NOT NULL,
	[Subject] [varchar](50) NOT NULL,
	[Sender] [nvarchar](50) NOT NULL,
	[IsBodyHtml] [bit] NOT NULL,
	[Priority] [int] NOT NULL,
	[CreateDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_EmailBroadcastEntry] PRIMARY KEY CLUSTERED 
(
	[EmailBroadcastEntryId] ASC
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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmailBroadcast]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EmailBroadcast](
	[EmailBroadcastId] [int] IDENTITY(1,1) NOT NULL,
	[TemplateName] [nvarchar](100) NOT NULL,
	[BroadcastId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_EmailBroadcast] PRIMARY KEY CLUSTERED 
(
	[EmailBroadcastId] ASC
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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Entity]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Entity](
	[EntityCode] [nvarchar](10) NOT NULL,
	[EntityName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](20) NULL,
	[PrimaryContactId] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[TimeZone] [int] NULL,
	[ABN] [nvarchar](12) NULL,
	[BusinessIndustryId] [int] NULL,
	[Active] [bit] NULL,
	[TimeZoneRef] [varchar](100) NULL,
	[CultureRef] [varchar](10) NULL,
 CONSTRAINT [PK_Entity] PRIMARY KEY CLUSTERED 
(
	[EntityCode] ASC
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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BannerGroup]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BannerGroup](
	[BannerGroupId] [uniqueidentifier] NOT NULL,
	[ClientCode] [varchar](10) NOT NULL,
	[Title] [varchar](50) NULL,
	[Height] [int] NULL,
	[Width] [int] NULL,
	[AcceptedFileType] [nvarchar](5) NULL,
	[IsTimerEnabled] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_BannerGroup] PRIMARY KEY CLUSTERED 
(
	[BannerGroupId] ASC
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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocumentStorage]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DocumentStorage](
	[DocumentID] [uniqueidentifier] NOT NULL,
	[ApplicationCode] [nvarchar](30) NOT NULL,
	[EntityCode] [nvarchar](10) NOT NULL,
	[AccountID] [uniqueidentifier] NULL,
	[DocumentCategoryId] [int] NOT NULL,
	[Username] [nvarchar](50) NULL,
	[FileType] [nvarchar](max) NOT NULL,
	[FileData] [varbinary](max) NULL,
	[FileLength] [int] NOT NULL,
	[NumberOfChunks] [numeric](18, 0) NULL,
	[FileName] [nvarchar](100) NULL,
	[Reference] [nvarchar](100) NULL,
	[IsPrivate] [bit] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[IsFileComplete] [bit] NULL,
 CONSTRAINT [PK_DocumentStorage] PRIMARY KEY CLUSTERED 
(
	[DocumentID] ASC
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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactAddress]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ContactAddress](
	[ContactAddressId] [int] IDENTITY(1,1) NOT NULL,
	[ContactId] [int] NOT NULL,
	[AddressId] [int] NOT NULL,
 CONSTRAINT [PK_ContactAddress] PRIMARY KEY CLUSTERED 
(
	[ContactAddressId] ASC
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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BillingSettings]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BillingSettings](
	[ClientCode] [varchar](10) NOT NULL,
	[BankId] [uniqueidentifier] NOT NULL,
	[BankName] [nvarchar](50) NULL,
	[Description] [nvarchar](250) NULL,
	[GatewayUrl] [nvarchar](500) NULL,
	[ReturnLinkText] [nvarchar](200) NULL,
	[GSTRate] [decimal](18, 2) NULL,
	[RefundPolicyUrl] [nvarchar](500) NULL,
	[ReturnLinkUrl] [nvarchar](500) NULL,
	[CollectAddressDetails] [bit] NOT NULL,
	[InvoiceBannerImageId] [uniqueidentifier] NOT NULL,
	[ReferencePrefix] [nchar](5) NOT NULL,
	[PP_BusinessEmail] [nvarchar](300) NOT NULL,
	[PP_CurrencyCode] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_BillingSettings] PRIMARY KEY CLUSTERED 
(
	[ClientCode] ASC,
	[BankId] ASC
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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccountTransactionType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AccountTransactionType](
	[TransactionTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NOT NULL,
	[ModuleId] [int] NULL,
 CONSTRAINT [PK_AccountTransactionType] PRIMARY KEY CLUSTERED 
(
	[TransactionTypeId] ASC
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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BillingInvoiceItem]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BillingInvoiceItem](
	[InvoiceItemId] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Summary] [nvarchar](500) NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[Price] [money] NOT NULL,
	[InvoiceId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_BillingInvoiceItem] PRIMARY KEY CLUSTERED 
(
	[InvoiceItemId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ShoppingCartItem]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ShoppingCartItem](
	[ShoppingCartItemId] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[Price] [money] NOT NULL,
	[ProductType] [nvarchar](20) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Summary] [nvarchar](max) NULL,
	[ShoppingCartId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ShoppingCartItem] PRIMARY KEY CLUSTERED 
(
	[ShoppingCartItemId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntityModule]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EntityModule](
	[EntityModuleId] [int] IDENTITY(1,1) NOT NULL,
	[EntityCode] [nvarchar](10) NOT NULL,
	[ModuleId] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
 CONSTRAINT [PK_Subscription] PRIMARY KEY CLUSTERED 
(
	[EntityModuleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EntityContact]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EntityContact](
	[EntityContactId] [int] IDENTITY(1,1) NOT NULL,
	[EntityCode] [nvarchar](10) NULL,
	[ContactId] [int] NULL,
 CONSTRAINT [PK_EntityContact] PRIMARY KEY CLUSTERED 
(
	[EntityContactId] ASC
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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccountTransaction]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[AccountTransaction](
	[AccountTransactionId] [int] IDENTITY(1,1) NOT NULL,
	[ClientCode] [varchar](10) NOT NULL,
	[Description] [varchar](max) NULL,
	[TransactionDate] [datetime] NOT NULL,
	[TransactionTypeId] [int] NOT NULL,
 CONSTRAINT [PK_AccountTransaction] PRIMARY KEY CLUSTERED 
(
	[AccountTransactionId] ASC
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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Banner]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Banner](
	[BannerId] [uniqueidentifier] NOT NULL,
	[ClientCode] [varchar](10) NOT NULL,
	[BannerGroupId] [uniqueidentifier] NOT NULL,
	[Title] [varchar](100) NOT NULL,
	[DocumentID] [uniqueidentifier] NOT NULL,
	[NavigateUrl] [varchar](150) NOT NULL,
	[RequestCount] [int] NOT NULL,
	[ClickCount] [int] NOT NULL,
	[StartDateTime] [datetime] NOT NULL,
	[EndDateTime] [datetime] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Banner] PRIMARY KEY CLUSTERED 
(
	[BannerId] ASC
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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BannerReference]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BannerReference](
	[BannerReferenceId] [int] IDENTITY(1,1) NOT NULL,
	[BannerReferenceTypeId] [int] NULL,
	[BannerId] [uniqueidentifier] NULL,
	[Value] [varchar](255) NULL,
 CONSTRAINT [PK_BannerReference] PRIMARY KEY CLUSTERED 
(
	[BannerReferenceId] ASC
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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BannerAudit]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BannerAudit](
	[BannerAuditId] [int] IDENTITY(1,1) NOT NULL,
	[BannerId] [uniqueidentifier] NOT NULL,
	[ActionTypeName] [varchar](20) NULL,
	[IPAddress] [varchar](50) NULL,
	[ClientCode] [varchar](10) NOT NULL,
	[ApplicationName] [varchar](50) NULL,
	[PageUrl] [varchar](500) NULL,
	[Location] [varchar](20) NULL,
	[UserGroup] [varchar](100) NULL,
	[Gender] [varchar](10) NULL,
	[Username] [varchar](50) NULL,
	[UserId] [uniqueidentifier] NULL,
	[PostCode] [varchar](10) NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[BannerTargetUrl] [nvarchar](500) NULL,
 CONSTRAINT [PK_BannerAudit] PRIMARY KEY CLUSTERED 
(
	[BannerAuditId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'BannerAudit', N'COLUMN',N'ActionTypeName'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Available options: Click, View' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'BannerAudit', @level2type=N'COLUMN',@level2name=N'ActionTypeName'
GO
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Banner_RequestCount]') AND parent_object_id = OBJECT_ID(N'[dbo].[Banner]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Banner_RequestCount]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Banner] ADD  CONSTRAINT [DF_Banner_RequestCount]  DEFAULT ((0)) FOR [RequestCount]
END


End
GO
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Banner_ClickCount]') AND parent_object_id = OBJECT_ID(N'[dbo].[Banner]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Banner_ClickCount]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Banner] ADD  CONSTRAINT [DF_Banner_ClickCount]  DEFAULT ((0)) FOR [ClickCount]
END


End
GO
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Banner_IsDeleted]') AND parent_object_id = OBJECT_ID(N'[dbo].[Banner]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Banner_IsDeleted]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Banner] ADD  CONSTRAINT [DF_Banner_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
END


End
GO
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Banner_CreatedDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[Banner]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Banner_CreatedDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Banner] ADD  CONSTRAINT [DF_Banner_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
END


End
GO
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_BannerAudit_ActionTypeName]') AND parent_object_id = OBJECT_ID(N'[dbo].[BannerAudit]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_BannerAudit_ActionTypeName]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BannerAudit] ADD  CONSTRAINT [DF_BannerAudit_ActionTypeName]  DEFAULT ('View') FOR [ActionTypeName]
END


End
GO
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_BannerGroup_AcceptedFileType]') AND parent_object_id = OBJECT_ID(N'[dbo].[BannerGroup]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_BannerGroup_AcceptedFileType]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BannerGroup] ADD  CONSTRAINT [DF_BannerGroup_AcceptedFileType]  DEFAULT (N'OTHER') FOR [AcceptedFileType]
END


End
GO
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_BannerGroup_IsActive]') AND parent_object_id = OBJECT_ID(N'[dbo].[BannerGroup]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_BannerGroup_IsActive]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[BannerGroup] ADD  CONSTRAINT [DF_BannerGroup_IsActive]  DEFAULT ((1)) FOR [IsActive]
END


End
GO
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_DocumentStorage_IsFileComplete]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentStorage]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_DocumentStorage_IsFileComplete]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DocumentStorage] ADD  CONSTRAINT [DF_DocumentStorage_IsFileComplete]  DEFAULT ((0)) FOR [IsFileComplete]
END


End
GO
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[EmailBroadcastEntry_CreatedDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[EmailBroadcastEntry]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[EmailBroadcastEntry_CreatedDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[EmailBroadcastEntry] ADD  CONSTRAINT [EmailBroadcastEntry_CreatedDate]  DEFAULT (getdate()) FOR [CreateDateTime]
END


End
GO
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_Region_StateCode]') AND parent_object_id = OBJECT_ID(N'[dbo].[Region]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Region_StateCode]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Region] ADD  CONSTRAINT [DF_Region_StateCode]  DEFAULT (N'Other') FOR [StateCode]
END


End
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AccountTransaction_AccountTransactionType]') AND parent_object_id = OBJECT_ID(N'[dbo].[AccountTransaction]'))
ALTER TABLE [dbo].[AccountTransaction]  WITH CHECK ADD  CONSTRAINT [FK_AccountTransaction_AccountTransactionType] FOREIGN KEY([TransactionTypeId])
REFERENCES [dbo].[AccountTransactionType] ([TransactionTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AccountTransaction_AccountTransactionType]') AND parent_object_id = OBJECT_ID(N'[dbo].[AccountTransaction]'))
ALTER TABLE [dbo].[AccountTransaction] CHECK CONSTRAINT [FK_AccountTransaction_AccountTransactionType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AccountTransactionType_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[AccountTransactionType]'))
ALTER TABLE [dbo].[AccountTransactionType]  WITH CHECK ADD  CONSTRAINT [FK_AccountTransactionType_Module] FOREIGN KEY([ModuleId])
REFERENCES [dbo].[Module] ([ModuleId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_AccountTransactionType_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[AccountTransactionType]'))
ALTER TABLE [dbo].[AccountTransactionType] CHECK CONSTRAINT [FK_AccountTransactionType_Module]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[Banner_BannerGroup_FK]') AND parent_object_id = OBJECT_ID(N'[dbo].[Banner]'))
ALTER TABLE [dbo].[Banner]  WITH CHECK ADD  CONSTRAINT [Banner_BannerGroup_FK] FOREIGN KEY([BannerGroupId])
REFERENCES [dbo].[BannerGroup] ([BannerGroupId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[Banner_BannerGroup_FK]') AND parent_object_id = OBJECT_ID(N'[dbo].[Banner]'))
ALTER TABLE [dbo].[Banner] CHECK CONSTRAINT [Banner_BannerGroup_FK]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Banner_DocumentStorage]') AND parent_object_id = OBJECT_ID(N'[dbo].[Banner]'))
ALTER TABLE [dbo].[Banner]  WITH CHECK ADD  CONSTRAINT [FK_Banner_DocumentStorage] FOREIGN KEY([DocumentID])
REFERENCES [dbo].[DocumentStorage] ([DocumentID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Banner_DocumentStorage]') AND parent_object_id = OBJECT_ID(N'[dbo].[Banner]'))
ALTER TABLE [dbo].[Banner] CHECK CONSTRAINT [FK_Banner_DocumentStorage]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BannerAudit_Banner]') AND parent_object_id = OBJECT_ID(N'[dbo].[BannerAudit]'))
ALTER TABLE [dbo].[BannerAudit]  WITH CHECK ADD  CONSTRAINT [FK_BannerAudit_Banner] FOREIGN KEY([BannerId])
REFERENCES [dbo].[Banner] ([BannerId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BannerAudit_Banner]') AND parent_object_id = OBJECT_ID(N'[dbo].[BannerAudit]'))
ALTER TABLE [dbo].[BannerAudit] CHECK CONSTRAINT [FK_BannerAudit_Banner]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[BannerGroup_BannerFileType_FK]') AND parent_object_id = OBJECT_ID(N'[dbo].[BannerGroup]'))
ALTER TABLE [dbo].[BannerGroup]  WITH CHECK ADD  CONSTRAINT [BannerGroup_BannerFileType_FK] FOREIGN KEY([AcceptedFileType])
REFERENCES [dbo].[BannerFileType] ([Code])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[BannerGroup_BannerFileType_FK]') AND parent_object_id = OBJECT_ID(N'[dbo].[BannerGroup]'))
ALTER TABLE [dbo].[BannerGroup] CHECK CONSTRAINT [BannerGroup_BannerFileType_FK]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BannerReference_Banner]') AND parent_object_id = OBJECT_ID(N'[dbo].[BannerReference]'))
ALTER TABLE [dbo].[BannerReference]  WITH CHECK ADD  CONSTRAINT [FK_BannerReference_Banner] FOREIGN KEY([BannerId])
REFERENCES [dbo].[Banner] ([BannerId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BannerReference_Banner]') AND parent_object_id = OBJECT_ID(N'[dbo].[BannerReference]'))
ALTER TABLE [dbo].[BannerReference] CHECK CONSTRAINT [FK_BannerReference_Banner]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BannerReference_BannerReferenceType]') AND parent_object_id = OBJECT_ID(N'[dbo].[BannerReference]'))
ALTER TABLE [dbo].[BannerReference]  WITH CHECK ADD  CONSTRAINT [FK_BannerReference_BannerReferenceType] FOREIGN KEY([BannerReferenceTypeId])
REFERENCES [dbo].[BannerReferenceType] ([BannerReferenceTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BannerReference_BannerReferenceType]') AND parent_object_id = OBJECT_ID(N'[dbo].[BannerReference]'))
ALTER TABLE [dbo].[BannerReference] CHECK CONSTRAINT [FK_BannerReference_BannerReferenceType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BillingSettings_Billing_Bank]') AND parent_object_id = OBJECT_ID(N'[dbo].[BillingSettings]'))
ALTER TABLE [dbo].[BillingSettings]  WITH CHECK ADD  CONSTRAINT [FK_BillingSettings_Billing_Bank] FOREIGN KEY([BankId])
REFERENCES [dbo].[Billing_Bank] ([BankId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BillingSettings_Billing_Bank]') AND parent_object_id = OBJECT_ID(N'[dbo].[BillingSettings]'))
ALTER TABLE [dbo].[BillingSettings] CHECK CONSTRAINT [FK_BillingSettings_Billing_Bank]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContactAddress_Address]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContactAddress]'))
ALTER TABLE [dbo].[ContactAddress]  WITH CHECK ADD  CONSTRAINT [FK_ContactAddress_Address] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Address] ([AddressId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContactAddress_Address]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContactAddress]'))
ALTER TABLE [dbo].[ContactAddress] CHECK CONSTRAINT [FK_ContactAddress_Address]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContactAddress_Contact]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContactAddress]'))
ALTER TABLE [dbo].[ContactAddress]  WITH CHECK ADD  CONSTRAINT [FK_ContactAddress_Contact] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contact] ([ContactId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ContactAddress_Contact]') AND parent_object_id = OBJECT_ID(N'[dbo].[ContactAddress]'))
ALTER TABLE [dbo].[ContactAddress] CHECK CONSTRAINT [FK_ContactAddress_Contact]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DocumentStorage_DocumentCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentStorage]'))
ALTER TABLE [dbo].[DocumentStorage]  WITH CHECK ADD  CONSTRAINT [FK_DocumentStorage_DocumentCategory] FOREIGN KEY([DocumentCategoryId])
REFERENCES [dbo].[DocumentCategory] ([DocumentCategoryId])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DocumentStorage_DocumentCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentStorage]'))
ALTER TABLE [dbo].[DocumentStorage] CHECK CONSTRAINT [FK_DocumentStorage_DocumentCategory]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Entity_BusinessIndustry]') AND parent_object_id = OBJECT_ID(N'[dbo].[Entity]'))
ALTER TABLE [dbo].[Entity]  WITH CHECK ADD  CONSTRAINT [FK_Entity_BusinessIndustry] FOREIGN KEY([BusinessIndustryId])
REFERENCES [dbo].[BusinessIndustry] ([BusinessIndustryId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Entity_BusinessIndustry]') AND parent_object_id = OBJECT_ID(N'[dbo].[Entity]'))
ALTER TABLE [dbo].[Entity] CHECK CONSTRAINT [FK_Entity_BusinessIndustry]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Entity_Contact]') AND parent_object_id = OBJECT_ID(N'[dbo].[Entity]'))
ALTER TABLE [dbo].[Entity]  WITH CHECK ADD  CONSTRAINT [FK_Entity_Contact] FOREIGN KEY([PrimaryContactId])
REFERENCES [dbo].[Contact] ([ContactId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Entity_Contact]') AND parent_object_id = OBJECT_ID(N'[dbo].[Entity]'))
ALTER TABLE [dbo].[Entity] CHECK CONSTRAINT [FK_Entity_Contact]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EntityContact_Contact]') AND parent_object_id = OBJECT_ID(N'[dbo].[EntityContact]'))
ALTER TABLE [dbo].[EntityContact]  WITH CHECK ADD  CONSTRAINT [FK_EntityContact_Contact] FOREIGN KEY([ContactId])
REFERENCES [dbo].[Contact] ([ContactId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EntityContact_Contact]') AND parent_object_id = OBJECT_ID(N'[dbo].[EntityContact]'))
ALTER TABLE [dbo].[EntityContact] CHECK CONSTRAINT [FK_EntityContact_Contact]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EntityContact_Entity]') AND parent_object_id = OBJECT_ID(N'[dbo].[EntityContact]'))
ALTER TABLE [dbo].[EntityContact]  WITH CHECK ADD  CONSTRAINT [FK_EntityContact_Entity] FOREIGN KEY([EntityCode])
REFERENCES [dbo].[Entity] ([EntityCode])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EntityContact_Entity]') AND parent_object_id = OBJECT_ID(N'[dbo].[EntityContact]'))
ALTER TABLE [dbo].[EntityContact] CHECK CONSTRAINT [FK_EntityContact_Entity]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Subscription_Entity]') AND parent_object_id = OBJECT_ID(N'[dbo].[EntityModule]'))
ALTER TABLE [dbo].[EntityModule]  WITH CHECK ADD  CONSTRAINT [FK_Subscription_Entity] FOREIGN KEY([EntityCode])
REFERENCES [dbo].[Entity] ([EntityCode])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Subscription_Entity]') AND parent_object_id = OBJECT_ID(N'[dbo].[EntityModule]'))
ALTER TABLE [dbo].[EntityModule] CHECK CONSTRAINT [FK_Subscription_Entity]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Subscription_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[EntityModule]'))
ALTER TABLE [dbo].[EntityModule]  WITH CHECK ADD  CONSTRAINT [FK_Subscription_Module] FOREIGN KEY([ModuleId])
REFERENCES [dbo].[Module] ([ModuleId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Subscription_Module]') AND parent_object_id = OBJECT_ID(N'[dbo].[EntityModule]'))
ALTER TABLE [dbo].[EntityModule] CHECK CONSTRAINT [FK_Subscription_Module]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[ShoppingCartItem_ShoppingCart_FK]') AND parent_object_id = OBJECT_ID(N'[dbo].[ShoppingCartItem]'))
ALTER TABLE [dbo].[ShoppingCartItem]  WITH CHECK ADD  CONSTRAINT [ShoppingCartItem_ShoppingCart_FK] FOREIGN KEY([ShoppingCartId])
REFERENCES [dbo].[ShoppingCart] ([ShoppingCartId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[ShoppingCartItem_ShoppingCart_FK]') AND parent_object_id = OBJECT_ID(N'[dbo].[ShoppingCartItem]'))
ALTER TABLE [dbo].[ShoppingCartItem] CHECK CONSTRAINT [ShoppingCartItem_ShoppingCart_FK]
GO
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'BannerRefrenceType' AND ss.name = N'dbo')
CREATE TYPE [dbo].[BannerRefrenceType] AS TABLE(
	[BannerReferenceTypeId] [int] NULL,
	[BannerId] [uniqueidentifier] NULL,
	[Value] [varchar](255) NULL
)
GO
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'NameValueCollectionType' AND ss.name = N'dbo')
CREATE TYPE [dbo].[NameValueCollectionType] AS TABLE(
	[Name] [varchar](50) NOT NULL,
	[Value] [varchar](255) NULL
)
GO
