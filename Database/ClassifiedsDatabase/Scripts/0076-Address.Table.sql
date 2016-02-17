
/****** Object:  Table [dbo].[Address]    Script Date: 17/02/2016 11:23:24 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Address](
	[AddressId] [bigint] IDENTITY(1,1) NOT NULL,
	[StreetNumber] [varchar](30) NULL,
	[StreetName] [varchar](255) NULL,
	[Suburb] [varchar](100) NULL,
	[State] [varchar](50) NULL,
	[Postcode] [varchar](5) NULL,
	[Country] [varchar](100) NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

