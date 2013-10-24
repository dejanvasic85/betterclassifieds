SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Lookup]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Lookup](
	[LookupId] [bigint] IDENTITY(1,1) NOT NULL,
	[GroupName] [nvarchar](50) NOT NULL,
	[LookupValue] [nvarchar](200) NOT NULL
 CONSTRAINT [PK_Lookup] PRIMARY KEY CLUSTERED 
(
	[LookupId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

INSERT INTO [dbo].[Lookup]
           ([GroupName]
           ,[LookupValue])
     VALUES
           ( 'TutorLevels', 'Any' ),
		   ( 'TutorLevels', 'Advanced' ),
		   ( 'TutorLevels', 'Intermediate' ),
		   ( 'TutorLevels', 'Beginners' ),

		   ( 'TutorTravelOptions', 'Any' ),
		   ( 'TutorTravelOptions', 'Tutor Location' ),
		   ( 'TutorTravelOptions', 'Student Location' ),
		   ( 'TutorTravelOptions', 'Mutual Location' ),

		   ( 'TutorPricingOptions', 'Flexible' ),
		   ( 'TutorPricingOptions', 'Hourly' ), 
		   ( 'TutorPricingOptions', 'Flat Rate' )


GO

