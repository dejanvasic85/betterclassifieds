CREATE TABLE [dbo].[Log] (
    [LogId]              UNIQUEIDENTIFIER NOT NULL,
    [Category]           VARCHAR (25)     COLLATE Latin1_General_CI_AS NOT NULL,
    [TransactionName]    VARCHAR (100)    COLLATE Latin1_General_CI_AS NULL,
    [Application]        VARCHAR (50)     COLLATE Latin1_General_CI_AS NOT NULL,
    [Domain]             VARCHAR (50)     COLLATE Latin1_General_CI_AS NULL,
    [User]               VARCHAR (50)     COLLATE Latin1_General_CI_AS NULL,
    [AccountId]          VARCHAR (50)     COLLATE Latin1_General_CI_AS NULL,
    [Data1]              VARCHAR (MAX)    COLLATE Latin1_General_CI_AS NULL,
    [Data2]              VARCHAR (MAX)    COLLATE Latin1_General_CI_AS NULL,
    [DateTimeCreated]    DATETIME         NOT NULL,
    [ComputerName]       VARCHAR (50)     COLLATE Latin1_General_CI_AS NULL,
    [IPAddress]          VARCHAR (25)     COLLATE Latin1_General_CI_AS NULL,
    [SessionId]          VARCHAR (20)     COLLATE Latin1_General_CI_AS NULL,
    [DateTimeUtcCreated] DATETIME         NULL,
    [Browser]            VARCHAR (500)    COLLATE Latin1_General_CI_AS NULL,
    CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED ([LogId] ASC)
);

