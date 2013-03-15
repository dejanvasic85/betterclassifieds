CREATE TABLE [dbo].[UserProfile] (
    [UserID]                 UNIQUEIDENTIFIER NOT NULL,
    [RefNumber]              NVARCHAR (20)    NULL,
    [FirstName]              NVARCHAR (25)    NULL,
    [LastName]               NVARCHAR (25)    NULL,
    [Email]                  NVARCHAR (50)    NULL,
    [Address1]               NVARCHAR (75)    NULL,
    [Address2]               NVARCHAR (75)    NULL,
    [City]                   NVARCHAR (50)    NULL,
    [State]                  NVARCHAR (20)    NULL,
    [PostCode]               NVARCHAR (10)    NULL,
    [Phone]                  NVARCHAR (12)    NULL,
    [SecondaryPhone]         NVARCHAR (12)    NULL,
    [PreferedContact]        NVARCHAR (12)    NULL,
    [BusinessName]           NVARCHAR (75)    NULL,
    [ABN]                    NVARCHAR (12)    NULL,
    [Industry]               INT              NULL,
    [BusinessCategory]       INT              NULL,
    [ProfileVersion]         INT              NULL,
    [LastUpdatedDate]        DATETIME         NULL,
    [NewsletterSubscription] BIT              CONSTRAINT [DF__UserProfi__Newsl__2180FB33] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_CustomProfile_UserProfile] PRIMARY KEY CLUSTERED ([UserID] ASC)
);

