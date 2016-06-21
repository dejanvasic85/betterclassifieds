-- 1: Create temporary table to migrate the data with the new foreign key

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[EventTicketField_Temp](
	[EventTicketFieldId] [int] IDENTITY(1,1) NOT NULL,
	[EventTicketId] [int] NULL,
	[FieldName] [varchar](50) NOT NULL,
	[IsRequired] [bit] NULL,	
 
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


-- 2: Data Migration

INSERT INTO EventTicketField_Temp (EventTicketId, FieldName, IsRequired)
	SELECT	et.EventTicketId,
			etf.FieldName,
			etf.IsRequired		
	FROM	EventTicketField etf
	JOIN	[Event] e
		ON	e.EventId = etf.EventId
	JOIN	EventTicket et
		ON	et.EventId = e.EventId
GO

-- 3: Drop the Original Table 
DROP TABLE EventTicketField

GO

-- 4: Rename the temp table to the real table name
EXEC sp_rename 'EventTicketField_Temp', 'EventTicketField'

GO

-- 5: Restore the constraints including the foreign keys


ALTER TABLE EventTicketField
WITH CHECK ADD CONSTRAINT [PK_EventTicketField] PRIMARY KEY CLUSTERED 
(
	[EventTicketFieldId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]


ALTER TABLE [dbo].[EventTicketField]  WITH CHECK ADD  CONSTRAINT [FK_EventTicketField_EventTicket] FOREIGN KEY([EventTicketId])
REFERENCES [dbo].[EventTicket] ([EventTicketId])
GO

ALTER TABLE [dbo].[EventTicketField] CHECK CONSTRAINT [FK_EventTicketField_EventTicket]
GO
