IF NOT (EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_NAME = 'EventBookingTicketValidation'))
BEGIN
   

	CREATE TABLE [dbo].[EventBookingTicketValidation](
		[EventBookingTicketValidationId] [bigint] IDENTITY(1,1) NOT NULL,
		[EventBookingTicketId] [int] NOT NULL,
		[ValidationCount] [int] NOT NULL,
	 CONSTRAINT [PK_EventBookingTicketValidation] PRIMARY KEY CLUSTERED 
	(
		[EventBookingTicketValidationId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[EventBookingTicketValidation] ADD  CONSTRAINT [DF_EventBookingTicketValidation_ValidationCount]  DEFAULT ((0)) FOR [ValidationCount]

END

