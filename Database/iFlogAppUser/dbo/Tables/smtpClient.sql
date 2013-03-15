CREATE TABLE [dbo].[smtpClient] (
    [smtpID]       INT            IDENTITY (1, 1) NOT NULL,
    [smtpClient]   NVARCHAR (150) NULL,
    [smtpUsername] NVARCHAR (50)  NULL,
    [smtpPassword] NVARCHAR (50)  NULL,
    CONSTRAINT [PK_smtpClient] PRIMARY KEY CLUSTERED ([smtpID] ASC)
);

